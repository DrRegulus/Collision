using UnityEngine;
using System.Collections;

public class SteamValve : MonoBehaviour {

	private bool locked = false;
	public bool debug = false;
	//public Animator valveAnim;
	//public Animator steamAnim;
	
	// Use this for initialization
	void Start () {
		//valveAnim = GetComponent<Animator>();
		//steamAnim = transform.parent.GetComponentInChildren<Animator> ();
	}

	void Update()
	{
		if(!locked && debug)
		{
			StartCoroutine (TurnValveCoRoutine ());
			locked = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(!locked)
		{
			//valveAnim.SetBool ("Turning", true);
			//steamAnim.SetBool ("On", true);
			StartCoroutine (TurnValveCoRoutine ());
			locked = true;
		}
	}
	
	IEnumerator TurnValveCoRoutine()
	{
		yield return new WaitForSeconds (1);
		Destroy (transform.parent.FindChild ("Steam").gameObject);
		//valveAnim.SetBool ("Turning", false);
		//steamAnim.SetBool ("Off", false);
	}
}
