using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;
using System.Linq;
using System.Text;

namespace UnityEngine.EventSystems
{
    [AddComponentMenu("Event/GameEvent Trigger")]
    public class GameEventTrigger :
        MonoBehaviour
        
    {
        [Serializable]
     //   public class TriggerEvent : UnityEvent<BaseEventData>
            public class TriggerEvent : UnityEvent
        { }

        [Serializable]
        public class Entry
        {
            public GameEventTriggerType eventID = GameEventTriggerType.CHARACTER_INTERACTIONS;
            public TriggerEvent callback = new TriggerEvent();
        }

        [FormerlySerializedAs("delegates")]
        [SerializeField]
        private List<Entry> m_Delegates;


        protected GameEventTrigger()
        { }

        public List<Entry> triggers
        {
            get
            {
                if (m_Delegates == null)
                    m_Delegates = new List<Entry>();
                return m_Delegates;
            }
            set { m_Delegates = value; }
        }

        //private void Execute(GameEventTriggerType id, BaseEventData eventData)
        //{
        //    for (int i = 0, imax = triggers.Count; i < imax; ++i)
        //    {
        //        var ent = triggers[i];
        //        if (ent.eventID == id && ent.callback != null)
        //            ent.callback.Invoke(eventData);
        //    }
        //}

        private void Awake()
        {
            //TrasMessenger tras = GameObject.FindObjectOfType<TrasMessenger>();
            if (m_Delegates != null) {
                if (TrasMessenger.m_Delegates != null)
                    TrasMessenger.m_Delegates = TrasMessenger.m_Delegates.Union(m_Delegates).ToList<Entry>();
                else TrasMessenger.m_Delegates = m_Delegates;
            }
        }

    }
}
