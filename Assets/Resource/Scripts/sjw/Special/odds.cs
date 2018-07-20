using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUIAnimator;

public class odds : MonoBehaviour {

    private Vector2 tarPos = new Vector2(0.16f, -0.85f);
    private void Awake()
    {
       Messenger.AddListener(GameEvent.END_MATCH, OnEndMatch);
        Messenger.AddListener(GameEvent.START_MATCH, OnStartMatch);
    }
 
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.END_MATCH, OnEndMatch);
        Messenger.RemoveListener(GameEvent.START_MATCH, OnStartMatch);
    }

   private  GameObject cloneOne = null;
    [SerializeField] private RuntimeAnimatorController animator;
    public  void OnStartMatch()
    {
        GameObject original = Singleselect.curSelect.gameObject;
        cloneOne = GameObject.Instantiate(original,original.transform.position,original.transform.rotation, GameObject.Find("Canvas").transform);
        cloneOne.GetComponent<Singleselect>().changeSelectedState(true);
        // cloneOne.transform.position = Singleselect.curSelect.gameObject

        //foreach (Transform child in transform)
        // {
        //     Debug.Log(child);
        //     child.gameObject.SetActive(false);
        // }
        StartCoroutine(Rate_StartMatch());
    }

    IEnumerator Rate_StartMatch()
    {

        //cloneOne.GetComponent<Animator>().SetTrigger(GameEvent.START_MATCH);
        yield return new WaitForSeconds(0.1f);
        // gameObject.SetActive(false);
        Debug.Log("play");
        UIAnimator._Move(cloneOne.transform as RectTransform,new Vector2(0f,0f), tarPos, 0.5f,false).SetModifier(Modifier.PolyIn).Play();
     gameObject.SetActive(false);
    }

    public void OnEndMatch()
    {
        cloneOne.SetActive(false);
        gameObject.SetActive(true);
    }
}
