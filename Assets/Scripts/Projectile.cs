using UnityEngine;
using System.Collections;


public class Projectile : ThrowableWeapon {

	public int damage = 0;
	public float liveTime = 5;
	public Animator anim;

	void Awake()
	{
		this.Speed = 25f; //set the speed of weapon
		this.throwWeapon ();
		Destroy (gameObject, liveTime); 
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.tag == "Powered" || col.tag == "Enemy" || (col.tag != "Player" && !col.isTrigger))
		{
			rigidbody2D.velocity = new Vector2(0, 0);
			anim.SetBool ("Collided", true);
			Destroy (gameObject, .5f);

			// If it hits an enemy...
			if(col.tag == "Enemy")
			{
				// ... find the Enemy script and call the Hurt function.
				col.gameObject.GetComponent<Enemy>().Hurt(damage);
			}

		}
	}

	void Update(){
		//rigidbody2D.AddTorque(Random.Range(-10,10));
	}
}
