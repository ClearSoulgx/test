
using System;
using UnityEngine;
using System.Collections;

using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
namespace UnityEngine.UI
{
    public class InitButton : MonoBehaviour
    {

        [SerializeField] private bool resetTargetGraphic = true;
        [SerializeField] private bool resetTrasition = true;
        [SerializeField]
        private Selectable.Transition transition = Selectable.Transition.None;

        [FormerlySerializedAs("addParticleEffect")]
        [SerializeField] private bool addParticleEffect = false;
        //[SerializeField] string _TraceParticleSystemPrefabPath = "External_Resource/JMO Assets/Cartoon FX/CFX4 Prefabs/Light/CFX4 Aura Bubble C";
        //[SerializeField] string _ClickParticleSystemPrefabPath = "External_Resource/JMO Assets/Cartoon FX/CFX4 Prefabs/Light/CFX4 Aura Bubble C";
        [SerializeField] GameObject _TraceParticleSystemPrefab = null;
        [SerializeField] GameObject _ClickParticleSystemPrefab = null;

        [SerializeField] private bool addClickedEvent = false;
        [Serializable]
        public class ButtonClickedEvent : Button.ButtonClickedEvent { }
        [FormerlySerializedAs("add to Click")]
        [SerializeField]
        private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

        [FormerlySerializedAs("navigation")]
        [SerializeField]
        private bool m_Navigation = false;

        // public bool navigation { get { return m_Navigation; } set { if (SetPropertyUtility.SetStruct(ref m_Navigation, value)) OnSetProperty(); } }
        public void reset()
        {
            Image tempImage;
            RawImage tempRawImage;
            Button button;
            UI_ButtonPlus buttonPlus;

            Debug.Log("addParticleEffect is : " + addParticleEffect);
            ArrayList transforms = BFS(transform);
            foreach (Transform _transform in transforms)
            {
                if (_transform.gameObject.activeSelf)
                {

                    if (button = _transform.GetComponent<Button>())
                    {
                        // resetTargetGraphic to it's comPonent
                        if (resetTargetGraphic)
                        {

                            if (tempImage = button.gameObject.GetComponent<Image>())
                            {
                                button.targetGraphic = tempImage;
                                Debug.Log("Reset button" + button.name + "to it's Image");

                            }
                            else if (tempRawImage = button.gameObject.GetComponent<RawImage>())
                            {

                                button.targetGraphic = tempRawImage;
                                Debug.Log("Reset button" + button.name + "to it's RawImage");

                            }
                            else Debug.Log("button" + button.name + "can't find default targetGraphic");

                        }
                        if (resetTrasition)
                        {
                            button.transition = transition;
                        }

                        // addClickedEvent to all child button
                        if (addClickedEvent)
                        {
                            button.onClick = m_OnClick;
                        }
                        buttonPlus = _transform.gameObject.GetComponent<UI_ButtonPlus>();
                        if (addParticleEffect)
                        {

                            if (buttonPlus == null) buttonPlus = _transform.gameObject.AddComponent<UI_ButtonPlus>(); ;

                            // mainly reset the particle effect with interactions

                          //  buttonPlus.alphaThrehold = 0f;
                            buttonPlus._ClickParticleSystemPrefab = _ClickParticleSystemPrefab;
                            buttonPlus._TraceParticleSystemPrefab = _TraceParticleSystemPrefab;
                            Debug.Log("reset" + gameObject + "'s buttonPlus");
                        }
                        //}else
                        //{
                        //    if (buttonPlus != null)
                        //    {
                        //        buttonPlus.alphaThrehold = 0f;
                        //        buttonPlus._ClickParticleSystemPrefab = null;
                        //        buttonPlus._TraceParticleSystemPrefab = null;
                        //    }
                        //}
                        

                    }

                }
            }
        }

        static public ArrayList BFS(Transform root)
        {
            ArrayList result = new ArrayList();
            Stack temp= new Stack();
            Transform child;
            temp.Push(root);
            while (temp.Count != 0)
            {
                child = temp.Pop() as Transform;
                result.Add(child);
                foreach(Transform _transform in child)
                {
                    temp.Push(_transform);
                }
            }
            return result;
        }
    }
}