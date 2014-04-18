using UnityEngine;
using System.Collections;
using System;

public class ShipMaglev : Maglev {

	private bool locked = false;

	// Use this for initialization
	void Start () {
		maglev = transform.FindChild("ShipDock");
		
		//set up event handlers for CheckPoints and Resets
		AliverController aliver = GameObject.Find("Aliver").GetComponent<AliverController>();
		aliver.CheckPoint += new AliverController.CheckPointEventHandler(CheckPointReached);
		aliver.Reset += new AliverController.ResetEventHandler(ResetToCheckPoint);
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (powered && !locked) {

			FixedRotator[] gears = maglev.GetComponentsInChildren<FixedRotator>();
			gears[0].Rotate(FixedRotator.Direction.COUNTERCLOCKWISE);
			gears[1].Rotate(FixedRotator.Direction.COUNTERCLOCKWISE);

			maglev.Translate(moveSpeed * moveDir, 0 , 0);
			
			if ((moveDir > 0 && maglev.position.x >= rightEdge.position.x)
			    || (moveDir < 0 && maglev.position.x <= leftEdge.position.x)) {
				powered = false;
				locked = true;
			}
		}
	}
}
