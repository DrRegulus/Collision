using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class DummyBot : Enemy {

	public Animator anim;
	public Transform left;
	public Transform right;
	public GameObject throwW;

	public float maxSpeed = 10f;
	public float waitTime = 0f;
	public float maxWaitTime = 2f;
	public float minWaitTime = 1f;

	public bool canMove = true;
	public bool moving = true;
	public bool moveRight = true;
	protected bool alive = true;

	public Stopwatch delay = new Stopwatch();
	private Vector3 dest;

	// Use this for initialization
	void Start () {
		dest = transform.position;
		delay.Start ();
	}


	void FixedUpdate() {

		if((moveRight && transform.position.x > dest.x) ||
		   (!moveRight && transform.position.x < dest.x))
		{
			moving = false;
			
			delay.Reset();
			delay.Start();
			Shoot ();
			waitTime = Random.Range(minWaitTime, maxWaitTime);

			dest = new Vector3(Random.Range(left.position.x, right.position.x), dest.y);
			moveRight = dest.x > transform.position.x;
		}

		if(moving){
			if(moveRight)
				rigidbody2D.velocity = new Vector2 (maxSpeed, rigidbody2D.velocity.y);
			else
				rigidbody2D.velocity = new Vector2 (-maxSpeed, rigidbody2D.velocity.y);
		}
		else if(canMove){

			if(delay.ElapsedMilliseconds > (waitTime * 1000)){
				moving = true;

				delay.Stop();
			}

		}
	
		if(!moving)
		{
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		}

		anim.SetBool("facingRight", moveRight);
		anim.SetBool("moving", moving);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(alive && col.tag == "Player")
		{
			AliverController aliver = col.gameObject.GetComponent<AliverController>();
			aliver.Freeze();
			moving = false;
			aliver.LoseLives(1);
			
			delay.Reset();
			delay.Start();
			
			waitTime = maxWaitTime;
			aliver.Unfreeze();
		}

		else if(col.tag == "Powered" || col.tag == "Weapon")
		{
			alive = false;
			moving = false;
			canMove = false;
			delay.Stop();
			anim.Play("Break");
			Hurt(1);
		}
	}
	void Shoot(){
		Vector3 aim;
		float angle = 0;
		float dir = 0;

		//Set arm rotation to match projectile
		if (moveRight)
		{
			aim = Vector3.right;
			angle = 0;
			dir = 1;
		}
		else
		{
			aim = Vector3.left;
			angle = 180;
			dir = -1;
		}
		
		aim.z = 0;
		aim.Normalize();
		//GameObject arm;
		
		//anim.SetBool("Attack", true);
		
		//arm.transform.parent = transform;
		//Destroy (arm, .2f);
		GameObject proj = Instantiate (throwW, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		proj.transform.Rotate(new Vector3(0, 0, 1), angle);
		proj.rigidbody2D.velocity = new Vector2 (dir,0) * 24f;
	}

	/*void OnCollisionEnter2D(Collision2D col)
	{
		GameObject obj = col.gameObject;

		if(obj.tag == "Player")
		{
			obj.GetComponent<AliverController>().LoseLives(1);
			moving = false;
			
			delay.Reset();
			delay.Start();
			
			waitTime = maxWaitTime;
		}

		if(col.collider.tag != "Ground")
		{
			moving = false;
			
			delay.Reset();
			delay.Start();
			
			waitTime = Random.Range(minWaitTime, maxWaitTime);
		}
	}*/
}
