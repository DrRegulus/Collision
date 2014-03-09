using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public Transform top;
	public Transform bottom;

	public bool powered = false;
	public float moveDir = -1.0f;
	public float moveSpeed = .05f;

	Rigidbody2D elevator;

	// Use this for initialization
	void Start () {
		elevator = transform.FindChild("elevatorPlatform").rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		if (powered) {
			elevator.transform.Translate(0, moveDir * moveSpeed, 0);
			
			if ((moveDir > 0 && elevator.transform.position.y >= top.position.y)
			    || (moveDir < 0 && elevator.transform.position.y <= bottom.position.y)) {
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
