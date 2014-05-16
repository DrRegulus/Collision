using UnityEngine;
using System.Collections;

public class LevelSplash : MonoBehaviour {

	public Texture nextBtn;
	public Texture quitBtn;
	public GUIStyle panelStyle = new GUIStyle();
	
	void Start()
	{
		panelStyle.alignment = TextAnchor.MiddleCenter;
		panelStyle.stretchHeight = true;
		panelStyle.stretchWidth = true;
	}
	
	void OnGUI()
	{
		GUI.backgroundColor = Color.clear;

		int btnWidth = Screen.width / 5;
		int btnHeight = Screen.width / 5;

		if(nextBtn != null)
		{
			if(GUI.Button(new Rect(Screen.width/3 - btnWidth/2, 3*Screen.height/4 - btnHeight/2,
			                       btnWidth, btnHeight), nextBtn))
			{
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		}

		if(GUI.Button(new Rect(2 * Screen.width/3 - btnWidth/2, 3*Screen.height/4 - btnHeight/2,
		                       btnWidth, btnHeight), quitBtn))
		{
			Application.LoadLevel("MainMenu");
		}
	}
}
