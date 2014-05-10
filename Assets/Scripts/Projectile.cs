using UnityEngine;
using System.Collections;


public class Projectile : ThrowableWeapon {

	public bool collided = false;
	public int damage = 1;
	public float liveTime = 5;
	public Animator anim;

	void Awake()
	{
		this.Speed = 25f; //set the speed of weapon
		this.throwWeapon ();
		Destroy (gameObject, liveTime); 
	}

	void Update()
	{
		if(collided)
		{
			anim.SetBool ("Collided", true);
			rigidbody2D.velocity = new Vector2(0, 0);
			Destroy (gameObject, .5f);
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.tag == "Powered" || (col.tag != "Player" && !col.isTrigger))
		{
			collided = true;
		}
	}
}
