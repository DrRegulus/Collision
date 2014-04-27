using UnityEngine;
using System.Collections;


public class EnemyProjectile : MonoBehaviour {
	
	public int damage = 1;
	public float liveTime = 5;
	public Animator anim;

	void Awake()
	{
		Destroy (gameObject, liveTime); 
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.tag == "Player" || (col.tag != "Enemy" && !col.isTrigger) || col.tag == "Shield")
		{
			rigidbody2D.velocity = new Vector2(0, 0);
			anim.SetBool ("Collided", true);
			Destroy (gameObject, .5f);
			

			if(col.tag == "Player")
			{
				col.gameObject.GetComponent<AliverController>().LoseLives(damage);
			}
			
		}
	}
}
