//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class Textbox : MonoBehaviour
{
	string NPC;
	string speech;
	public GameObject myNPC;
	bool displayText;
	public GUIStyle myStyle;
	int counter;
	int lastStop;
	int currentIndex;
	public GameObject target;


	void TheStart(string name){
		NPC = name;
		//print (NPC);
		displayText = false;
		target = GameObject.FindWithTag ("Player");
	}

	void Update(){
		/*if (Vector3.Distance (target.transform.position, transform.position) > 10) {
			print ("Destroying Textbox from proximity");
			//print (target.transform.position.x);
			//print (transform.position.x);
			//print (Vector3.Distance (target.transform.position, transform.position));
			Destroy (gameObject); 
			//renderer.enabled = false;
		}*/
	}

	void TextDisplay(int num){
		XmlDocument textFile = new XmlDocument ();
		textFile.Load (Application.dataPath + "/Dialogue.xml");
		
		
		if (textFile == null) {
				//TODO: Display File Not Found Error
				//print ("Text not found");
				return;
		}
		else {
			//print ("Text found");
		}


		//XML READING

		foreach (XmlNode textXML in textFile.SelectNodes("dialogue//"+NPC)) {
			speech = textXML.SelectSingleNode("speech" + num.ToString()).InnerText;
			speech += "\n\nPress 'E' to advance.";
				//print (speech);
			displayText = true;

			//speechNumber = int.Parse(textXML.SelectSingleNode
			//myTest = textXML.SelectSingleNode ("s").InnerText;
			//print (myTest);
			//print (textXML.InnerText);

			
		}
	}

	void OnGUI(){
		myStyle.normal.textColor = Color.black;
		myStyle.fontSize = 20;
		counter = 0;
		lastStop = 0;
		currentIndex = 0;
		string mySpeech = "";
		char[] splitCriteria = {'\t'};
		char[] speechChars = speech.ToCharArray ();

		/*foreach (char letter in speechChars) {
			counter ++;
			currentIndex ++;
			if(counter >= 55 && letter == ' '){
				//print (speechChars.Length);
				string line = new String(speechChars, lastStop, currentIndex-lastStop);
				mySpeech += line + "\n";
				counter = 0;
				lastStop = currentIndex;
			}
			if(currentIndex == speechChars.Length){
				//print (lastStop);
				//print (speechChars.Length);
				string line = new String(speechChars, lastStop, currentIndex - lastStop);
				//print (line);
				mySpeech += line + "\n";
				break;
			}
		}*/
		//print (speechChars[12]);
		/*string[] speechSplit = speech.Split (splitCriteria);
		string mySpeech = "";
		foreach (string line in speechSplit) {
			mySpeech += "\n" + line;
		}*/
		//string Myspeech = speechSplit[0] + "\n" + speechSplit[1];
		//foreach (string word in speechSplit) {
			
		//}
		//print (speechSplit [0]);
		//print ("ongui");
		//if (displayText == true) {
		myStyle.fontSize = 15;
		GUI.Box(new Rect (Screen.width/6, Screen.height/12, Screen.width/3, Screen.height/3), speech, myStyle);
		GUI.contentColor = Color.black;
		//GUI.Label (new Rect(Screen.width/(float)1.25, Screen.height/(float)3.6, Screen.width, Screen.height/(float)3), "Press 'E' to Continue");
		//GUI.contentColor = Color.black;
		//GUI.Label (new Rect (100, 100, 100, 200), speech);
		//}
	}
}



