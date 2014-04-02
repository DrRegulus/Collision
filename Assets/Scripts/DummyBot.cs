using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class DummyBot : MonoBehaviour {

	public float moveRange = 5f;
	public float maxSpeed = 10f;
	public float waitTime = 0f;
	public float maxWaitTime = 2f;
	public float minWaitTime = 1f;
	public float moveCenterX = 0;
	public float moveDestinationX = 0;
	public bool move = true;
	public bool moving = false;
	public bool leftOfDestination = false;

	public Stopwatch delay = new Stopwatch();


	// Use this for initialization
	void Start () {
	
		moveCenterX = transform.position.x;
		move = true;
		moving = false;
	}


	void FixedUpdate() {

		UnityEngine.Debug.Log( "" + moving);

		if(move){

			moveDestinationX = moveCenterX + Random.Range(0f, moveRange * 2) - moveRange;

			if(moveDestinationX > transform.position.x){

				leftOfDestination = true;
			}
			else{

				leftOfDestination = false;
			}

			move = false;
			moving = true;
		}

		if(moving){

			if(leftOfDestination){

				rigidbody2D.velocity = new Vector2 (maxSpeed, rigidbody2D.velocity.y);
			}
			else{

				rigidbody2D.velocity = new Vector2 (-maxSpeed, rigidbody2D.velocity.y);
			}


			if( (moveDestinationX > transform.position.x) != leftOfDestination){

				rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);

				moving = false;

				delay.Reset();
				delay.Start();

				waitTime = Random.Range(minWaitTime, maxWaitTime);
				//set not moving animation
			}

		}

		if(!moving && !move){

			if(delay.ElapsedMilliseconds > (waitTime * 1000)){

				delay.Stop();
				move = true;
			}

		}


	}

	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionExit2D(Collision2D col)
	{
		moving = false;
		
		delay.Reset();
		delay.Start();
		
		waitTime = Random.Range(minWaitTime, maxWaitTime);
	}

}
