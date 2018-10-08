using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateDefinitionNamespace;


public class StateContentScript : MonoBehaviour {

	// Define maximum number of states
	static int maxNumberOfStates = 10;		// define max number of states here
	public int myMaxNumberOfStates = maxNumberOfStates;

	public StateDefinitionScript[] myStateScript = new StateDefinitionScript[maxNumberOfStates];	//define array of myStateScript from type of class StateDefinitionScript

	// States: Set all variables within states, derived from object StateDefintionScript
	void Start () {
		int presentStateNumber;		// number of presently used state
		int numberOfVirtualObjectsInState; // variable within State for the used number of virtualObjects
		int presentVirtualObjectNumber;		// number of presently used virtualObject
		int presentButtonNumberAssignedVirtualObject;		// number of presently used virtualObject
		int selectedTextNumber;		// number defines 3D Text element
		string text;	// contains 3D text

		// Initialize Array of States by calling constructor of class StateDefinitionScript
		for(int i=0; i<maxNumberOfStates; ++i){
			myStateScript[i] = new StateDefinitionScript();
		}

		/**********************************************************
			STATE 0
		************************************************************/
		// Initialize state 
		presentStateNumber=0;		// Set the number of state to be used in array
		
		// Assign state name, next state name and previous state name
		myStateScript[presentStateNumber].thisStateIDName = "ID_Start";
		myStateScript[presentStateNumber].nextStateIDName = "ID_state1";
		myStateScript[presentStateNumber].previousStateIDName = "ID_Start";


		// VirtualObject
		numberOfVirtualObjectsInState = 2;		//Define number of used retangles in this state
		InitializeVirtualObject(presentStateNumber, numberOfVirtualObjectsInState);  // Intialize: set all variables to false for VirtualObjects;  set number of VirtualObjects in the class for this state; parameters: presentStateNumber, max Number of VirtualObjects
		
		// VirtualObject 0
		presentVirtualObjectNumber=0;	
		presentButtonNumberAssignedVirtualObject = 1;

		AssginVirtualObjectToButton(presentStateNumber, presentVirtualObjectNumber, presentButtonNumberAssignedVirtualObject); 		// Assign virtualObject to button
		VirtualObjectPosition(presentStateNumber, presentVirtualObjectNumber, -0.5f, 0.06f, 1.33f); // new position with parameters: presentStateNumber, virtualObjectNr, positionX, positionY, positionZ		
		VirtualObjectScale(presentStateNumber, presentVirtualObjectNumber, 0.6f, 0.001f, 0.6f); // new Scale with parameters: presentStateNumber, virtualObjectNr, positionX, positionY, positionZ

		// Text
		selectedTextNumber = 0;
		text = "Text 0: Das ist mein neuer Text. Wir probieren es mal aus. Das ist ein guter Versuch. Super, dass es klappt.";
		AssignTextToButton(presentStateNumber, presentVirtualObjectNumber, selectedTextNumber, text); // assign 3D Text to button

		// VirtualObject 1
		presentVirtualObjectNumber=1;	
		presentButtonNumberAssignedVirtualObject = 0;
		AssginVirtualObjectToButton(presentStateNumber, presentVirtualObjectNumber, presentButtonNumberAssignedVirtualObject); 		// Assign virtualObject to button
		// no changes in position or scale


		/**********************************************************
			STATE 1
		************************************************************/
		// Initialize state 
		presentStateNumber=1;		// Set the number of state to be used in array
		
		// Assign state name, next state name and previous state name
		myStateScript[presentStateNumber].thisStateIDName = "ID_state1";
		myStateScript[presentStateNumber].nextStateIDName = "ID_state2";
		myStateScript[presentStateNumber].previousStateIDName = "ID_Start";

		// VirtualObject
		numberOfVirtualObjectsInState = 3;		//Define number of used retangles in this state
		InitializeVirtualObject(presentStateNumber, numberOfVirtualObjectsInState);  // Intialize: set all variables to false for VirtualObjects;  set number of VirtualObjects in the class for this state; parameters: presentStateNumber, max Number of VirtualObjects
		
		// VirtualObject 0
		presentVirtualObjectNumber=0;	
		presentButtonNumberAssignedVirtualObject = 1;
		AssginVirtualObjectToButton(presentStateNumber, presentVirtualObjectNumber, presentButtonNumberAssignedVirtualObject); 		// Assign virtualObject to button
		VirtualObjectPosition(presentStateNumber, presentVirtualObjectNumber, -1.5f, 0.06f, 1.33f); // new position with parameters: presentStateNumber, virtualObjectNr, positionX, positionY, positionZ		
		VirtualObjectScale(presentStateNumber, presentVirtualObjectNumber, 0.15f, 0.001f, 0.15f); // new Scale with parameters: presentStateNumber, virtualObjectNr, positionX, positionY, positionZ

		// VirtualObject 1
		presentVirtualObjectNumber=1;	
		presentButtonNumberAssignedVirtualObject = 2; // define to which button shall the virtualObject be assigned
		AssginVirtualObjectToButton(presentStateNumber, presentVirtualObjectNumber, presentButtonNumberAssignedVirtualObject); 		// Assign virtualObject to button
		// no changes in position or scale
		// Text
		selectedTextNumber = 0;
		text = "Text 1a: Das ist mein neuer Text. Und auch dieser Text ist neu!";
		AssignTextToButton(presentStateNumber, presentVirtualObjectNumber, selectedTextNumber, text); // assign 3D Text to button

		// VirtualObject 2: WheelButton
		presentVirtualObjectNumber=2;	
		presentButtonNumberAssignedVirtualObject = 3;
		AssginVirtualObjectToButton(presentStateNumber, presentVirtualObjectNumber, presentButtonNumberAssignedVirtualObject); 		// Assign virtualObject to button
		// Text
		selectedTextNumber = 1;
		text = "Text 1b: Bitte schalten Sie die Maschine ein. Drücken Sie das Drehrad 3s lang. Drehen sie es dann bis im Menü das Richtige erscheint.";
		AssignTextToButton(presentStateNumber, presentVirtualObjectNumber, selectedTextNumber, text); // assign 3D Text to button

		/**********************************************************
			STATE 2
		************************************************************/
		// Initialize state 
		presentStateNumber=2;		// Set the number of state to be used in array

		// Assign state name, next state name and previous state name
		myStateScript[presentStateNumber].thisStateIDName = "ID_state2";
		myStateScript[presentStateNumber].nextStateIDName = "ID_Start";
		myStateScript[presentStateNumber].previousStateIDName = "ID_state1";

		// VirtualObject
		numberOfVirtualObjectsInState = 2;		//Define number of used retangles in this state
		InitializeVirtualObject(presentStateNumber, numberOfVirtualObjectsInState);  // Intialize: set all variables to false for VirtualObjects;  set number of VirtualObjects in the class for this state; parameters: presentStateNumber, max Number of VirtualObjects
		
		// VirtualObject 0
		presentVirtualObjectNumber=0;	
		presentButtonNumberAssignedVirtualObject = 1;
		AssginVirtualObjectToButton(presentStateNumber, presentVirtualObjectNumber, presentButtonNumberAssignedVirtualObject); 		// Assign virtualObject to button
		VirtualObjectPosition(presentStateNumber, presentVirtualObjectNumber, 1f, 0.06f, 0.5f); // new position with parameters: presentStateNumber, virtualObjectNr, positionX, positionY, positionZ		
		VirtualObjectScale(presentStateNumber, presentVirtualObjectNumber, 0.15f, 0.001f, 0.15f); // new Scale with parameters: presentStateNumber, virtualObjectNr, positionX, positionY, positionZ

		// VirtualObject 1: WheelButton
		presentVirtualObjectNumber=1;	
		presentButtonNumberAssignedVirtualObject = 3;
		AssginVirtualObjectToButton(presentStateNumber, presentVirtualObjectNumber, presentButtonNumberAssignedVirtualObject); 		// Assign virtualObject to button
		

	}

