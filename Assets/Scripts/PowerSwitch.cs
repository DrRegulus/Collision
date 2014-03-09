using UnityEngine;
using System.Collections;

public class PowerSwitch : MonoBehaviour {
	
	public float secondsDelayed = 3f;
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
		yield return new WaitForSeconds (secondsDelayed);
		transform.parent.GetComponent<Maglev> ().Activate ();
		yield return new WaitForSeconds (5);
		anim.SetBool ("Power", false);
	}
}
