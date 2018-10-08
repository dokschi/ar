/***********************************************************************************
	ShowAndHide3DTextClass
	- Displays the 3DText and hides it
	- done by changing localScale from 0 to 1 and back, if variabel text3DVisible set to true
	- Script "ShowAndHide3DTextClass" is attached to a parent EmptyGameObject that contains ARText (created out of ARText Prefab)
	- Background: No other good approach found to show and hide a 3DText (tried: setActive, Component MeshRenderer to false, etc.)

************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide3DTextClass : MonoBehaviour {
	public bool text3DVisible = false;

	// Initialize by changing scale to zero
	void Start () {
		transform.localScale = new Vector3 (0,0,0);
	}

	// place GameObject to new position
	public void position3DText(float x, float y, float z){
		transform.localPosition = new Vector3(x, y, z);
	}
	
	// Update is called once per frame
	void Update () {
		if (text3DVisible == true){
			transform.localScale = new Vector3 (1f,1f,1f);

		}		
		else{
			GetComponent<Renderer>().enabled= false;
			transform.localScale = new Vector3 (0f,0f,0f);
		}
	}
}
