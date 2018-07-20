#if UNITY_ANDROID && !UNITY_EDITOR
#define ANDROID
#endif
#if UNITY_IPHONE && !UNITY_EDITOR
#define IPHONE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EasyUIAnimator;
using UnityEngine.UI;
public  class UI_Controller : MonoBehaviour {
    // Use this for initialization
    public Stack<GameObject> curPannelStack;
   // public enum UI_Panels : int { DEFAULT = 0, MAIN, VIP }
    public Dictionary<string, UI_Panels> UI_NAME2PanelIdx = new Dictionary<string, UI_Panels>();
    void Start () {
        curPannelStack = new Stack<GameObject>();
        UI_InteractionMatchAnimation_Init();
      
    }

    private void UI_InteractionMatchAnimation_Init()
    {
       UI_NAME2PanelIdx.Add("DEFAULT", UI_Panels.DEFAULT);
       UI_NAME2PanelIdx.Add("Main",UI_Panels.MAIN);
        UI_NAME2PanelIdx.Add("VIP", UI_Panels.VIP);
    }

    // Update is called once per frame
    void Update () {
        Test();
	}
    static public void SetActive(GameObject go, bool state)
    {
        if (go == null)
        {
            return;
        }
        if (go.activeSelf != state)
        {
            go.SetActive(state);
        }
    }

    public void Test()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
#if IPHONE || ANDROID
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if (EventSystem.current.IsPointerOverGameObject())
#endif
                Debug.Log("当前触摸在UI上");

            else
                Debug.Log("当前没有触摸在UI上");
        }
    }

    public void On_Click_Character()
    {
        Debug.Log("click character(rawImage)");
        Messenger<Character.Character_Interactions>.Broadcast(GameEvent.CHARACTER_INTERACTIONS, Character.Character_Interactions.FEMALE_MOVE_1);
        TrasMessenger tras = GameObject.FindObjectOfType<TrasMessenger>();
        //if(tras!=null)
    }

   
     public void On_Click_Close(PanelPlus ClosePanel)
    {
        if (ClosePanel == null|| !ClosePanel.gameObject.activeSelf) return;
        UI_Panels panel = ClosePanel.panelIndex;
        //SetActive(ClosePanel.gameObject, true);
        UIAnimation animationMatched;
        switch (panel)
        {
            case UI_Panels.VIP:
                animationMatched = UIAnimator.Scale(ClosePanel.UnmaskRegion.transform as RectTransform, new Vector3(1f,1f), new Vector3(1.1f, 1.1f), 0.3f)
                    .SetModifier(Modifier.PolyIn);
                UIAnimation animationMatched2 = UIAnimator.Scale(ClosePanel.UnmaskRegion.transform as RectTransform, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(0.3f, 0.3f), 0.2f)
                    .SetModifier(Modifier.PolyOut);
                
                animationMatched.SetCallback(animationMatched2.Play);
     
                animationMatched2.SetCallback(ClosePanel.Inactive);
                //TODO
                if (TempHideObject != null) animationMatched2.SetCallback(TempHideObject.Active, true);
                break;
            default:
               // if(UIAnimator.Animations.Count!=0) return;
                animationMatched = UIAnimator
                    .FadeOut(ClosePanel.UnmaskRegion.GetComponent<Image>(), 0.2f).SetModifier(Modifier.PolyIn);
                animationMatched.SetCallback(ClosePanel.Inactive);
                break;
        }
        animationMatched.Play();
       //curPannelStack.Pop();             //add an interpretation in BACK()
    }

     public void On_Click_Back()
    {
        PanelPlus ClosePanel = curPannelStack.Pop().GetComponent<PanelPlus>();
        //pop the curPanel anyway,if it has been closed,just do nothing 
        if (ClosePanel == null || !ClosePanel.gameObject.activeSelf) return;
        UI_Panels panel = ClosePanel.panelIndex;
        //SetActive(ClosePanel.gameObject, true);
        UIAnimation animationMatched;
        switch (panel)
        {
            case UI_Panels.VIP:
                animationMatched = UIAnimator.Scale(ClosePanel.UnmaskRegion.transform as RectTransform, new Vector3(1f, 1f), new Vector3(1.1f, 1.1f), 0.3f)
                    .SetModifier(Modifier.PolyIn);
                UIAnimation animationMatched2 = UIAnimator.Scale(ClosePanel.UnmaskRegion.transform as RectTransform, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(0.3f, 0.3f), 0.2f)
                    .SetModifier(Modifier.PolyOut);

                animationMatched.SetCallback(animationMatched2.Play);
                animationMatched2.SetCallback(ClosePanel.Inactive);
                break;
            default:
                if (UIAnimator.Animations.Count != 0) return;
                animationMatched = UIAnimator
                    .FadeOut(ClosePanel.UnmaskRegion.GetComponent<Image>(), 0.2f).SetModifier(Modifier.PolyIn);
                animationMatched.SetCallback(ClosePanel.Inactive);
                break;
        }
        animationMatched.Play();
    }
    public PanelPlus TempHideObject;
    public void On_Click_Open(PanelPlus OpenPanel)
    {
        SetActive(OpenPanel.gameObject, true);
        UI_Panels PanelIndex = OpenPanel.panelIndex;
        UIAnimation animationMatched;
        switch (PanelIndex)
        {
            case UI_Panels.VIP:
                animationMatched = UIAnimator.Scale(OpenPanel.UnmaskRegion.transform as RectTransform, new Vector3(0.0f, 0.0f), new Vector3(1.1f, 1.1f), 0.05f).SetModifier(Modifier.PolyIn);
                UIAnimation animationMatched2 = UIAnimator.Scale(OpenPanel.UnmaskRegion.transform as RectTransform, new Vector3(1.1f, 1.1f), new Vector3(1f, 1f), 0.1f).SetModifier(Modifier.PolyOut);
                animationMatched.SetCallback(animationMatched2.Play);
                //TODO:
                if (TempHideObject!=null)TempHideObject.Inactive();  
                break;
            default:
                animationMatched = UIAnimator
                    .FadeIn(OpenPanel.UnmaskRegion.GetComponent<Image>(),0.4f).SetModifier(Modifier.PolyIn);
                StartCoroutine(WaitForDefaultAnimation(OpenPanel));
                break;
        }
        animationMatched.Play();
        curPannelStack.Push(OpenPanel.gameObject);
    }
     IEnumerator WaitForDefaultAnimation(PanelPlus OpenPanel)
    {
        yield return new WaitForSeconds(1.4f);
        On_Click_Close( OpenPanel);

    }


    public void SwitchToScene(string index)
    {
        Messenger<string>.Broadcast(GameEvent.SWITCH_SCENCE, index);
    }

   
}
