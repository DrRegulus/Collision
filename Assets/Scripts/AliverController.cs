using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class AliverController : MonoBehaviour {

	public Animator anim;

	//Background variables
	public Transform background;
	public float ParallaxFactor = .04f;

	//Life counter
	public Sprite lifeSprite;
	public int lives = 3;

	//Movement variables
	private bool frozen = false;
	private bool shielded = false;
	public float maxSpeed = 14f;

	//Weapon prefabs
	public ThrowableWeapon throwW;
	public GameObject shield;
	public float coolDown = 1.0f;
//	private float lastTime = 0.0f;
	public float loadTime = 0.0f;
	private GameObject shieldInstance;

	//Grounding and jump variables
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

	//Private tracking variables
	private Vector3 lastPos;
	private float lastHit = 0;
	private Vector2 parVel = new Vector2(0, 0);
	private Vector3 cameraDest;

	//Checkpoint events & delegates
	public event CheckPointEventHandler CheckPoint;
	public event ResetEventHandler Reset;
	public delegate void CheckPointEventHandler(object sender, EventArgs e);
	public delegate void ResetEventHandler(object sender, EventArgs e);


	// Use this for initialization
	void Start () {
		if (anim == null)
			anim = GetComponent<Animator> ();

		lastPos = transform.position;

		CheckPoint += new CheckPointEventHandler(CheckPointReached);
		Reset += new ResetEventHandler(ResetToCheckPoint);
		facingRight = true;
		CheckPoint (this, EventArgs.Empty);

		int temp = PlayerPrefs.GetInt ("Lives");
		if (temp != 0)
			lives = temp;
	}


	void FixedUpdate()
	{
		if (facingRight)
		{
			cameraDest = new Vector3(transform.position.x + 4, transform.position.y, Camera.main.transform.position.z);
			if(Camera.main.transform.position.x <=  cameraDest.x)
				Camera.main.transform.position += new Vector3(.6f, 0, 0);
		}
		else
		{
			cameraDest = new Vector3(transform.position.x - 4, transform.position.y, Camera.main.transform.position.z);
			if(Camera.main.transform.position.x >=  cameraDest.x)
				Camera.main.transform.position -= new Vector3(.6f, 0, 0);
		}
	}


	// Update is called once per frame
	void Update () {
		if(lives <= 0)
			GameOver();

		grounded = Physics2D.Linecast (transform.position, groundCheck.position, whatIsGround);
		if (!grounded)
			anim.SetBool ("Attack", false);

		if(shielded)
		{
			Camera.main.transform.position += new Vector3(UnityEngine.Random.Range(-.1f, .1f), UnityEngine.Random.Range(-.1f, .1f), 0);
			rigidbody2D.velocity = parVel;
			//shieldInstance.rigidbody2D.velocity = parVel;
		}

		if(!frozen)
		{
			if(!shielded)
			{
				//Move character
				float move = Input.GetAxis ("Horizontal");
				rigidbody2D.velocity = new Vector2 (move * maxSpeed + parVel.x, rigidbody2D.velocity.y);

				if(move > 0)
				{
					facingRight = true;
				}
				else if(move < 0)
				{
					facingRight = false;
				}

				if(anim.GetBool("Attack"))
				{
					Camera.main.transform.position += new Vector3(UnityEngine.Random.Range(-.05f, .05f), UnityEngine.Random.Range(-.05f, .05f), 0);
				}

				//Ignore jumps and attacks while not grounded
				if (grounded && !anim.GetBool("Attack"))
				{
					//Jump
					if(Input.GetButtonDown ("Jump"))
					{
						rigidbody2D.AddForce(new Vector2(0, jumpForce));
					}
					
					//Shoot attack
					if(Input.GetKeyDown(KeyCode.Mouse1) && Time.timeScale == 1)
					{
						if(!anim.GetBool("Attack"))
						{
							anim.SetBool("Attack", true);
							if(Time.time - loadTime > coolDown || loadTime == 0)
							{
								Shoot();
								audio.Play();
								loadTime = Time.time;
							}
						}
					}

					if(Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale == 1)
					{
						shielded = true;
						shieldInstance = Instantiate(shield, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
						shieldInstance.transform.parent = transform;
						anim.SetBool("Attack", true);
					}
				}
				else if(anim.GetBool("Attack") && Time.time - loadTime > coolDown){
					Shoot ();
					audio.Play();
					loadTime = Time.time;
				}

				if(Input.GetKeyUp(KeyCode.Mouse1)){
					anim.SetBool("Attack", false);
				}
			}
			else if(Input.GetKeyUp(KeyCode.Mouse0) && Time.timeScale == 1)
			{
				if(shieldInstance != null)
				{
					Destroy(shieldInstance);
					shielded = false;
					anim.SetBool("Attack", false);
				}
			}
		}
		else
		{
			rigidbody2D.velocity = new Vector2(0, 0);
		}

	

		//Compute distance moved
		float xDist = transform.position.x - lastPos.x;
		float yDist = transform.position.y - lastPos.y;
		
		//Shift background in opposite direction
		background.position = background.position + new Vector3(-ParallaxFactor * xDist, -ParallaxFactor * yDist, 0);
		lastPos = transform.position;

		//Update animation variables
		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("hSpeed", rigidbody2D.velocity.x - parVel.x);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y - parVel.y);
	}


	void OnGUI()
	{
		GUI.backgroundColor = Color.clear;
		for (int i = 0; i < lives; i++) 
		{
			GUI.Box(new Rect(i * Screen.width/20, Screen.height - Screen.height/10,
			                 Screen.width/20, Screen.height/10), new GUIContent(lifeSprite.texture));
		}
	}


	///Set a new checkpoint
	public void CheckPointReached(object sender, EventArgs e){
		
		CheckPointPosition = transform.position;
		checkVelocity = rigidbody2D.velocity;
		
		checkGrounded = grounded;
		checkDirection = rigidbody2D.velocity.x;
		
	}


	///Revert to last checkpoint
	public void ResetToCheckPoint(object sender, EventArgs e){
		
		transform.position = CheckPointPosition;
		rigidbody2D.velocity = checkVelocity;
		
		anim.SetBool ("Grounded", checkGrounded);
		anim.SetFloat ("hSpeed", checkDirection);
		
		grounded = checkGrounded;
		
	}


	///Generate new projectile
	private void Shoot(){
		Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		aim.z = 0;
		aim.Normalize();
		float angle = Vector3.Angle( new Vector3(1,0,0), aim);
		
		if(aim.y < 0)
			angle = 360 - angle;

		Instantiate (throwW, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle)));
	}


	/*private void ShootBomb(){
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
	}*/

	
	/// <summary>
	/// Decrements life counter and prevents continuous collision damage.
	/// </summary>
	/// <param name="damage">Damage received</param>
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


	/// <summary>
	/// Increment the life counter.
	/// </summary>
	public void GainLife()
	{
		lives++;
	}


	/// <summary>
	/// Disable all player movement.
	/// </summary>
	public void Freeze()
	{
		frozen = true;
		rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
	}


	/// <summary>
	/// Enable all player movement.
	/// </summary>
	public void Unfreeze()
	{
		frozen = false;
	}

	/// <summary>
	/// Track parent platform's velocity for smooth movement
	/// </summary>
	/// <param name="vel">Parent's velocity</param>
	public void SetParentVelocity(Vector2 vel)
	{
		parVel = vel;
	}

	/// <summary>
	/// Ends game immediately.
	/// </summary>
	public void GameOver()
	{
		if(Application.loadedLevelName == "Level3")
		{
			PlayerPrefs.SetInt("Ending", -1);
			PlayerPrefs.SetInt("Lives", 3);
			Application.LoadLevel("Credits");
		}
		else
		{
			PlayerPrefs.SetInt("Lives", 3);
			Application.LoadLevel ("GameOver");
		}
	}
}
