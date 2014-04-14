using UnityEngine;
using System.Collections;

public class ComicPlayer : MonoBehaviour {

	public int lvlNum = 0;
	public Texture[] panels;
	public Texture nextBtn;
	public Texture prevBtn;
	public Texture beginBtn;
	public GUIStyle panelStyle = new GUIStyle();
	private int idx = 0;

	void Start()
	{
		panelStyle.fixedHeight = 4 * Screen.height / 5;
		panelStyle.fixedWidth = 4 * Screen.width / 5;
	}

	void OnGUI()
	{
		GUI.backgroundColor = Color.clear;
		GUI.Box (new Rect (Screen.width/10, Screen.height/10, 4*Screen.width/5, 4*Screen.height/5), panels [idx], panelStyle);

		if(idx < panels.Length - 1)
		{
			if(GUI.Button(new Rect(Screen.width - nextBtn.width, Screen.height/2 - nextBtn.height/2,
				                    nextBtn.width, nextBtn.height), nextBtn))
			{
				idx++;
			}
		}
		else if(GUI.Button(new Rect(Screen.width - beginBtn.width, Screen.height/2 - beginBtn.height/2,
		                            beginBtn.width, beginBtn.height), beginBtn))
		{
			Destroy(Camera.main.gameObject);
			Application.LoadLevelAdditive("level" + lvlNum);
			Destroy(gameObject);
		}

		//print (panels.Length);

		if(0 < idx && idx < panels.Length - 1)
		{
			if(GUI.Button(new Rect(0, Screen.height/2 - prevBtn.height/2,
			                       prevBtn.width, prevBtn.height), prevBtn))
			{
				idx--;
			}
		}
	}
}
