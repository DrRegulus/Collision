using UnityEngine;
using System.Collections;

public class GetBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Get current background image based on level name
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		Sprite spr = Resources.Load<Sprite>("Backgrounds/" + Application.loadedLevelName);

		//print (Application.loadedLevelName);

		if (spr == null)
		{
			spr = Resources.Load<Sprite>("Backgrounds/Level1");
		}

		sr.sprite = spr;
	}
}
