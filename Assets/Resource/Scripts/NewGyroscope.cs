#if UNITY_ANDROID && !UNITY_EDITOR
#define ANDROID
#endif
#if UNITY_IPHONE && !UNITY_EDITOR
#define IPHONE
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGyroscope : MonoBehaviour {
    public float sensitivity = 15f;//灵敏度
    public float maxYspeed = 5f;//Y最大转动速度
    public float maxXspeed = 25f;//X最大转动速度
    public GameObject[] target;//转动对象
    private Vector3 m_MobieOrientation;//手机移动的值，-1~1
    private Vector3 m_TagerTransofm;//移动物体的Transofm
    public Vector3 m_InitialYAxisTransofm=new Vector3(25,14,0);//初始位置
    public enum RotationAxial//选择转动的轴
    {
        x,
        y, 
        xAndy
    }
    public RotationAxial rotationAxialState;
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;//手机横屏显示
        m_TagerTransofm = Vector3.zero;//初始化位置
    }    
    private void LateUpdate()
    {
#if IPHONE || ANDROID
       m_MobieOrientation = Input.acceleration;
#else
        m_MobieOrientation = new Vector3(0.5f*(Mathf.Sin(Time.realtimeSinceStartup)), 0.5f*( Mathf.Cos(Time.realtimeSinceStartup)),0);
#endif
        switch (rotationAxialState)
        {
            case RotationAxial.x:
                m_TagerTransofm.x = Mathf.Lerp(m_TagerTransofm.x, m_MobieOrientation.y * maxXspeed, 0.2f);
                break;
            case RotationAxial.y:
                m_TagerTransofm.y = Mathf.Lerp(m_TagerTransofm.y, -m_MobieOrientation.x * maxYspeed, 0.2f);
                break;
            case RotationAxial.xAndy:
                {
                    m_TagerTransofm.y = Mathf.Lerp(m_TagerTransofm.y, -m_MobieOrientation.x * maxYspeed, 0.2f);   
                    m_TagerTransofm.x = Mathf.Lerp(m_TagerTransofm.x, m_MobieOrientation.y * maxXspeed, 0.2f);
                    m_TagerTransofm.x = Mathf.Clamp(m_TagerTransofm.x, -25, 25);
                }
                break;
        }
        foreach (GameObject tager in target)
        {
            tager.transform.localRotation = Quaternion.Lerp(tager.transform.localRotation,
                 Quaternion.Euler(m_TagerTransofm + m_InitialYAxisTransofm), sensitivity * Time.deltaTime);
        }
    }
}

