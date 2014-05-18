using UnityEngine;
using System.Collections;

public class EndCinematic : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		int end = PlayerPrefs.GetInt ("Ending");

		switch(end)
		{
			case -1:
				anim.Play("Bad");
			break;
			case 0:
				anim.Play ("Neutral");
			break;
			case 1:
				anim.Play("Good");
			break;
		}
		
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt ("Lives", 3);
	}
}
