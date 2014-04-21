using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class AliverController : MonoBehaviour {

	public Animator anim;
	private Transform background;
	private bool frozen = false;

	public int lives = 3;
	public float ParallaxFactor = 1f;
	public float maxSpeed = 10f;

	public ThrowableWeapon throwW;			//projectile weapon
	public ThrowableWeapon bomb;
	public float coolDown = 3.0f;			//able to shoot after 3 sec
	private float lastTime = 0.0f;

	public Sprite lifeSprite;

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
	private float lastHit = 0;

	private Vector2 parVel = new Vector2(0, 0);

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
		CheckPoint (this, EventArgs.Empty);
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
		if(lives <= 0)
			GameOver();

		grounded = Physics2D.Linecast (transform.position, groundCheck.position, whatIsGround);

		if(!frozen)
		{
			//Move character
			float move = Input.GetAxis ("Horizontal");
			rigidbody2D.velocity = new Vector2 (move * maxSpeed + parVel.x, rigidbody2D.velocity.y);

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
				if(Input.GetKeyDown(KeyCode.Mouse1) && Time.timeScale == 1)
				{
					if(!anim.GetBool("Attack"))
					{
						Shoot ();
					}
				}
				if(Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale == 1)
				{
					if(!anim.GetBool("Attack"))
					{
						ShootBomb ();
					}
				}
			}
		}
		else
		{
			rigidbody2D.velocity = new Vector2(0, 0);
		}

		if(Time.time - lastTime > coolDown || lastTime == 0)
		{
			anim.SetBool("Attack", false);
		}

		//Compute distance moved
		float xDist = transform.position.x - lastPos.x;
		float yDist = transform.position.y - lastPos.y;
		
		if (xDist > 0)
			facingRight = true;
		else
			facingRight = false;
		
		//Shift background in opposite direction
		background.Translate(-ParallaxFactor * xDist, -ParallaxFactor * yDist, 0);
		lastPos = transform.position;

		//Update animation variables
		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("hSpeed", rigidbody2D.velocity.x - parVel.x);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y - parVel.y);
	}


	//Generate new projectile
	void Shoot(){
			//Set arm rotation to match projectile
			Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			aim.z = 0;
			aim.Normalize();
			float angle = Vector3.Angle( new Vector3(1,0,0), aim);
			//GameObject arm;

			anim.SetBool("Attack", true);
			
			if(aim.y < 0)
				angle = 360 - angle;
			
			//arm.transform.parent = transform;
			//Destroy (arm, .2f);
			
			Instantiate (throwW, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle)));
			lastTime = Time.time;
	}
	void ShootBomb(){
		//Set arm rotation to match projectile
		Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		aim.z = 0;
		aim.Normalize();
		float angle = Vector3.Angle( new Vector3(1,0,0), aim);
		//GameObject arm;
		
		anim.SetBool("Attack", true);
		
		if(aim.y < 0)
			angle = 360 - angle;
		
		//arm.transform.parent = transform;
		//Destroy (arm, .2f);
		
		Instantiate (bomb, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle)));
		lastTime = Time.time;
	}


	public void LoseLives(int damage)
	{
		if(Time.time - lastHit > 1)
		{
			lastHit = Time.time;
			lives -= damage;
			if (lives <= 0)
				GameOver ();
		}
	}

	public void GainLife()
	{
		lives++;
	}

	public void Freeze()
	{
		frozen = true;
		rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
	}

	public void Unfreeze()
	{
		frozen = false;
	}

	void OnGUI()
	{
		GUI.backgroundColor = Color.clear;
		for (int i = 0; i < lives; i++) 
		{
			GUI.Box(new Rect(i * lifeSprite.texture.width, Screen.height - lifeSprite.texture.height, lifeSprite.texture.width, lifeSprite.texture.height), new GUIContent(lifeSprite.texture));
		}
	}

	public void SetParentVelocity(Vector2 vel)
	{
		parVel = vel;
	}

	public void GameOver()
	{
		Application.LoadLevel ("MainMenu");
	}
}
