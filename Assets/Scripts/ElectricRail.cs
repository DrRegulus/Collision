using UnityEngine;
using System.Collections;

public class ElectricRail : MonoBehaviour {

	private Machine control;

	void Start()
	{
		control = transform.parent.GetComponent<Machine> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Powered")
		{
			if(control.powered)
				control.moveDir *= -1;
			else
				control.Activate();
		}
	}
}
