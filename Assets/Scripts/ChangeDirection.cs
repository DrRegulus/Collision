using UnityEngine;
using System.Collections;

public class ChangeDirection : MonoBehaviour {

	public float maxSpeed = 20f;
	public Transform Boss;
	private Transform[] neighbors;
	
	public Transform dest;
	private Transform nextDest;
	public Vector2 currVel;
	private Vector2 nextVel;

	// Use this for initialization
	void FixedUpdate() {

		if(currVel.y > 0 && transform.position.y > dest.position.y ||
		   currVel.y < 0 && transform.position.y < dest.position.y ||
		   currVel.x > 0 && transform.position.x > dest.position.x ||
		   currVel.x < 0 && transform.position.x < dest.position.x)
		{
			currVel = nextVel;
			dest = nextDest;
		}

		Boss.rigidbody2D.velocity = currVel;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "ChangeDir"){
			neighbors = col.gameObject.GetComponent<GetNeighbors>().GetNeighborArray();
			nextDest = neighbors[Random.Range(0, neighbors.Length)];

			if(nextDest.position.y > dest.position.y)
			{
				nextVel = new Vector2(0, maxSpeed);
			}
			else if(nextDest.position.x > dest.position.x)
			{
				nextVel = new Vector2(maxSpeed, 0);
			}
			else if(nextDest.position.y < dest.position.y)
			{
				nextVel = new Vector2(0, -maxSpeed);
			}
			else if(nextDest.position.x < dest.position.x)
			{
				nextVel = new Vector2(-maxSpeed, 0);
			}
		}
	}
}
