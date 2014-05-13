using UnityEngine;
using System.Collections;

public class ChangeDirection : MonoBehaviour {
	public float maxSpeed = 50f;
	public Transform Boss;
	private Transform coll; 
	private float step;
	private bool changed = false;
	// Use this for initialization
	public int direction;
	void FixedUpdate() {
		step = maxSpeed * Time.deltaTime;
		if(coll != null){
			if(!changed){
			Boss.position = Vector3.MoveTowards(Boss.position, coll.position, step);
			}
		if(Boss.position == coll.position){
		changed = true;
		Rigidbody2D foo = Boss.rigidbody2D;
		direction = Random.Range(1,5);
		switch(direction){
		case 1: // up
			foo.velocity = new Vector2 (0, maxSpeed);
			break;
		case 2: // right
			foo.velocity = new Vector2 (maxSpeed, 0);
			break;
		case 3: // down
			foo.velocity = new Vector2 (0, -maxSpeed);
			break;
		case 4: // left
			foo.velocity = new Vector2 (-maxSpeed, 0);
			break;
		default:
			break;
			}
		}
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		
		//Take damage from projectile
		if(col.gameObject.tag == "ChangeDir"){
			coll = col.gameObject.transform;
			changed = false;
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{

	//Take damage from projectile
		if(col.tag == "ChangeDir"){
			coll = col.gameObject.transform;
			changed = false;
		}
	}
}
