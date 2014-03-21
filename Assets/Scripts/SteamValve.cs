using UnityEngine;
using System.Collections;

public class SteamValve : MonoBehaviour {
	
	//public Animator valveAnim;
	//public Animator steamAnim;
	
	// Use this for initialization
	void Start () {
		//valveAnim = GetComponent<Animator>();
		//steamAnim = transform.parent.GetComponentInChildren<Animator> ();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		//valveAnim.SetBool ("Turning", true);
		//steamAnim.SetBool ("On", true);
		StartCoroutine (TurnValveCoRoutine ());
	}
	
	IEnumerator TurnValveCoRoutine()
	{
		yield return new WaitForSeconds (2);
		transform.parent.collider2D.enabled = false;
		//valveAnim.SetBool ("Turning", false);
		//steamAnim.SetBool ("Off", false);
	}
}
