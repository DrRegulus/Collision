using UnityEngine;
using System.Collections;
using System;

public class ShipMaglev : Maglev {

	private bool locked = false;
	private AudioSource gearSound;

	// Use this for initialization
	void Start () {
		maglev = transform.FindChild("ShipDock");
		gearSound = transform.FindChild ("switch").audio;

		//set up event handlers for CheckPoints and Resets
		AliverController aliver = GameObject.Find("Aliver").GetComponent<AliverController>();
		aliver.CheckPoint += new AliverController.CheckPointEventHandler(CheckPointReached);
		aliver.Reset += new AliverController.ResetEventHandler(ResetToCheckPoint);
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (powered && !locked) {

			if(!gearSound.isPlaying)
			{
				gearSound.Play();
			}

			FixedRotator[] gears = maglev.GetComponentsInChildren<FixedRotator>();
			gears[0].Rotate(FixedRotator.Direction.COUNTERCLOCKWISE);
			gears[1].Rotate(FixedRotator.Direction.COUNTERCLOCKWISE);

			//maglev.Translate(moveSpeed * moveDir, 0 , 0);

			maglev.rigidbody2D.velocity = new Vector2(2 * moveDir * moveSpeed, 0);
			
			if ((moveDir > 0 && maglev.position.x >= rightEdge.position.x)
			    || (moveDir < 0 && maglev.position.x <= leftEdge.position.x)) {
				powered = false;
				locked = true;

				if(gearSound.isPlaying)
				{
					gearSound.Stop();
				}

				maglev.rigidbody2D.velocity = new Vector2(0, 0);
			}
		}
	}
}
