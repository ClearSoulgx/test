using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using EasyUIAnimator;

[AddComponentMenu("UI/PanelPlus")]
[DisallowMultipleComponent]
//[RequireComponent(typeof(Panel))]

public class PanelPlus : MonoBehaviour {

    [SerializeField] private GameObject _UnmaskRegion=null;
    public GameObject UnmaskRegion {
        get { if (_UnmaskRegion == null) Debug.Log(gameObject + "didn't set UnmaskRegion!");return _UnmaskRegion; }
        set { _UnmaskRegion = value; }
    }

    [SerializeField] private UI_Panels _panelIndex;
    public UI_Panels panelIndex
    {
        get { if (_panelIndex <0) Debug.Log(gameObject + "didn't set panelIndex!"); return _panelIndex; }
        set { _panelIndex = value; }
    }

    /*-----------------------------------------------------------------------------------------------------------*/
    private void Awake()
    {
      //Messenger<UI_Panels>.AddListener(GameEvent.PANEL_BACK,On_Panel_Back);
       // Messenger<string>.AddListener(GameEvent.PANEL_OPEN, On_Panel_Open);
        //gameObject.SetActive(false);
    }
    void Start () {}
    void Update () {}
    private void OnDestroy()
    {
       //Messenger<UI_Panels>.AddListener(GameEvent.PANEL_BACK, On_Panel_Back);
        //Messenger<string>.AddListener(GameEvent.PANEL_OPEN, On_Panel_Open);
    }
    private void OnEnable()
    {
        if (UnmaskRegion.GetComponent<Animator>() != null)
            UnmaskRegion.GetComponent<Animator>().SetBool("isshow", false);
    }
    /*-----------------------------------------------------------------------------------------------------------*/

   public void Active()
    {
        gameObject.SetActive(true);
    }
    public void Inactive()
    {
        gameObject.SetActive(false);
    }
    //private void On_Panel_Back(UI_Controller.UI_Panels panel)
    //{
    //    string closePanelName = "VIP";
    //    if (gameObject.name == closePanelName)
    //    {
    //        UI_Controller.SetActive(gameObject, true);
    //        //UIAnimation scale1Animation = UIAnimator.Scale(UnmaskRegion.transform as RectTransform, new Vector3(0.0f, 0.0f), new Vector3(1f, 1f), 0.5f).SetModifier(Modifier.PolyIn);
    //        UIAnimation animationMatched;
    //        //UI_Animaons.TryGetValue(OpenPanelName, out animationMatched);
    //        UI_Controller.UI_Panels PanelIndex;
    //        UI_Controller.UI_NAME2PanelIdx.TryGetValue(closePanelName, out PanelIndex);
    //        switch (PanelIndex)
    //        {
    //            default:
    //                animationMatched = UIAnimator.Scale(UnmaskRegion.transform as RectTransform, new Vector3(1f, 1f), new Vector3(1.1f, 1.1f), 0.3f).SetModifier(Modifier.PolyIn);
    //                UIAnimation animationMatched2 = UIAnimator.Scale(UnmaskRegion.transform as RectTransform, new Vector3(1, 1f, 1.1f), new Vector3(0f, 0f), 0.5f).SetModifier(Modifier.PolyIn);
    //                animationMatched.SetCallback(animationMatched2.Play);
    //                animationMatched.Play();
    //                break;
    //        }
    //        animationMatched.Play();
    //        UI_Controller.curPannelStack.Push(gameObject);
    //    }
    //}

    //private void On_Panel_Open(string OpenPanelName)
    //{
    //    if (gameObject.name == OpenPanelName)
    //    {
    //        UI_Controller.SetActive(gameObject, true);
    //        //UIAnimation scale1Animation = UIAnimator.Scale(UnmaskRegion.transform as RectTransform, new Vector3(0.0f, 0.0f), new Vector3(1f, 1f), 0.5f).SetModifier(Modifier.PolyIn);
    //        UIAnimation animationMatched;
    //        //UI_Animaons.TryGetValue(OpenPanelName, out animationMatched);
    //        UI_Panels PanelIndex;
    //        UI_Controller.UI_NAME2PanelIdx.TryGetValue(OpenPanelName,out PanelIndex);
    //        switch (PanelIndex)
    //        {
    //            default:
    //                animationMatched = UIAnimator.Scale(UnmaskRegion.transform as RectTransform, new Vector3(0.0f, 0.0f), new Vector3(1f, 1f), 0.5f).SetModifier(Modifier.PolyIn);
    //                break;
    //        }
    //        animationMatched.Play();
    //        UI_Controller.curPannelStack.Push(gameObject);
    //    }
    //}
}
