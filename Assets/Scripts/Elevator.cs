using UnityEngine;
using System.Collections;
using System;

public class Elevator : Machine {

	public Transform top;
	public Transform bottom;
	public Transform elevator;
	
	void FixedUpdate()
	{
		if (powered) {
			
			elevator.rigidbody2D.velocity = new Vector2(0, moveDir * moveSpeed);
			
			if ((moveDir > 0 && elevator.position.y >= top.position.y)
			    || (moveDir < 0 && elevator.position.y <= bottom.position.y)) {
				elevator.rigidbody2D.velocity = new Vector2(0, 0);
				Deactivate();
			}

			Transform aliver = elevator.GetComponent<MovingPlatform>().aliver;
			if(aliver != null)
			{
				aliver.GetComponent<AliverController>().SetParentVelocity(elevator.rigidbody2D.velocity);	
			}
		}
	}
}
