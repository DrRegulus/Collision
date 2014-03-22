using UnityEngine;
using System.Collections;
using System;

public class Elevator : MonoBehaviour {

	public Transform top;
	public Transform bottom;

	public bool powered = false;
	public float moveDir = -1.0f;
	public float moveSpeed = .05f;

	public bool checkPowered = false;
	public float checkMoveDir = -1.0f;
	public float checkMoveSpeed = .05f;
	public Vector3 CheckPointPosition;

	Transform elevator;
	Rigidbody2D elevatorRig;


	// Use this for initialization
	void Start () {

		//set up event handlers for CheckPoints and Resets
		AliverController aliver = GameObject.Find("Aliver1").GetComponent<AliverController>();
		aliver.CheckPoint += new AliverController.CheckPointEventHandler(CheckPointReached);
		aliver.Reset += new AliverController.ResetEventHandler(ResetToCheckPoint);

		elevator = transform.FindChild("elevatorPlatform");
		elevatorRig = elevator.rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		if (powered) {
			elevatorRig.transform.Translate(0, moveDir * moveSpeed, 0);
			
			if ((moveDir > 0 && elevatorRig.transform.position.y >= top.position.y)
			    || (moveDir < 0 && elevatorRig.transform.position.y <= bottom.position.y)) {
				moveDir *= -1;
				powered = false;
			}
		}
	}

	public void CheckPointReached(object sender, EventArgs e){
		
		checkPowered = powered;
		checkMoveDir = moveDir;
		checkMoveSpeed = moveSpeed;
		
		CheckPointPosition = new Vector3(elevator.position.x, elevator.position.y, elevator.position.z);
	}
	
	public void ResetToCheckPoint(object sender, EventArgs e){
		
		powered = checkPowered;
		moveDir = checkMoveDir;
		moveSpeed = checkMoveSpeed;
		
		elevator.position = CheckPointPosition;
	}

	public void SetCheckPoint(){
		
		checkPowered = powered;
		checkMoveDir = moveDir;
		checkMoveSpeed = moveSpeed;
		
		CheckPointPosition = new Vector3(elevator.position.x, elevator.position.y, elevator.position.z);
	}
	
	public void ResetToCheckPoint(){
		
		powered = checkPowered;
		moveDir = checkMoveDir;
		moveSpeed = checkMoveSpeed;
		
		elevator.position = CheckPointPosition;
	}


	public void Activate()
	{
		powered = true;
	}
}
