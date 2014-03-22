using UnityEngine;
using System.Collections;
using System;

public class Rotator : MonoBehaviour {

	public float rotationSpeed = .5f;
	public float checkRotationSpeed;
	public Vector3 checkPosition;
	public Quaternion checkRotation;


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


	void FixedUpdate()
	{
		transform.Rotate (0, 0, rotationSpeed);
	}
	
	
	public void CheckPointReached(object sender, EventArgs e){
		
		checkRotationSpeed = rotationSpeed;

		checkPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		checkRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
	}
	
	public void ResetToCheckPoint(object sender, EventArgs e){

		rotationSpeed = checkRotationSpeed;

		transform.position = checkPosition;
		transform.rotation = checkRotation;
		
	}


}
