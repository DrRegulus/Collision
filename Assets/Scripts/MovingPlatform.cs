using UnityEngine;
using System.Collections;
using System;

public class MovingPlatform : MonoBehaviour {


	public Vector3 checkPosition;

	// Use this for initialization
	void Start () {
	
		//set up event handlers for CheckPoints and Resets
		AliverController aliver = GameObject.Find("Aliver1").GetComponent<AliverController>();
		aliver.CheckPoint += new AliverController.CheckPointEventHandler(CheckPointReached);
		aliver.Reset += new AliverController.ResetEventHandler(ResetToCheckPoint);


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		col.transform.parent = transform;
	}


	public void CheckPointReached(object sender, EventArgs e){
		
		checkPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
	}
	
	public void ResetToCheckPoint(object sender, EventArgs e){
		
		transform.position = checkPosition;
		
	}

}
