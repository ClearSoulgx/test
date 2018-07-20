using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_cpn_restoration : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private Vector3 _position = new Vector3(0, 0, 0);
	void Start () {
        gameObject.transform.position = _position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
