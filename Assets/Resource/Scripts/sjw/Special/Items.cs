using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class Items : MonoBehaviour
{

    public enum Item_State : int { Need_Buy = 1, Unused = 2, Used = 3 };
    [SerializeField] private int item_id = -1;
    [SerializeField] private Item_State _itemState;
    private Dictionary<Item_State, string> Item_State_Tags = new Dictionary<Item_State, string>
    {
        {Item_State.Need_Buy,"Item_NeedBuy" },{Item_State.Unused,"Item_Unused" },{Item_State.Used,"Item_Used" }
    };
    private GameObject curStateObject = null;

    public Item_State itemState
    {
        get { return _itemState; }
        set { if ((int)value != 0) ChangeState(value); }
    }

    // Use this for initialization
    void Start()
    {
        InitItem();


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitItem()
    {
        List<string> tmpl = new List<string>();
        string tmp;
        Transform tmpo;
        foreach (Item_State i in Enum.GetValues(typeof(Item_State)))
        {
            Item_State_Tags.TryGetValue(i, out tmp);
            tmpo = transform.Find(tmp);
            if (tmpo == null)
            {
                StringBuilder debug = new StringBuilder("item " + gameObject + " don't have child with tag:" + tmp);
                Debug.Log(debug.ToString());
                return;
            }
            if (i != _itemState) tmpo.gameObject.SetActive(false);
            if (i == _itemState) { tmpo.gameObject.SetActive(true); curStateObject = tmpo.gameObject; }
        }

    }
    public void ChangeState(Item_State change2State) { ChangeState((int)change2State); }
    public void ChangeState(int change2State)
    {
        if (itemState == (Item_State)change2State && curStateObject != null)
        {
            StringBuilder debug = new StringBuilder("item " + gameObject + " change to the same state!");
            Debug.Log(debug.ToString());
            return;
        }
        if (curStateObject != null) curStateObject.SetActive(false);
        string state;
        Item_State_Tags.TryGetValue((Item_State)change2State, out state);
        curStateObject = transform.Find(state).gameObject;
        if (curStateObject == null)
        {
            StringBuilder debug = new StringBuilder("item " + gameObject + " don't have child with tag:" + state);
            Debug.Log(debug.ToString());
            return;
        }
        curStateObject.SetActive(true);
        _itemState = (Item_State)change2State;
    }

    public void Buy_Item()
    {
        if (_itemState != Item_State.Need_Buy)
        {
            StringBuilder debug = new StringBuilder("item " + gameObject + "don't need to buy");
            Debug.Log(debug.ToString());
            return;
        }
        //.......
        ChangeState(Item_State.Used);
    }

    public void UseItem()
    {
        if (_itemState != Item_State.Unused)
        {
            StringBuilder debug = new StringBuilder("item " + gameObject + "don't need to use");
            Debug.Log(debug.ToString());
            return;
        }
        //.......
        ChangeState(Item_State.Used);
    }


    public void UnuseItem()
    {
        if (_itemState != Item_State.Used)
        {
            StringBuilder debug = new StringBuilder("item " + gameObject + "don't need to Unuse");
            Debug.Log(debug.ToString());
            return;
        }
        //.......
        ChangeState(Item_State.Unused);
    }

    public void OpenChildPanel(PanelPlus Panel)
    {
        Transform temp = Panel.transform.parent;
        Panel.transform.parent = transform;
        (Panel.transform as RectTransform).anchoredPosition = new Vector2(192, 108);
         Panel.transform.parent = temp;
        Panel.Active();

    }

    public void CloseChildPanel(PanelPlus Panel)
    {
        //PanelPlus Panel;
        //foreach (Transform _transform in transform)
        //{
        //    Panel = _transform.GetComponent<PanelPlus>();
        //    if (Panel != null) Panel.Inactive();
        //}
        Panel.Inactive();

    }
}
