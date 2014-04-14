using UnityEngine;
using System.Collections;

public class ShipMaglev : Maglev {

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
		if (powered) {

			FixedRotator[] gears = maglev.GetComponentsInChildren<FixedRotator>();
			gears[0].Rotate();
			gears[1].Rotate();

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
}
