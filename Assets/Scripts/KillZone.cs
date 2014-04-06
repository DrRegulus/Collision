using UnityEngine;
using System.Collections;
using System;

public class KillZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		AliverController controls = null;

		if(col.tag == "Player")
		{
			controls = col.gameObject.GetComponent<AliverController>();
		}

		if (controls != null)
		{
			//Infinite lives for debugging
			//controls.lives--;

			if(controls.lives > 0)
				controls.ResetToCheckPoint(this, EventArgs.Empty);
			else
				controls.GameOver();
		}
		else
			Destroy (col.gameObject);
	}
}
