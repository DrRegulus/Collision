using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().Play ();
	}
}
