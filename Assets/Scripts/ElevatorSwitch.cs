using UnityEngine;
using System.Collections;

public class ElevatorSwitch : MonoBehaviour {

	public Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		anim.SetBool ("Power", true);
		StartCoroutine (FlipSwitchCoRoutine ());
	}
	
	IEnumerator FlipSwitchCoRoutine()
	{
		transform.parent.parent.GetComponent<Elevator> ().Activate ();
		yield return new WaitForSeconds (2);
		anim.SetBool ("Power", false);
	}
}
