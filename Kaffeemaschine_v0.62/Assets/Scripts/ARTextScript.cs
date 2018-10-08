/************************************************************************************* 
	ARTextScript
	- Script organizes 3DText Boxes
	- generated and inherited from Prefab ARText by importing Prefab
	- scipt is attached to gameobject 3DText from Prefab, which is assigned to parent 3DText_X
	- sizes all elements from the 3DText Box: background, lines; resizes the original textBox from Prefab with size and scale (1, 1, 1)
	- contains defintion of parameters for sizing the 3D Text Box
	- has method for wrapping text to a defined width and height form text Box
**************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARTextScript : MonoBehaviour {
	public GameObject parentBox; //partentBox is expected to be ARText

	float oldSizeOfBoxInX = 1f;	//initial value for the size of the AR Text background in X
	float newSizeOfBoxInX;
	float deltaSizeOfBoxInX;

	float oldSizeOfBoxInZ = 1f;  //initial value for the size of the AR Text background in 
	float newSizeOfBoxInZ;
	float deltaSizeOfBoxInZ;

	//3D Text: Number of characters in one line
	int numberOfCharacters3DText = 30;
	//Wrap 3D Text: Number of lines after wrapping
	int numberOfLinesWrap3DText = 0;

	void Start () {
	}

	// text box is redimensioned according to new text
	public void newTextForBox(string t){
		string newText = t;
		
		int textLength = newText.Length;
		

		// Resize text box
		if (textLength > 0){
			// Find children objects under ARText
			GameObject backgroundBox = parentBox.transform.Find("Background").gameObject;
			GameObject borderTopBox = parentBox.transform.Find("BorderTop").gameObject;
			GameObject borderBottomBox = parentBox.transform.Find("BorderBottom").gameObject;
			GameObject borderLeftBox = parentBox.transform.Find("BorderLeft").gameObject;
			GameObject borderRightBox = parentBox.transform.Find("BorderRight").gameObject;
			GameObject textInBox = parentBox.transform.Find("Text").gameObject;

			float offset3DTextInX;

			// Calculate delta between new size of box, depending on text length/numberOfCharacters3DText plus static offset and old size of box
			// Define values for text box	
			float offsetWithinBox = 0.2f;	// border within Box on left and right side of text
			float offsetCharacter = 0.04f;	// space for each character in X
			float offsetHeightNumberOfLines = 0.035f;  // space for each line in Y
			float offsetTextPositionInZ = 0.08f;	// border within Box on top and bottom of text
			float offsetBorderTopBottomPositionInZ = 0.2f;

			// New Width of Box (in X): Calculation relative to former size as well as absolute
			newSizeOfBoxInX = ((float)numberOfCharacters3DText * offsetCharacter + offsetWithinBox);
			deltaSizeOfBoxInX = newSizeOfBoxInX - oldSizeOfBoxInX;
			oldSizeOfBoxInX = newSizeOfBoxInX;

			// New Height of Box (in Z): Calculation relative to former size as well as absolute
			newSizeOfBoxInZ = (offsetBorderTopBottomPositionInZ + (float) (1 + numberOfLinesWrap3DText) * offsetHeightNumberOfLines)*2;
			deltaSizeOfBoxInZ = newSizeOfBoxInZ - oldSizeOfBoxInZ;
			oldSizeOfBoxInZ = newSizeOfBoxInZ;

			if (backgroundBox != null){
				// Background & borders: Top, Bottom, Left, Right
				backgroundBox.transform.localScale += new Vector3(deltaSizeOfBoxInX, 0, deltaSizeOfBoxInZ);

				// BorderTop
				borderTopBox.transform.localScale += new Vector3(deltaSizeOfBoxInX, 0, 0);
				//borderTopBox.transform.position = new Vector3(borderTopBox.transform.position.x, borderTopBox.transform.position.y, scaleAdaptionOfImageTarget*(offsetBorderTopBottomPositionInZ + (float) (1 + numberOfLinesWrap3DText) * offsetHeightNumberOfLines));
				borderTopBox.transform.localPosition = new Vector3(0, 0, offsetBorderTopBottomPositionInZ + (float) (1 + numberOfLinesWrap3DText) * offsetHeightNumberOfLines);

				// BorderBottom
				borderBottomBox.transform.localScale += new Vector3(deltaSizeOfBoxInX, 0, 0);
				//borderBottomBox.transform.position = new Vector3(borderBottomBox.transform.position.x, borderBottomBox.transform.position.y, scaleAdaptionOfImageTarget*(-(offsetBorderTopBottomPositionInZ + (float) (1 + numberOfLinesWrap3DText) * offsetHeightNumberOfLines)));
				borderBottomBox.transform.localPosition = new Vector3(0, 0, -(offsetBorderTopBottomPositionInZ + (float) (1 + numberOfLinesWrap3DText) * offsetHeightNumberOfLines));

				// BorderLeft
				borderLeftBox.transform.localScale += new Vector3(deltaSizeOfBoxInZ, 0, 0); // as border is rotated by 90 degree around y, the x value needs to be scaled
				borderLeftBox.transform.localPosition = new Vector3((-(newSizeOfBoxInX)/2f), 0, 0);

				// BorderRight
				borderRightBox.transform.localScale += new Vector3(deltaSizeOfBoxInZ, 0, 0); // as border is rotated by 90 degree around y, the x value needs to be scaled
				borderRightBox.transform.localPosition = new Vector3((newSizeOfBoxInX/2f), 0, 0);

				// new position of 3D Text
				if (textLength < numberOfCharacters3DText){
					offset3DTextInX = (float)textLength * offsetCharacter / 2 * (-1);
				}
				else{
					offset3DTextInX = (float)numberOfCharacters3DText * offsetCharacter / 2 * (-1);
				}
				
				textInBox.GetComponent<TextMesh>().transform.localPosition = new Vector3(offset3DTextInX, 0, (offsetTextPositionInZ + (float)numberOfLinesWrap3DText * offsetHeightNumberOfLines));
				textInBox.GetComponent<TextMesh>().text = newText;
			}
			else{
				Debug.Log("No Child with the name Background found");
			}
		}
	}

	/************************************************************************************* 
		WrapTxt(string text)
		- Wraps a 3DText	
		- Returns text with line breaks
		- width is controlled by global Variable numberOfCharacters3DText
		- global variable numberOfLinesWrap3DText counts number of lines generated by wrapping
	**************************************************************************************/
	public string WrapTxt(string text){
		int width = numberOfCharacters3DText;
		var e=text.Length;
		var i=0;
		var lineBegin=0;
		var lineLength=0;
		var prevSeparatorPos=0;
		var wrappedText="";
		var separators=" ;:\n\t,.-()@#?";
		var cutOffSeparators=" \n\t";
		var cutOffSeparator=false;
		numberOfLinesWrap3DText = 0;
		while(i<e) {
			var c = text.Substring(i,1);
			i++;
			lineLength++;
			if(separators.IndexOf(c)>=0) {
				prevSeparatorPos=i;
				if(cutOffSeparators.IndexOf(c)>=0) cutOffSeparator=true;
				else cutOffSeparator=false;
			}
			if(lineLength>width) {
				if(prevSeparatorPos<=lineBegin) prevSeparatorPos=i-1;  // no separator found on entire line
				if(wrappedText.Length>0) wrappedText+="\n";
				if(cutOffSeparator) wrappedText+=text.Substring(lineBegin,prevSeparatorPos-lineBegin-1);
				else wrappedText+=text.Substring(lineBegin,prevSeparatorPos-lineBegin);
				i=prevSeparatorPos;
				lineBegin=i;
				lineLength=0;
				cutOffSeparator=false;
				numberOfLinesWrap3DText++;
			}
		}
		if(lineBegin<i-1) {
			if(prevSeparatorPos<=lineBegin) prevSeparatorPos=i;  // no separator found on entire line
			if(wrappedText.Length>0) wrappedText+="\n";
			wrappedText+=text.Substring(lineBegin,i-lineBegin);
		}
		return wrappedText;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
