using UnityEngine;
using System.Collections;

public class GetBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		Sprite spr = Resources.Load<Sprite>("Backgrounds/background_level" + Application.loadedLevel);

		if(Application.loadedLevelName == "Tutorial")
			spr = Resources.Load<Sprite>("Backgrounds/background_level1");

		if (spr == null)
		{
			//spr = Resources.Load<Sprite>("Backgrounds/debugDefault");
			spr = null;
		}

		sr.sprite = spr;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
