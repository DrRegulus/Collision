using UnityEngine;
using System.Collections;

public class AliverController : MonoBehaviour {

	public Animator anim;

	public float maxSpeed = 10f;

	public bool grounded = true;
	public Transform groundCheck;
	public float jumpForce = 1000f;
	public LayerMask whatIsGround;

	// Use this for initialization
	void Start () {

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
	}


	void OnCollisionExit2D(Collision2D col)
	{
		transform.parent = null;
	}
}
