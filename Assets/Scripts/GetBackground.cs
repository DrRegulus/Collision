using UnityEngine;
using System.Collections;

public class GetBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		Sprite spr = Resources.Load<Sprite>("Backgrounds/background_level" + Application.loadedLevel);

		if (spr == null)
		{
			spr = Resources.Load<Sprite>("Backgrounds/debugDefault");
		}

		sr.sprite = spr;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
