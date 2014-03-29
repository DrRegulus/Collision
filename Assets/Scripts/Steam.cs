using UnityEngine;
using System.Collections;

public class Steam : MonoBehaviour {
	
	private const float steamWidth = 2f;
	private float currDist = steamWidth;
	private float dir = 1;
	private const float steamPush = 5f;
	public bool collision = false;
	private Transform player;

	void Update()
	{
		if (collision)
		{
			if(currDist < steamPush)
			{
				currDist += .1f;
				player.Translate(dir * 0.1f, 0 , 0);
			}
			else
			{
				collision = false;
				currDist = steamWidth;
				//Enable player movement
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			collision = true;
			player = col.transform;
			//Disable player movement

			if(player.transform.position.x < transform.position.x)
			{
				dir = -1;
			}
			else
			{
				dir = 1;
			}
		}
	}
}
