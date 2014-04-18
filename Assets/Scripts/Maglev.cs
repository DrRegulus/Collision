using UnityEngine;
using System.Collections;
using System;

public class Maglev : Machine {
	
	public Transform leftEdge;
	public Transform rightEdge;
	public Transform maglev;

	/*// Use this for initialization
	void Start () {
		checkMoveDir = moveDir;
		checkMoveSpeed = moveSpeed;
		checkPowered = powered;

		//set up event handlers for CheckPoints and Resets
		AliverController aliver = GameObject.Find("Aliver").GetComponent<AliverController>();
		aliver.CheckPoint += new AliverController.CheckPointEventHandler(CheckPointReached);
		aliver.Reset += new AliverController.ResetEventHandler(ResetToCheckPoint);

	}*/

	void FixedUpdate()
	{
		if (powered) {

			maglev.rigidbody2D.velocity = new Vector2(moveDir * moveSpeed, 0);
			
			if ((moveDir > 0 && maglev.position.x >= rightEdge.position.x)
			    || (moveDir < 0 && maglev.position.x <= leftEdge.position.x)) {
				maglev.rigidbody2D.velocity = new Vector2(0, 0);
				Deactivate();
			}

			Transform aliver = maglev.GetComponent<MovingPlatform>().aliver;
			if(aliver != null)
			{
				aliver.GetComponent<AliverController>().SetParentVelocity(maglev.rigidbody2D.velocity);	
			}
		}
	}

}
