using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int lives = 1;
	public bool alive = true;

	public void Hurt(int damage)
	{
		lives -= damage;
		if(lives <= 0)
		{
			alive = false;
		}
	}
}
