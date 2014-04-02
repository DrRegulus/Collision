using UnityEngine;
using System.Collections;
using System;

public class AliverController : MonoBehaviour {

	public Animator anim;
	private Transform background;

	public int lives = 3;
	public float ParallaxFactor = 1f;
	public float maxSpeed = 10f;

	public ThrowableWeapon throwW;			//projectile weapon
	public float coolDown = 3.0f;			//able to shoot after 3 sec
	private float lastTime = 0.0f;

	public GameObject RightArm;
	public GameObject LeftArm;

	public bool grounded = true;
	public Transform groundCheck;
	public float jumpForce = 1000f;
	public LayerMask whatIsGround;

	//CheckPoint Variables
	public Vector3 CheckPointPosition;
	public Vector2 checkVelocity;
	public float checkDirection;
	public bool checkGrounded;
	public bool facingRight = true;

	public event CheckPointEventHandler CheckPoint;
	public event ResetEventHandler Reset;

	private Vector3 lastPos;


	public delegate void CheckPointEventHandler(object sender, EventArgs e);
	public delegate void ResetEventHandler(object sender, EventArgs e);


	// Use this for initialization
	void Start () {
		if (anim == null)
			anim = GetComponent<Animator> ();

		lastPos = transform.position;
		background = transform.FindChild ("Background");

		CheckPoint += new CheckPointEventHandler(CheckPointReached);
		Reset += new ResetEventHandler(ResetToCheckPoint);
		facingRight = true;
		//CheckPoint (this, EventArgs.Empty);
	}

	//Set a new checkpoint
	public void CheckPointReached(object sender, EventArgs e){

		CheckPointPosition = transform.position;
		checkVelocity = rigidbody2D.velocity;

		checkGrounded = grounded;
		checkDirection = rigidbody2D.velocity.x;

	}

	//Revert to last checkpoint
	public void ResetToCheckPoint(object sender, EventArgs e){
				
		transform.position = CheckPointPosition;
		rigidbody2D.velocity = checkVelocity;

		anim.SetBool ("Grounded", checkGrounded);
		anim.SetFloat ("hSpeed", checkDirection);

		grounded = checkGrounded;

	}

	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, whatIsGround);

		//Set a new checkpoint DEBUG ONLY
		if(Input.GetButtonDown("CheckPoint")){
	
			CheckPoint(this, EventArgs.Empty);

		}

		//Reset to checkpoint DEBUG ONLY
		if (Input.GetButtonDown ("Reset"))
		{
			Reset(this, EventArgs.Empty);
		}

		//Get movement input
		float move = Input.GetAxis ("Horizontal");
		if (move > 0) {
			facingRight = true;
		} 
		if (move < 0) {
			facingRight = false;
		}

		//Ignore jumps and attacks while not grounded
		if (grounded)
		{
			//Jump
			if(Input.GetButtonDown ("Jump"))
			{
				rigidbody2D.AddForce(new Vector2(0, jumpForce));
			}

			/*Melee attack
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{

			}*/

			//Shoot attack
			if(Input.GetKeyDown(KeyCode.Mouse1))
			{
				if(Time.time - lastTime > coolDown || lastTime == 0)
				{
					//Set arm rotation to match projectile
					Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
					aim.z = 0;
					aim.Normalize();
					float angle = Vector3.Angle( new Vector3(1,0,0), aim);
					GameObject arm;

					if(aim.x > 0)
					{
						anim.Play("ShootRight");
						arm = Instantiate(RightArm, transform.position + new Vector3(.4f, 1.2f , 0), Quaternion.Euler (aim)) as GameObject;
					}
					else
					{
						anim.Play("ShootLeft");
						arm = Instantiate(LeftArm, transform.position + new Vector3(-.4f, 1.2f , 0), Quaternion.Euler(aim)) as GameObject;
					}

					if(aim.y < 0)
						angle = 360 - angle;

					arm.transform.parent = transform;
					Destroy (arm, .2f);

					Shoot( angle );
					lastTime = Time.time;
				}
			}
		}
	}


	void FixedUpdate() {

		//Disconnect from parent platform or object
		if (!grounded)
			transform.parent = null;

		//Update animation variables
		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("hSpeed", rigidbody2D.velocity.x);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		//Move character
		float move = Input.GetAxis ("Horizontal");
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		//Compute distance moved
		float xDist = transform.position.x - lastPos.x;
		float yDist = transform.position.y - lastPos.y;

		//Shift background in opposite direction
		background.Translate(-ParallaxFactor * xDist, -ParallaxFactor * yDist, 0);
		lastPos = transform.position;
	}

	//Generate new projectile
	void Shoot(float angle){
		Debug.Log(angle);
		//Need to set projectile rotation
		Instantiate (throwW, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle)));
	}
}
