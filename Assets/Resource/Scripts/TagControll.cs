using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagControll : MonoBehaviour {
    public ScrollRect scroll;
    public Animator listAnimator;
    public Animator tagsAnimator;
    private Vector3 initPosition;
    private Toggle toggle;
    private void Start()
    {
        toggle = GetComponent<Toggle>();
        initPosition = listAnimator.gameObject.GetComponent<RectTransform>().position;//记录初始位置
        listAnimator.SetBool("IsShow", true);
    }
    public void ValueChange()
    {
        if(toggle.isOn==true)
        {
            scroll.enabled = true;
            listAnimator.SetBool("IsShow", true);
        }
        else
        {            //当标签失效的时候，滑动列表的位置变为初始位置
            listAnimator.gameObject.GetComponent<RectTransform>().position = initPosition;
            scroll.enabled = false;
            listAnimator.SetBool("IsShow", false);
        }
    }
    public void TagValueChange()
    {
        if (toggle.isOn == true)
        {
            tagsAnimator.SetBool("IsShow", true);            
        }
        else
        {
            tagsAnimator.SetBool("IsShow",false);
        }
    }
}
