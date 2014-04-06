using UnityEngine;
using System.Collections;
using System;

public class Maglev : MonoBehaviour {

	public bool powered = false, checkPowered = false;
	public float moveDir = 1f, checkMovDir = 1f;
	public float moveSpeed = 0.1f, checkMoveSpeed = 0.1f;

	public Transform leftBorder;
	public Transform rightBorder;
	public Transform leftEdge;
	public Transform rightEdge;

	protected Transform maglev;

	// Use this for initialization
	void Start () {
		maglev = transform.FindChild("platform_block");

		//set up event handlers for CheckPoints and Resets
		AliverController aliver = GameObject.Find("Aliver").GetComponent<AliverController>();
		aliver.CheckPoint += new AliverController.CheckPointEventHandler(CheckPointReached);
		aliver.Reset += new AliverController.ResetEventHandler(ResetToCheckPoint);

	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		if (powered) {

			maglev.Translate(moveSpeed * moveDir, 0 , 0);
			
			if ((moveDir > 0 && rightBorder.position.x >= rightEdge.position.x)
			    || (moveDir < 0 && leftBorder.position.x <= leftEdge.position.x)) {
				moveDir *= -1;
				powered = false;
				Transform Aliver = transform.FindChild("Aliver");
				if(Aliver != null)
					Aliver.parent = null;
			}
		}
	}


	public void Activate()
	{
		powered = true;
	}


	public void CheckPointReached(object sender, EventArgs e){

		checkPowered = powered;
		checkMovDir = moveDir;
		checkMoveSpeed = moveSpeed;

	}
	
	public void ResetToCheckPoint(object sender, EventArgs e){

		powered = checkPowered;
		moveDir = checkMovDir;
		moveSpeed = checkMoveSpeed;
		
	}

}
