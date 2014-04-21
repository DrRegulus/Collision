﻿using UnityEngine;
using System.Collections;

public class SteamValve : PowerSwitch {

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Powered"){
			if(!anim.GetBool("Power")){
				anim.SetBool ("Power", true);
				Destroy(transform.parent.FindChild ("Steam").gameObject, 2);
			}
		}
	}

	void Update()
	{
		if(debug)
		{
			anim.SetBool ("Power", true);
			Destroy(transform.parent.FindChild ("Steam").gameObject, 2);
			debug = false;
		}
	}
}
