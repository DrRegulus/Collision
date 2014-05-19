using UnityEngine;
using System.Collections;

public class Bomb : ThrowableWeapon {
	
	public int damage = 1;
	public float liveTime = 5;
	public bool armed = false;

	void Awake()
	{
		this.Speed = 0f; //set the speed of weapon
		this.throwWeapon ();
		particleSystem.Pause ();
		Destroy (gameObject, liveTime); 
	}

	void OnCollisionEnter2D (Collision2D col) 
	{
		if(col.gameObject.tag == "Projectile"){
			armed = true;
			particleSystem.Play();
			Destroy (gameObject, 0.1f);
			
			
			// If it hits an enemy...
			if(col.gameObject.tag == "Enemy")
			{
				// ... find the Enemy script and call the Hurt function.
				col.gameObject.GetComponent<Enemy>().Hurt(damage);
				
			}
			else if(col.gameObject.tag == "Player"){
				col.gameObject.GetComponent<AliverController>().LoseLives(damage);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.gameObject.tag == "Projectile"){
			armed = true;
			particleSystem.Play();
			
			Destroy (gameObject, 0.1f);
			
			
			// If it hits an enemy...
			if(col.tag == "Enemy")
			{
				// ... find the Enemy script and call the Hurt function.
				col.gameObject.GetComponent<Enemy>().Hurt(damage);
				
			}
			else if(col.tag == "Player"){
				col.gameObject.GetComponent<AliverController>().LoseLives(damage);
			}
		}
	}
}
