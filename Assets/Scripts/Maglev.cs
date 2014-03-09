using UnityEngine;
using System.Collections;

public class Maglev : MonoBehaviour {

	public bool powered = false;
	public float moveDir = 1f;
	public float moveSpeed = 0.1f;

	public Transform leftBorder;
	public Transform rightBorder;
	public Transform leftEdge;
	public Transform rightEdge;

	Rigidbody2D maglev;

	// Use this for initialization
	void Start () {
		maglev = transform.FindChild("platform_block").rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		if (powered) {

			maglev.transform.Translate(moveSpeed * moveDir, 0 , 0);
			
			if ((moveDir > 0 && rightBorder.position.x >= rightEdge.position.x)
			    || (moveDir < 0 && leftBorder.position.x <= leftEdge.position.x)) {
				moveDir *= -1;
				powered = false;
			}
		}
	}


	public void Activate()
	{
		powered = true;
	}
}
