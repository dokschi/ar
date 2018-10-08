/***********************************************************************************
	AnimationAreaObjectsClass
	- Displays with an animation a GameObject or hides it
	- Animates GameObject by scaling out of zero point to original size using the animation "AreaAnimation""
	- Script "AnimationAreaObjectClass" can be attached to overlays for any kind of GameObject as buttons, wheels, etc.
	- To use it properly the GameObject (using the script) shall be attached as a child to a "parent" EmptyGameObject:
		- GameObject (Child) should have scale (1,1,1) and position (0,0,0)
		- Parent EmptyGameObject should have right scale and position
		- Animator AreaAnimation needs to be attached to GameObject (Child) 

	Methods:
	- public void ShowArea(void):	Displays Gameobject and starts Animation
	- public void HideArea(void):	Hides Gameobject

************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAreaObjectsClass : MonoBehaviour {
	Animator animButtonX;
	bool objectVisible = false; // Flag to hide object in Update() Function, as initial "renderer = false" doesnt work

	// Use this for initialization
	void Start () {
		animButtonX = GetComponent<Animator>();
		//GetComponent<Renderer>().enabled = false; // doesn't work due to parallel threads, therefore HideArea() is called in ControlScript in Update()
	}
	
	// public method to render GameObject and start its Animation
	public void ShowArea(){
		//GetComponent<Renderer>().enabled = true;
		animButtonX.SetBool("StartAnimationBool", true); //start animation by setting the transition with StartAnimationBool in AreaAnimationController
		objectVisible = true;	// in Update() this variable sets the component Renderer continously to true or false
	}

	// public method to hide GameObject and start its Animation
	public void HideArea(){
		//gameObject.GetComponent<Renderer>().enabled = false;
		objectVisible = false;	// in Update() this variable sets the component Renderer continously to true or false
	}

	// Update is called once per frame
	void Update () {
		// reset transition with bool value StartAnimationBool for ensuring to rest in AnimationReady state after running through animation loop for one time
		if( animButtonX.GetCurrentAnimatorStateInfo(0).IsName("AreaAnimationFinished")){
			animButtonX.SetBool("StartAnimationBool", false);
		}
		// Show or Hide GameObject by setting Component Renderer
		if (objectVisible == false) gameObject.GetComponent<Renderer>().enabled = false;
		else gameObject.GetComponent<Renderer>().enabled = true;
	}
}
