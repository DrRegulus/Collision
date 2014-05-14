using UnityEngine;
using System.Collections;

public class ElectricRail : MonoBehaviour {

	public GameObject electricity;
	private GameObject elecInst;
	private Machine control;
	private bool on = false;

	void Start()
	{
		control = transform.parent.GetComponent<Machine> ();
	}

	void Update()
	{
		if(on)
		{
			if(!audio.isPlaying)
			{
				audio.Play();
			}
		}
		else
		{
			if(audio.isPlaying)
			{
				audio.Stop();
			}
		}

		if(control.powered && !on)
		{
			on = true;
			elecInst = Instantiate(electricity, transform.position, new Quaternion()) as GameObject;
			elecInst.transform.rotation = transform.rotation;
		}
		else if(!control.powered && on)
		{
			on = false;
			Destroy (elecInst);
		}
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Projectile")
		{
			if(control.powered)
				control.moveDir *= -1;
			else
				control.Activate();
		}
	}
}
