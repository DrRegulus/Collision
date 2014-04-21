using UnityEngine;
using System.Collections;
using System;

public class MovingPlatform : MonoBehaviour {

	public Transform aliver;
	private Vector3 checkPosition;

	// Use this for initialization
	void Start () {
		//set up event handlers for CheckPoints and Resets
		AliverController aliver = GameObject.Find("Aliver").GetComponent<AliverController>();
		aliver.CheckPoint += new AliverController.CheckPointEventHandler(CheckPointReached);
		aliver.Reset += new AliverController.ResetEventHandler(ResetToCheckPoint);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		aliver = col.transform;
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if(aliver != null)
		{
			aliver.GetComponent<AliverController>().SetParentVelocity(new Vector2(0, 0));	
			aliver = null;
		}
	}


	public void CheckPointReached(object sender, EventArgs e){
		
		checkPosition = transform.position;
		
	}
	
	public void ResetToCheckPoint(object sender, EventArgs e){
		
		transform.position = checkPosition;
		
	}
}
