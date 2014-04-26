using UnityEngine;
using System.Collections;

public class FlyingRobot : Enemy {

	public Animator anim;
	public Transform[] patrolPoints;
	public GameObject throwW;
	
	public float maxSpeed = 12f;
	public float cooldown = 1f;
	
	protected bool alive = true;

	private Vector3 dest;
	private int patrolIdx = 0;
	private int xDir = 0;
	private int yDir = -1;

	private bool inCombat = false;
	private float shootTime = 0f;


	// Use this for initialization
	void Start () {
		dest = patrolPoints[patrolIdx].position;
	}
	
	// Update is called once per frame
	void Update () {
		if(inCombat)
		{
			rigidbody2D.velocity = new Vector2(0, 0);
		}
		else
		{
			if(!Arrived ())
			{
				rigidbody2D.velocity = new Vector2(xDir * maxSpeed, yDir * maxSpeed);
			}
			else
			{
				patrolIdx = (patrolIdx + 1) % patrolPoints.Length;
				dest = patrolPoints[patrolIdx].position;

				if(dest.x > transform.position.x)
					xDir = 1;
				else if(dest.x < transform.position.x)
					xDir = -1;
				else
					xDir = 0;

				if(dest.y > transform.position.y)
					yDir = 1;
				else if(dest.y < transform.position.y)
					yDir = -1;
				else
					yDir = 0;
			}
		}
	}

	private bool Arrived()
	{
		return ((xDir > 0 && transform.position.x >= dest.x) ||
				(xDir < 0 && transform.position.x <= dest.x) ||
		         xDir == 0) &&
			   ((yDir > 0 && transform.position.y >= dest.y) ||
		 		(yDir < 0 && transform.position.y <= dest.y) ||
			 	 yDir == 0);
	}
}
