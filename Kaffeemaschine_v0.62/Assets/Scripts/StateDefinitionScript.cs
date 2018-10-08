using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateDefinitionNamespace{
	public class StateDefinitionScript {
		// Define max amount of parallel virtual Objects in one state at same time 
		static int maxNumberOfVirtualObjectsInOneState = 10;
		public virtualObject[] myVirtualObject = new virtualObject[maxNumberOfVirtualObjectsInOneState]; // array of struct virtualObjects needs to be initialized and allocated => ???Check if it is possible also in stateContentScript??? 


		// Properties of state
		public string thisStateIDName = "";
		public string nextStateIDName = "";	// up to now only one descender state, needs to be changed later to several with an array
		public string previousStateIDName = "";

		public int usedNumberOfVirtualObjects=0;  // number of virtualObjects used in a stage initialized with 0; real number will be assigned in stateContentScript

		// VirtualObject
		public struct virtualObject{
			// Number of button that the virtualObject shall be assigned to 
			public int numberOfButton;

			// coordinates of virtualObject 
			public bool newPosition; // new coordinate to be used
			public float positionX;
			public float positionY;
			public float positionZ;
	
			// scale of virtualObject 
			public bool newScale; // new scale to be used
			public float scaleX;
			public float scaleY;
			public float scaleZ;

			// color of virtualObject
			/*
			public struct myColor{
				// set initial color to light green with transparency
				public byte red = 30;
				public byte green = 233;
				public byte blue = 42;
				public int alpha = 107; 
			}*/

			// 3DText
			public bool textAssignedToVirtualObject;
			public string textContent;
			public int numberOf3DText;
		}
	}
}