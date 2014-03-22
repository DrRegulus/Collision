﻿using UnityEngine;
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
			controls.lives--;
			if(controls.lives > 0)
				controls.ResetToCheckPoint(this, EventArgs.Empty);
			else
				GameOver();
		}
		else
			Destroy (col.gameObject);
	}
	
	public void GameOver()
	{
		Application.LoadLevel ("mainMenu");
	}
}
