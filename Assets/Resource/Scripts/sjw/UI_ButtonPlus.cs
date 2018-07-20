using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class UI_ButtonPlus : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    //const string _TraceParticleSystemPrefabPath = "External_Resource/JMO Assets/Cartoon FX/CFX4 Prefabs/Light/CFX4 Aura Bubble C";
    //const string _ClickParticleSystemPrefabPath = "External_Resource/JMO Assets/Cartoon FX/CFX4 Prefabs/Light/CFX4 Aura Bubble C";

    //[SerializeField] private  GameObject _TraceParticleSystemPrefab = Resources
    //    .Load(_TraceParticleSystemPrefabPath) as GameObject;
    //[SerializeField]
    //private GameObject _ClickParticleSystemPrefab = Resources
    //    .Load(_ClickParticleSystemPrefabPath) as GameObject;
    public GameObject _ClickParticleSystemPrefab, _TraceParticleSystemPrefab = null;
    [SerializeField]
    //private GameObject _TraceParticleSystemPrefab = null;
    private float _alphaThrehold = 0;
    public float alphaThrehold
    {
        get { return _alphaThrehold; }
        set { _alphaThrehold = value;if(_alphaThrehold>0)AlphaHitTest(); }
    }
    private GameObject _ParticleSystemInstance = null;

    void Start() {
        if (_alphaThrehold > 0) AlphaHitTest();
    }
    private void AlphaHitTest(){
    Image image = gameObject.GetComponent<Image>();
        if (image != null) image.alphaHitTestMinimumThreshold = alphaThrehold;
        else Debug.Log(gameObject + " don't have image component");
        }
	// Update is called once per frame
	void FixedUpdate () {
        TracePointer();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("hit : " + gameObject);
       
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Destroy(_ParticleSystemInstance);
        Vector3 input_position = Input.mousePosition;
        if (_ClickParticleSystemPrefab != null)
        {
            _ParticleSystemInstance = Instantiate(_ClickParticleSystemPrefab) as GameObject;
            //TODO
            _ParticleSystemInstance.layer = 5;
            if (Camera.current != null)
                _ParticleSystemInstance.transform.position = Camera.main.ScreenToWorldPoint(input_position) + Vector3.forward;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Destroy(_ParticleSystemInstance);
        //Vector3 input_position = Input.mousePosition;
        //_ParticleSystemInstance = Instantiate(_TraceParticleSystemPrefab) as GameObject;
        //if (Camera.current != null) _ParticleSystemInstance.transform.position = Camera.current.ScreenToWorldPoint(input_position) + Vector3.forward;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = 1.05f* gameObject.transform.localScale;
        if (_TraceParticleSystemPrefab != null && _ParticleSystemInstance == null)
        {
            _ParticleSystemInstance = Instantiate(_TraceParticleSystemPrefab) as GameObject;
            //TODO
            _ParticleSystemInstance.layer = 5;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = Vector3.one;
        Destroy(_ParticleSystemInstance);
        _ParticleSystemInstance = null;
    }

    void TracePointer()
    {
        if(_ParticleSystemInstance!=null)
        {   
            Vector3 input_position = Input.mousePosition;
           // Debug.Log("input_position: "+input_position);
            if (Camera.current != null)
            {
                Vector3 tmpPosition = Camera.allCameras[0].ScreenToViewportPoint(input_position);
               // Debug.Log("tmpPosition : "+tmpPosition);

                _ParticleSystemInstance.transform.position = Camera.current.ViewportToWorldPoint(tmpPosition) + Vector3.forward;
                Vector3 tmpPosition2 = Camera.current.ViewportToWorldPoint(tmpPosition);
             //   Debug.Log("Camera.current.ViewportToWorldPoint(input_position) " + Camera.current.ScreenToWorldPoint(input_position));
               // Debug.Log(Camera.current);
            }
        }
    }

}
