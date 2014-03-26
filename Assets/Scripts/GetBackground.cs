using UnityEngine;
using System.Collections;

public class GetBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		Sprite spr = Resources.LoadAssetAtPath<Sprite>("Assets/Sprites/_Environment/Backgrounds/background_level" + Application.loadedLevel + ".png");

		if (spr == null)
		{
			spr = Resources.LoadAssetAtPath<Sprite>("Assets/Sprites/_Environment/Backgrounds/debugDefault.png");
		}

		sr.sprite = spr;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
