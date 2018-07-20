using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;
using System;
using System.Text;

public class UI_Ctrl_BP : MonoBehaviour {
    //private float matchTime = 0;
    private System.DateTime matchTime;
    // Use this for initialization
    string debug;
    string path = "localhost:8080/test";
    void Start () {
       
        StartCoroutine(GoForm());

    }
    IEnumerator GoForm()
    {
        WWW www = new WWW(path);
        yield return www;

        if (www.isDone)
        {
            debug = www.text;
        }
    }

    // Update is called once per frame
    void Update () {
        
		if(InMatch.activeSelf)
        {
            //Due to fucking .Net2.0
           
            TimeSpan ts3 = System.DateTime.Now- matchTime;
            //你想转的格式
            //InMatchText.text = ts3.Minutes.ToString()+":" +ts3.Seconds.ToString();
            StringBuilder debug = new StringBuilder((ts3.Minutes>=10?"":"0") + ts3.Minutes
                + ":" + (ts3.Seconds >= 10 ? "" : "0")+ ts3.Seconds);
            InMatchText.text = debug.ToString();
            //  InMatchText.text = string.Format("{0:T}", System.DateTime.Now );
        }

    }
    public GameObject UnMatch;
    public GameObject InMatch;
    public Text InMatchText;
    
    public void match()
    {
        if (UnMatch.activeSelf)
        {
            matchTime = System.DateTime.Now;
            UnMatch.SetActive(false);
            InMatch.SetActive(true);
            Messenger.Broadcast(GameEvent.START_MATCH);
        }
        else if (InMatch.activeSelf)
        {
            InMatch.SetActive(false);
            UnMatch.SetActive(true);
            Messenger.Broadcast(GameEvent.END_MATCH);
        }
        else Debug.Log("match error!");
        return;
    }
}
