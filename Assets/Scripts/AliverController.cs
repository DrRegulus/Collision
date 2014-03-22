using UnityEngine;
using System.Collections;
using System;

public class AliverController : MonoBehaviour {

	public Animator anim;
	private Transform background;

	public int lives = 3;
	public float ParallaxFactor = 1f;
	public float maxSpeed = 10f;

	public bool grounded = true;
	public Transform groundCheck;
	public float jumpForce = 1000f;
	public LayerMask whatIsGround;

	//CheckPoint Variables
	public Vector3 CheckPointPosition;
	public Vector2 checkVelocity;
	public float checkDirection;
	public bool checkGrounded;

	public event CheckPointEventHandler CheckPoint;
	public event ResetEventHandler Reset;

	private Vector3 lastPos;


	public delegate void CheckPointEventHandler(object sender, EventArgs e);
	public delegate void ResetEventHandler(object sender, EventArgs e);

	protected virtual void OnCheckPoint(EventArgs e) 
	{
		CheckPoint(this, e);
	}

	protected virtual void OnReset(EventArgs e) 
	{
		Reset(this, e);
	}


	// Use this for initialization
	void Start () {
		lastPos = transform.position;
		background = transform.FindChild ("Background");

		CheckPoint += new CheckPointEventHandler(CheckPointReached);
		Reset += new ResetEventHandler(ResetToCheckPoint);

		CheckPoint (this, EventArgs.Empty);
	}


	public void CheckPointReached(object sender, EventArgs e){

		CheckPointPosition = transform.position;
		checkVelocity = rigidbody2D.velocity;

		checkGrounded = grounded;
		checkDirection = rigidbody2D.velocity.x;

	}

	public void ResetToCheckPoint(object sender, EventArgs e){
				
		transform.position = CheckPointPosition;
		rigidbody2D.velocity = checkVelocity;

		anim.SetBool ("Grounded", checkGrounded);
		anim.SetFloat ("vSpeed", checkDirection);

		grounded = checkGrounded;

	}

	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, whatIsGround);

	
		if(Input.GetButtonDown("CheckPoint")){
	
			OnCheckPoint(EventArgs.Empty);

		}


		if(Input.GetButtonDown("Reset")){

			OnReset(EventArgs.Empty);

		}


		if (grounded && Input.GetButtonDown ("Jump"))
		{
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
			transform.parent = null;
		}
	}


	void FixedUpdate() {

		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.x);

		float move = Input.GetAxis ("Horizontal");
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		float xDist = transform.position.x - lastPos.x;
		float yDist = transform.position.y - lastPos.y;
		
		background.Translate(-ParallaxFactor * xDist, -ParallaxFactor * yDist, 0);
		lastPos = transform.position;
	}


	void OnCollisionExit2D(Collision2D col)
	{
		transform.parent = null;
	}
}
