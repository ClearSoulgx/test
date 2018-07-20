using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour {
	public Camera modelCamera;//用来渲染的相机
	private Animation currerentAnimation;//控制动画
	private Ray ray;
	private RaycastHit hitIofo; 
	private void Awake() 
	{
		modelCamera=modelCamera.GetComponent<Camera>();
		currerentAnimation=GetComponent<Animation>();
		currerentAnimation.playAutomatically=true;
	}
	private void Update() 
	{
		if(Input.GetMouseButtonDown(0))
		{
			ray=modelCamera.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray,out hitIofo))//射线检测
			{
				if(hitIofo.collider.tag=="Player")
				{
					currerentAnimation.Play("Male_move_1");				
				}
			}
		}	
	}

}
