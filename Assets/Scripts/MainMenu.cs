using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	//public Texture lv1texture; 
	//public Transform button1spot;
	public Texture[] Texlist; 
	public Transform[] buttonspots;


	void Start() 
	{
		GUI.backgroundColor = Color.clear;
		//GetComponent (SpriteRenderer);
	}

	/**
	 * Button
	 * A helper call to GUI.Button that takes in a Transform (which you place in the scene), width and height, and a texture.
	 * This serves to center the button on the Transform instead of having to worry about the button's upper-left being the Transform, and how that all looks in the scene editor.
	 * You will need to add public Texture, public Transform to the top of the class, then in Main Menu's GUI object, set the script's objects there.
	 * 
	 */
	bool Button (Transform worldObject, int width, int height, Texture texture)
	{
		Vector3 button1pos = Camera.main.WorldToScreenPoint (worldObject.position);
		Vector3 zero = Camera.main.WorldToScreenPoint (new Vector3 (0, 0, 0));//For some reason, the button was mirrored across the Y axis, so having the inscene object in the top half would put the button in the bottom half. I couldn't figure it out, but this workaround works.
		button1pos.y = (zero.y - button1pos.y) + zero.y;//The center dot of the object will be the upper-left of the button.

		return GUI.Button (new Rect (button1pos.x - width / 2, button1pos.y - height / 2, width, height), "");
	}

	void OnGUI()
	{

		GUI.backgroundColor = Color.clear;

		/*Debug.Log ("button1spot x" + button1spot.position.x + "," + button1spot.position.y);
		GUI.BeginGroup (new Rect (button1spot.position.x, button1spot.position.y, 100, 100));
		GUI.Button (new Rect (button1spot.position.x, button1spot.position.y, 40, 80), "Test");
		GUI.EndGroup ();*/

		//Transform t = new Vector3(button1spot.position.x,-1*button1spot.position.y,button1spot.position.z);
		//Transform t = new Transform( //new Vector3(1,2,3);
		Transform button1spot = buttonspots [0];

		Debug.Log ("button1spot x" + button1spot.position.x + "," + button1spot.position.y);
		//Debug.Log ("button1pos x" + button1pos.x + "," + button1pos.y);
		//if(GUI.Button(new Rect(button1pos.x,button1pos.y,148,75), lv1texture)) {
		//if(Button(button1spot,148,75,lv1texture)) {
		if(Button(buttonspots[0],74,38,null)) {
			Application.LoadLevel("Tutorial");
		}
		if(Button(buttonspots[1],74,38,null)) {
			Application.LoadLevel("Comic1");
		}
		if(Button(buttonspots[2],74,38,null)) {
			Application.Quit();
		}

		int scrwid = Screen.width;
		//int scrhei = Screen.currentResolution.height;
		int scrhei = Screen.height;
		//Screen.height
		//int menuheight = (int)(scrhei  * 1960f / 2100f);
		int menuheight = (int)(scrhei  * 2000f / 2100f);
		Debug.Log ("Screen width, height, menuheight:" + scrwid + "," + scrhei + "," + menuheight);
		//GUI.BeginGroup (new Rect (390, 568, 300, 100));
		/*GUI.BeginGroup (new Rect (0, menuheight, scrwid,100));
		GUI.Box (new Rect (0, 0, scrwid, 100), "");
		//if(GUI.Button(new Rect(10,10,80,20), "Level 1")) {
		/*if(GUI.Button(new Rect(8,0,64,28), lv1texture)) {
			Application.LoadLevel("level1");
		}

		if(GUI.Button(new Rect(10,40,80,20), "Level 2")) {
			Application.LoadLevel("level2");
		}

		if(GUI.Button(new Rect(10,70,80,20), "Quit")) {
			Application.Quit ();
		}

		GUI.EndGroup ();*/
	}
}
