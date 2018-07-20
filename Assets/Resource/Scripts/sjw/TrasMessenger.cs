using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrasMessenger : MonoBehaviour
{

    // Use this for initialization
    public  static List<GameEventTrigger.Entry> m_Delegates;
    public static List<GameEventTrigger.Entry> triggers
    {
        get
        {
            if (m_Delegates == null)
                m_Delegates = new List<GameEventTrigger.Entry>();
            return m_Delegates;
        }
        set { m_Delegates = value; }
    }
   
    private void Start()
    {
        string _event;
        GameEvent.InitDic();
        //foreach (GameEventTrigger.Entry entry in m_Delegates)
        //{
        //    GameEvent.GameEventDic.TryGetValue((int)entry.eventID, out _event);
        //    if (_event != null)
        //        Messenger<GameEventTriggerType>.AddListener(_event, Excute);
        //}
    }


    public static void Excute(GameEventTriggerType id)
    {
        for (int i = 0, imax = triggers.Count; i < imax; ++i)
        {
            var ent = triggers[i];
            if (ent.eventID == id && ent.callback != null)
                ent.callback.Invoke();
        }
    }

    public static void Excute(string _event)
    
        {
            GameEventTriggerType id;
            GameEvent.GameEventDic.TryGetValue(_event,out id);
            Excute (id);
    }
    
}
