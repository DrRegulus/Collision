using UnityEngine;
using System.Collections;

public class SetCheckpoint : MonoBehaviour {

	public Animator anim;

	private SetCheckpoint[] checkpoints;
	private AliverController aliver;

	// Use this for initialization
	void Start () {
		checkpoints = FindObjectsOfType<SetCheckpoint> ();
		aliver = FindObjectOfType<AliverController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			foreach(SetCheckpoint chk in checkpoints)
			{
				if(this != chk)
					chk.anim.SetBool("Activated", false);
			}

			anim.SetBool("Activated", true);
			aliver.CheckPointReached(this, null);
		}
	}
}
