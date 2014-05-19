using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Boss : Enemy {

	public Animator anim;
	public GameObject throwW;
	
	public float maxSpeed = 10f;
	public float waitTime = 1f;
	public float maxWaitTime = 2f;
	public float minWaitTime = 1f;
	public float cooldown = 1f;
	
	public bool canMove = true;
	public bool moving = true;
	public bool moveRight = true;
	private Vector3 aim;
	public Stopwatch delay = new Stopwatch();
	private Vector3 dest;
	private float shootTime = 0f;
	private bool turned = false;
	public GameObject targetDir;


	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = new Vector2 (-maxSpeed, 0);
		delay.Start ();
	}
	
	void Update()
	{
		if(!alive)
		{
			PlayerPrefs.SetInt("Ending", 1);
			Application.LoadLevel("Credits");
		}
	}

	void FixedUpdate() {

		//Change direction of sprite
		if(!turned && rigidbody2D.velocity.x > 0)
		{
			transform.Rotate(0, 180, 0);
			turned = true;
		}
		else if(turned && rigidbody2D.velocity.x <0)
		{
			transform.Rotate(0, -180, 0);
			turned = false;
		}

		if(moving){
			//Check attack cooldown while moving only
			if(Time.time - shootTime > cooldown)
			{
				//Shoot randomly
				if(Random.Range(0, 4) > 0)
				{
					anim.Play ("Attack");
					Shoot ();
				}
			}

		}
		else if(canMove){
			
			//Wait condition
			if(delay.ElapsedMilliseconds > (waitTime * 1000)){
				moving = true;
				delay.Stop();
			}
			
		}
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		//Damage Aliver on collision and stop moving
		if (alive && col.tag == "Player") {
						AliverController aliver = col.gameObject.GetComponent<AliverController> ();
						aliver.Freeze ();
						moving = false;
						aliver.LoseLives (1);
			
						delay.Reset ();
						delay.Start ();
			
						waitTime = maxWaitTime;
						aliver.Unfreeze ();
				}
		
		//Take damage from projectile
		else if (col.tag == "Bomb" && col.gameObject.GetComponent<Bomb>().armed) {
						moving = false;
						canMove = false;
						delay.Stop ();
						Hurt (1);
						anim.Play ("Hurt");
				} else if (col.tag == "Shield") {
						moving = false;
						dest = transform.position;
						delay.Reset ();
						delay.Start ();
				} else if (col.tag == "Boundary" || col.tag == "Ground") {
					rigidbody2D.velocity = rigidbody2D.velocity  * -1;
				} else if (col.tag == "ChangeDirection") {
					aim = col.gameObject.GetComponent<ChangeDirection> ().nextVel;
				}
	}

	
	
	/// <summary>
	/// Shoot while moving.
	/// </summary>
	private void Shoot(){
		
		float angle = 0;
		float dir = 0;
		
		shootTime = Time.time;

		Vector3 aim =  GameObject.Find("Aliver").transform.position - transform.position;
		aim.z = 0;
		aim.Normalize();
		angle = Vector3.Angle( new Vector3(1,0,0), aim);

		if(transform.position.y > GameObject.Find("Aliver").transform.position.y ){

			angle = 360 - angle;
		}

		Vector3 tangent = Vector3.Cross( aim, new Vector3(0,0,1) );
		
		if( tangent.magnitude == 0 )
		{
			tangent = Vector3.Cross( aim, new Vector3(0,1,0) );
		}

		tangent.Normalize();
		tangent *= 4;

		Vector3 pos1 = transform.position + tangent;
		Vector3 pos2 = transform.position;
		Vector3 pos3 = transform.position - tangent;

		GameObject proj1 = Instantiate (throwW, pos1, Quaternion.Euler (new Vector3 (0, 0, angle))) as GameObject;

		//UnityEngine.Debug.Log(angle);
		GameObject proj2 = Instantiate (throwW, pos2, Quaternion.Euler (new Vector3 (0, 0, angle))) as GameObject;
		GameObject proj3 = Instantiate (throwW, pos3, Quaternion.Euler (new Vector3 (0, 0, angle))) as GameObject;
		Vector2 vel = new Vector2 (Mathf.Cos (angle * (Mathf.PI / 180)) * (180 / Mathf.PI), Mathf.Sin (angle * (Mathf.PI / 180)) * (180 / Mathf.PI));
		proj1.rigidbody2D.velocity = vel.normalized * 20;
		proj2.rigidbody2D.velocity = vel.normalized * 25;
		proj3.rigidbody2D.velocity = vel.normalized * 20;
	}
	
	bool hasVision(  string target, float range){
		
		GameObject targ = GameObject.Find(target);
		if( targ == null ){
			
			return false;
		}
		
		int mask = ~(1 << LayerMask.NameToLayer("Enemies"));
		RaycastHit2D hit = Physics2D.Raycast(transform.position, targ.transform.position - transform.position, range, mask);
		
		if( hit == null ){
			
			return false;
		}
		
		if ( hit.transform.name.Equals(target) ){
			
			return true;
		}
		
		return false;
		
	}
	
	bool isFacing(string target){
		
		GameObject targ = GameObject.Find(target);
		if( targ == null ){
			
			return false;
		}
		
		float targetDir = targ.transform.position.x - transform.position.x;
		
		if( (moveRight && targetDir >= 0) || (!moveRight && targetDir <= 0)){
			
			return true;
		}
		
		return false;
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
