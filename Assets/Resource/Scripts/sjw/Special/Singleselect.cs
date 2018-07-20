using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Singleselect : MonoBehaviour, IPointerClickHandler
{

    // Use this for initialization
    private static Transform curSlectedOne=null;
    public static  Transform curSelect
    {
        get { return curSlectedOne ; }
    }
    private bool isSelected = false;
    [SerializeField] private Sprite selectedImg, unSelectedImg;
    [SerializeField] private Color selectedColor, unSelectedColor;
    void Start () {
        changeSelectedState(isSelected);
   //     curSlectedOne = null;

    }
    void Awake()
    {
        Messenger.AddListener(GameEvent.SINGLE_SELECT, ON_Single_Select);
       // Messenger.AddListener(GameEvent.START_MATCH, OnStartMatch);
        //    Messenger.AddListener(GameEvent)
    }
    // Update is called once per frame
    void Update () {
		
	}

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SINGLE_SELECT, ON_Single_Select);
       // Messenger.RemoveListener(GameEvent.START_MATCH, OnStartMatch);
    }

    public void ON_Single_Select()
    {
        if (transform != curSlectedOne && isSelected) changeSelectedState(false); 
    }

    public  void changeSelectedState(bool selected)
    {
        gameObject.GetComponent<Image>().sprite = selected ? selectedImg : unSelectedImg;
      //  gameObject.transform
        transform.Find("Text").GetComponent<Text>().color = selected ? selectedColor : unSelectedColor;
        isSelected = selected;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        curSlectedOne = transform;
        Messenger.Broadcast(GameEvent.SINGLE_SELECT);
        changeSelectedState(true);

    }

   

}
