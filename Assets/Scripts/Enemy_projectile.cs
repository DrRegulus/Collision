using UnityEngine;
using System.Collections;


public class Enemy_projectile : MonoBehaviour {
	
	public int damage = 0;
	public float liveTime = 5;
	public Animator anim;
	private float speed;
	void Awake()
	{
		speed = 20f; //set the speed of weapon
		//this.rigidbody2D.velocity = this.transform.position * speed;

		Destroy (gameObject, liveTime); 
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.tag == "Player" || (col.tag != "Enemy" && !col.isTrigger))
		{
			rigidbody2D.velocity = new Vector2(0, 0);
			anim.SetBool ("Collided", true);
			Destroy (gameObject, .5f);
			
			/*// If it hits an enemy...
			if(col.tag == "Enemy")
			{
				// ... find the Enemy script and call the Hurt function.
				col.gameObject.GetComponent<Enemy>().Hurt(damage);
			}*/
			
		}
	}

	void Update(){
		//rigidbody2D.AddTorque(Random.Range(-10,10));
	}
}
