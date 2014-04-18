using UnityEngine;
using System.Collections;
using System;

public class Machine : MonoBehaviour {

	public bool powered = false;
	public float moveDir = 1f;
	protected float moveSpeed = 5f;

	protected bool checkPowered;
	protected float checkMoveDir;
	protected float checkMoveSpeed;

	// Use this for initialization
	void Start () {
		checkMoveDir = moveDir;
		checkMoveSpeed = moveSpeed;
		checkPowered = powered;
		
		//set up event handlers for CheckPoints and Resets
		AliverController aliver = GameObject.Find("Aliver").GetComponent<AliverController>();
		aliver.CheckPoint += new AliverController.CheckPointEventHandler(CheckPointReached);
		aliver.Reset += new AliverController.ResetEventHandler(ResetToCheckPoint);
	}

	public void Activate()
	{
		powered = true;
	}

	public void Deactivate()
	{
		powered = false;
		moveDir *= -1;
	}

	public void CheckPointReached(object sender, EventArgs e){
		
		checkPowered = powered;
		checkMoveDir = moveDir;
		checkMoveSpeed = moveSpeed;
		
	}
	
	public void ResetToCheckPoint(object sender, EventArgs e){
		
		powered = checkPowered;
		moveDir = checkMoveDir;
		moveSpeed = checkMoveSpeed;
		
	}
}
