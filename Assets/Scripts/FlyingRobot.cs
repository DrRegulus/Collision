using UnityEngine;
using System.Collections;

public class FlyingRobot : Enemy {
	
	public Animator anim;
	public Transform[] patrolPoints;
	public GameObject throwW;
	
	public float maxSpeed = 12f;
	public float cooldown = 2f;
	
	private Vector3 dest;
	private int patrolIdx = 0;
	private float xDir = 0;//I'd rename to deltaX or something
	private float yDir = -1;
	
	private bool inCombat = false;
	private bool isCloseToAliver = false;
	private float shootTime = 0f;
	private float lastSeenTime = 0f; //The time the robot last could engage Aliver.
	private float suspicionDuration = 5f; //How long to keep being suspicious after Aliver escapes the range.
	private bool turned = false;
	
	//The circlecollider for combat radius used to be 4;
	
	private void dlog(string o)
	{
		//Debug.Log (o);
	}
	
	private Transform playerPos;
	
	// Use this for initialization
	void Start () {
		dest = patrolPoints[patrolIdx].position;
	}
	
	// Update is called once per frame
	void Update () {
		if(alive)
		{
			//Change direction of sprite
			if(!turned && dest.x > transform.position.x)
			{
				transform.Rotate(0, 180, 0);
				turned = true;
			}
			else if(turned && dest.x < transform.position.x)
			{
				transform.Rotate(0, -180, 0);
				turned = false;
			}

			//dlog ("I'm a flying robot who updated!");
			if(inCombat)
			{
				//rigidbody2D.velocity = new Vector2(0, 0);
				dlog("I am in combat. Is Aliver close?: "+isCloseToAliver);
				
				
				
				dlog (Time.time +" current/shoot "+shootTime);
				if( (Time.time - shootTime) > cooldown)
				{
					Shoot ();
				}
				
				
				
				if(!isCloseToAliver)
				{
					
					
					if(!playerPos.Equals(null))
					{
						headTowards(playerPos.position,4);
					}
					
					//Disengage with Aliver
					if(Time.time - lastSeenTime > suspicionDuration)
					{
						playerPos = null;
						inCombat = false;
					}
				}
				//I am close.
				else
				{
					rigidbody2D.velocity = new Vector2(0, 0);
				}
				
				//Shoot at aliver here
			}
			else
			{
				if(!Arrived ())
				{
					calculateDirection();
					
					dlog("I am moving at a speed! maxSpeed is "+maxSpeed);
					//rigidbody2D.velocity = new Vector2(xDir * maxSpeed, yDir * maxSpeed);
					//rigidbody2D.velocity = Vector2.MoveTowards(transform.position,dest,1);
					headTowards(dest);
					//dlog("I'm going this fast "+rigidbody2D.velocity.magnitude);
				}
				else
				{
					if(patrolPoints.Length>1)
						patrolIdx = (patrolIdx + 1) % patrolPoints.Length;
					
					calculateDirection();
				}
				
				
			}
		}
	}
	
	float headTowards(Vector2 destination, float minSpeed)
	{
		rigidbody2D.velocity = Vector2.ClampMagnitude((Vector2)destination - (Vector2)transform.position,maxSpeed);
		if(rigidbody2D.velocity.magnitude<minSpeed) rigidbody2D.velocity = ((Vector2)destination - (Vector2)transform.position).normalized*minSpeed;
		
		return rigidbody2D.velocity.magnitude;
	}
	
	float headTowards (Vector2 destination) 
	{
		return headTowards (destination, 0);
	}
	
	void calculateDirection()
	{
		dest = patrolPoints[patrolIdx].position;
		dlog("I am heading to "+patrolPoints[patrolIdx].name);
		return;
		/*		float slope = (dest.x - transform.position.x) / (dest.y - transform.position.y);
		dlog ("Slope " + slope);
		xDir = Mathf.Sin (slope);
		yDir = Mathf.Cos (slope);
		return;*/
		
	}
	
	bool hasVision(  string target, float range){
		
		GameObject targ = GameObject.Find(target);
		if( targ == null ){
			
			return false;
		}
		
		int mask = ~(1 << LayerMask.NameToLayer("Enemies"));
		//UnityEngine.Debug.DrawLine(transform.position, targ.transform.position - transform.position, range, mask);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, targ.transform.position - transform.position, range, mask);
		if( RaycastHit2D.ReferenceEquals(hit,null) || RaycastHit2D.ReferenceEquals(hit.transform,null) ) //hit == null was some convoluted error where a null like X > null, X == null is always false because the operation's unevaluatable because null. I dunno.
			//|| hit.transform == null){
		{
			
			dlog("No hit!");
			return false;
		}
		
		if ( hit.transform.name.Equals(target) ){
			
			return true;
		}
		
		dlog("I hit "+hit.transform.name);
		return false;
		
	}
	
	private bool Arrived()
	{
		return(dest - transform.position).magnitude < 0.1f;//Approximate arrival should be good enough for simplicity, understandability, etc. Prevents Xeno's paradox in moving.
		
		/*return ((xDir > 0 && transform.position.x >= dest.x) ||
				(xDir < 0 && transform.position.x <= dest.x) ||
		         xDir == 0) &&
			   ((yDir > 0 && transform.position.y >= dest.y) ||
		 		(yDir < 0 && transform.position.y <= dest.y) ||
			 	 yDir == 0);*/
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			inCombat = true;
			isCloseToAliver = true;
			playerPos = col.transform;
		}
		else if(col.tag == "Projectile" && alive)
		{
			Hurt (1);
			anim.Play("Break");
			rigidbody2D.gravityScale = 1;
			rigidbody2D.fixedAngle = false;
			rigidbody2D.velocity = new Vector2(0, 0);
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			isCloseToAliver = false;
			lastSeenTime = Time.time;
			//playerPos = null;
		}
	}
	
	private void Shoot(){
		
		float angle = 0;
		float dir = 0;
		
		shootTime = Time.time;
		//anim.SetBool("Attack", true);
		
		Vector3 aim =  GameObject.Find("Aliver").transform.position - transform.position;
		aim.z = 0;
		aim.Normalize();
		angle = Vector3.Angle( new Vector3(1,0,0), aim);
		
		if(transform.position.y > GameObject.Find("Aliver").transform.position.y ){
			
			angle = 360 - angle;
		}
		
		
		Vector3 pos1 = transform.position;
		
		GameObject proj1 = Instantiate (throwW, pos1, Quaternion.Euler (new Vector3 (0, 0, angle))) as GameObject;
		
		dlog("I'm shooting at angle "+angle);
		
		Vector2 vel = new Vector2 (Mathf.Cos (angle * (Mathf.PI / 180)) * (180 / Mathf.PI), Mathf.Sin (angle * (Mathf.PI / 180)) * (180 / Mathf.PI));
		proj1.rigidbody2D.velocity = vel.normalized * 20;
		
	}
	
	
}
