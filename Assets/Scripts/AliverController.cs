using UnityEngine;
using System.Collections;

public class AliverController : MonoBehaviour {

	public Animator anim;
	private Transform background;

	public float ParallaxFactor = 1f;
	public float maxSpeed = 10f;

	public bool grounded = true;
	public Transform groundCheck;
	public float jumpForce = 1000f;
	public LayerMask whatIsGround;

	private Vector3 lastPos;

	// Use this for initialization
	void Start () {
		lastPos = transform.position;
		background = transform.FindChild ("Background");
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, whatIsGround);

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