	/**********************************************************
		VirtualObject 
	************************************************************/	
	void InitializeVirtualObject(int stateNr, int nrOfVirtualObjectsInState){  // parameters: presentStateNumber, max Number of VirtualObjects
		myStateScript[stateNr].usedNumberOfVirtualObjects = nrOfVirtualObjectsInState;  // set number of virtualObjects used in this state
		
		// set all variables to false for each virtualObject in this state;
		for(int i=0; i<nrOfVirtualObjectsInState; ++i){
			myStateScript[stateNr].myVirtualObject[i].newPosition = false;	
			myStateScript[stateNr].myVirtualObject[i].newScale = false;
			myStateScript[stateNr].myVirtualObject[i].textAssignedToVirtualObject = false;
		}
	}
	// Define to which gameobject button the virtualObject shall be assigned to (inheriting position, scale, color, etc.)
	void AssginVirtualObjectToButton(int stateNr, int virtualObjectNr, int buttonNr){
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].numberOfButton = buttonNr; // assign virtualObject to button gameobject
	}

	// set new Position in variables of class StateDefinitionScript
	void VirtualObjectPosition(int stateNr, int virtualObjectNr, float posX, float posY, float posZ){
		// Position
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].newPosition = true;
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].positionX = posX;
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].positionY = posY;
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].positionZ = posZ;
	}

	// set new Scale in variables of class StateDefinitionScript
	void VirtualObjectScale(int stateNr, int virtualObjectNr, float scaleX, float scaleY, float scaleZ){
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].newScale = true;
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].scaleX = scaleX;
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].scaleY = scaleY;
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].scaleZ = scaleZ;
	}
	
	// assign 3D text to the corresoponding button respectively virtualObject
	void AssignTextToButton(int stateNr, int virtualObjectNr,  int textNr, string txtContent){ // assign 3D Text to button
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].textAssignedToVirtualObject = true;
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].textContent = txtContent;
		myStateScript[stateNr].myVirtualObject[virtualObjectNr].numberOf3DText = textNr;
	}

}
