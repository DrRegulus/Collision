using UnityEngine;
using System.Collections;


public class pyochang : throwableWeapon {

	public float liveTime = 5;
	void Awake()
	{
		this.Speed = 25f; //set the speed of weapon
		this.throwWeapon ();
		Destroy (gameObject, liveTime); 
	}
	void Update(){ //spin the weapon
		rigidbody2D.AddTorque(Random.Range(-10,10));
	}
}
