using UnityEngine;
using System.Collections;

public class Steam : MonoBehaviour {
	
	private const float steamWidth = 2f;
	private float currDist = steamWidth;
	private float dir = 1;
	private const float steamPush = 4f;
	private bool collision = false;
	private Transform player;

	void Update()
	{
		if (collision)
		{
			//Translate player a set distance, then unfreeze
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
				player.GetComponent<AliverController>().Unfreeze();
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
			player.GetComponent<AliverController>().Freeze();
			//player.GetComponent<AliverController>().anim.Play();

			//Set direction based on relative positions
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
