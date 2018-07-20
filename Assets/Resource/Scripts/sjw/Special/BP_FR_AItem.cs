using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_FR_AItem : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Messenger.AddListener(GameEvent.START_MATCH, OnStartMatch);
        Messenger.AddListener(GameEvent.END_MATCH, OnEndMatch);
    }
	
	

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.START_MATCH, OnStartMatch);
        Messenger.RemoveListener(GameEvent.END_MATCH, OnEndMatch);
    }

    public void OnStartMatch()
    {
        gameObject.SetActive(false);
    }


    public void OnEndMatch()
    {
        gameObject.SetActive(true);
    }
}
