/***********************************************************************************
	ControlScript
	- executes the states of a work process in a state machine by reading in a single state, make the connection to the Gameobjects and rendering them
	- controls all GameObjects (Buttons, texts, etc.)


************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript: MonoBehaviour {
	// 3D TextBoxes
	static int numberOf3DTextBoxes = 2;	//set number of 3D Text Boxes in the scene
	GameObject[] myARText = new GameObject[numberOf3DTextBoxes];

	public Transform[] parentOf3DText = new Transform[numberOf3DTextBoxes];	//empty transform / game Objects that containt the 3DTextes generated out of the prefab ARText
	
	public GameObject ARTextPrefab;

	ShowAndHide3DTextClass[] myButtonShowAndHide3DTextClass = new ShowAndHide3DTextClass[numberOf3DTextBoxes]; //class ShowAndHide3DTextClass of gameObject 3DText_X to be able to set ARText visible

	StateContentScript myStateContentScript;	//define variable myState from type of script CollectionOfStateScripts to use it in this class

	// Definition of Buttons
	// virtual objects as rectangular buttons, wheel button, etc. will be assigned to a number by pulling them in in that order to the script gameObject space
	// Buttons 0-2: virtualObject Button
	// Button 3: WheelButton
	static int numberOfButtons = 4; // Number of used buttons has to be declared here

	// Array of public GameObjects to contain all virtual elements as buttons, wheel buttons, etc.
	public GameObject[] button = new GameObject[numberOfButtons];
	
	// Animator for each Object
	Animator[] animButton = new Animator[numberOfButtons];		//Animator components of for all virtual Objects as Button, WheelButton, etc.
	bool[] buttonSetToVisible = new bool[numberOfButtons];		//Animator components of for all virtual Objects as Button, WheelButton, etc.
	string[] button3DTextContent = new string[numberOfButtons];
	int[] buttonNumberOf3DText = new int[numberOfButtons];

	string wrappedText;	// contains the wrapped text of the present text of state

	// 3DTextBox Size: Set scaling factor
	float scalingFactorForTextBox = 0.5f;	//general adjustment of Textboxes

	int stateNumber = 0;  // initial number of present state to be rendered

	bool renderState = false;  // initial state for rendering; on change of this bool variable, the state will be rendered once

	bool[] buttonAnimationRunning = new bool[numberOfButtons];
	bool[] buttonAnimationStarted = new bool[numberOfButtons];
	bool allAnimationsStopped = true;

	// Initialization
	void Start () {
		//Create 3DText Boxes
		for (int x=0; x<numberOf3DTextBoxes; ++x){
			Initialize3DTextBoxes(x);
			myButtonShowAndHide3DTextClass[x] = parentOf3DText[x].GetComponent<ShowAndHide3DTextClass>(); // assign script "ShowAndHide3DTextClass" to array of myButtonShowAndHide3DTextClass
		}
		// set visibility of all buttons to false at start
		for (int x=0; x<numberOfButtons; ++x){
			buttonSetToVisible[x] = false;
			if ( button[x].GetComponent<Animator>()){
				animButton[x] = button[x].GetComponent<Animator>();  // if button x has a animator component read it in
			}
			buttonAnimationRunning[x] = false; // buttons and 3D Texts not running initially
			buttonAnimationStarted[x] = false; // buttons and 3D Texts not running initially
		}

		// Get Script Component from script "CollectionOfStateScripts"
		myStateContentScript = GameObject.Find("ControlObject").GetComponent<StateContentScript>();

	}

	/* ************************************************************************
		Update ()
		- reads in state from StateContentScript and renders state
		- includes control for test purposes
	***************************************************************************/
	void Update () {
		// Render the state by reading in state variables and display virtual objects at new position and Scale
		if(renderState){
			// Hide all objects before rendering the new one
			// Hide Buttons
			for (int i=0; i<numberOfButtons; ++i){
				button[i].GetComponent<AnimationAreaObjectsClass>().HideArea();
				buttonSetToVisible[i] = false;
			}
			// Hide TextBoxes
			for (int i=0; i<numberOf3DTextBoxes; ++i){
				parentOf3DText[i].gameObject.SetActive(false);
				myButtonShowAndHide3DTextClass[i].text3DVisible = false;
			}
			// Set allAnimationsStopped to false, as Animations are about to start
			allAnimationsStopped = false;

			// Loop over amount of VirtualObjects (respectively buttons) to be rendered in state
			for (int i=0; i<myStateContentScript.myStateScript[stateNumber].usedNumberOfVirtualObjects; ++i){
				
				// New Position of virtualObject/Button assigned? Change position of parent
				if (myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].newPosition){
					button[myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].numberOfButton].transform.parent.localPosition = 
												new Vector3 (myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].positionX, 
															myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].positionY, 
															myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].positionZ);
				}

				// New Scale of virtualObject/Button assigned? Change Scale of parent
				if (myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].newScale){
					button[myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].numberOfButton].transform.parent.localScale = 
												new Vector3 (myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].scaleX, 
															myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].scaleY, 
															myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].scaleZ);
				}

				// Show button with animation
				button[myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].numberOfButton].GetComponent<AnimationAreaObjectsClass>().ShowArea();
				buttonAnimationRunning[myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].numberOfButton] = true;

				// set buttonSetToVisible to true
				if (myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].textAssignedToVirtualObject == true){
					buttonSetToVisible[myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].numberOfButton] = true; // mark the button with "buttonSetToVisible" that 3DText is assigned to
					button3DTextContent[myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].numberOfButton] = myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].textContent; // assign text to button
					buttonNumberOf3DText[myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].numberOfButton] = myStateContentScript.myStateScript[stateNumber].myVirtualObject[i].numberOf3DText; // assign number of 3D text to button
				}
			}
			

			renderState = false;  // render only once
		}

		// Show 3DText associated with Button after button animation is in state finished
		for (int i=0; i<numberOfButtons; ++i){
			// Check Animation for buttons with 3D Text
			if ((buttonSetToVisible[i] == true) && animButton[i].GetCurrentAnimatorStateInfo(0).IsName("AreaAnimationFinished")){
				showAndPosition3DText(button3DTextContent[i], button[i], myButtonShowAndHide3DTextClass[buttonNumberOf3DText[i]], parentOf3DText[buttonNumberOf3DText[i]], buttonNumberOf3DText[i]);
			}
			// Check for each button if animation is finished and animation state returned to AreaAnimationReady
			//if ((allAnimationsStopped == false) && animButton[i].GetCurrentAnimatorStateInfo(0).IsName("AreaAnimationReady")){	// Animation of button returned to State AreaAnimationReady
			/*if ((allAnimationsStopped == false) && animButton[i].GetCurrentAnimatorStateInfo(0).IsName("AreaAnimationFinished")){	// Animation of button returned to State AreaAnimationReady
								buttonAnimationRunning[i] = false;
			}*/
			if ((allAnimationsStopped == false) && animButton[i].GetCurrentAnimatorStateInfo(0).IsName("AreaAnimation")){	// Animation of button is in state AreaAnimation
					buttonAnimationStarted[i] = true;
			}
			if ((allAnimationsStopped == false) && (buttonAnimationStarted[i] == true) && animButton[i].GetCurrentAnimatorStateInfo(0).IsName("AreaAnimationReady")){	// Animation of button returned to State AreaAnimationReady
								buttonAnimationStarted[i] = false;
								buttonAnimationRunning[i] = false;
			}
		}

		// Start application with home screen by pressing key "s"
		if(Input.GetKey("s") && (allAnimationsStopped==true)){
			searchAndSelectStateByStateIDName("ID_Start");		// Find Home State and set according state number
		}

		// Check if all animations are finished
		if (allAnimationsStopped == false){
			allAnimationsStopped = true;	// set value to true first
			for (int i=0; i<numberOfButtons; ++i){
				if (buttonAnimationRunning[i] == true){
					allAnimationsStopped = false; // as long as at least one of the animations of one of the buttons is running, allAnimationsStopped is set to false again
				}
			}
		}


		// Control application with "a": previous state and "d": next state
		// Next state
		if(Input.GetKey("a") && (allAnimationsStopped==true)){
			searchAndSelectStateByStateIDName(myStateContentScript.myStateScript[stateNumber].previousStateIDName);			
		}
		// previous state
		if(Input.GetKey("d") && (allAnimationsStopped==true)){
			searchAndSelectStateByStateIDName(myStateContentScript.myStateScript[stateNumber].nextStateIDName);			
		}
		

		// Change 3D Text manually for testing purpose => removed later
		if(Input.GetKey("q")){
			parentOf3DText[0].gameObject.SetActive(true);
			wrappedText = myARText[0].GetComponent<ARTextScript>().WrapTxt("Hallo"); // Accessing the script functions we need to Get the Component
			myARText[0].GetComponent<ARTextScript>().newTextForBox(wrappedText);
			Debug.Log("a gedrückt");
		}
		if(Input.GetKey("w")){
			parentOf3DText[1].gameObject.SetActive(true);
			wrappedText = myARText[1].GetComponent<ARTextScript>().WrapTxt("Hallo das ist ein langer Text");
			myARText[1].GetComponent<ARTextScript>().newTextForBox(wrappedText);
		}
		if(Input.GetKey("e")){
			parentOf3DText[0].gameObject.SetActive(true);
			wrappedText = myARText[0].GetComponent<ARTextScript>().WrapTxt("Hallo das ist ein absolut super langer und guter Text");
			myARText[0].GetComponent<ARTextScript>().newTextForBox(wrappedText);
		}
		if(Input.GetKey("r")){
			parentOf3DText[1].gameObject.SetActive(true);
			wrappedText = myARText[1].GetComponent<ARTextScript>().WrapTxt("Hallo das ist ein absolut super langer und guter Text. Und er ist noch viel länger als gedacht. Er geht über mehrere Zeilen.");
			myARText[1].GetComponent<ARTextScript>().newTextForBox(wrappedText);
		}		
	}

	/* ************************************************************************
		Initialize3DTextBoxes(int i)
		- Creates a defined number of 3DText elements out of prefab ARText
		- append ARText to predefined GameObjects 
		- stores new children elements in Array myARText[i]

	***************************************************************************/
	void Initialize3DTextBoxes(int i){
		myARText[i] = Instantiate(ARTextPrefab, new Vector3(0, 0, 0) , Quaternion.identity);
		// Append ARText to each parent
		if (i == 0) myARText[i].transform.parent = parentOf3DText[0];
		else if (i == 1) myARText[i].transform.parent = parentOf3DText[1];
		else Debug.Log("Number of 3D Text fields wrong ");

		// Adding the Script to ARText (out of the Prefab)
		myARText[i].AddComponent<ARTextScript>();
		// after adding myARText to the parent, the local position needs to be set to 0 in order to have the text placed, where the parent is.
		myARText[i].transform.localPosition = new Vector3 (0, 0, 0);
		// after adding myARText to the parent, the local Scale needs to be set to 1 in order to have a defined starting value
		myARText[i].transform.localScale = new Vector3 (scalingFactorForTextBox, scalingFactorForTextBox, scalingFactorForTextBox);
		Debug.Log("Initialisierung 3DTextBoxes");
	}

	/* ************************************************************************
		showAndPosition3DText
		- Show 3DText at right position with right Text
		- position 3D Text with offset to selected Gameobject
		- wrap text in box
		- Display text
		- Inputs: 
			string text:	text to be displayed
			GameObject originGameObjectButtonElement : GameObject, that the 3D Text shall be connected to
			ShowAndHide3DTextClass mySelected3DText: class/Script that contains the boolsche variable to set 3D Text to visible
			Transform myParentOf3DText: parent element of 3D Text
			int ARTextArrayNumber: number of ARText element within array of 3D text elements

	***************************************************************************/
	void showAndPosition3DText(string text, GameObject originGameObjectButtonElement, ShowAndHide3DTextClass mySelected3DText, Transform myParentOf3DText, int ARTextArrayNumber){
		// Set position of the 3DText by moving the 3D Text parent; define new position derived by GameObject button parent position with offset
		mySelected3DText.position3DText(originGameObjectButtonElement.transform.parent.localPosition.x+0.5f, 0.06f, originGameObjectButtonElement.transform.parent.localPosition.z+0.3f);	

		// Define and wrap the 3D Text
		myParentOf3DText.gameObject.SetActive(true);
		wrappedText = myARText[ARTextArrayNumber].GetComponent<ARTextScript>().WrapTxt(text); // Accessing the script functions we need to Get the Component
		myARText[ARTextArrayNumber].GetComponent<ARTextScript>().newTextForBox(wrappedText);

		// Show 3DText
		mySelected3DText.text3DVisible = true;
	}

	/* ************************************************************************
		searchAndSelectStateByStateIDName
		- Find state by name and set according stateNumber
		- Inputs: string stateName: NameID of searched state

	***************************************************************************/	
	void searchAndSelectStateByStateIDName(string newStateName){
		// Find state by name and set according stateNumber
		for (int x=0; x < myStateContentScript.myMaxNumberOfStates; ++x){		// run through all states (max number is defined by maxNumberOfStates)
			if (myStateContentScript.myStateScript[x].thisStateIDName == newStateName){		// search for state with name
				stateNumber = x;												// assign the array number to stateNumber
				renderState = true;
				Debug.Log(x);
			}
		}
	}
}
