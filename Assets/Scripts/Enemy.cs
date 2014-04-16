using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int lives = 1;

	public void Hurt(int damage)
	{
		lives -= damage;
		if(lives <= 0)
		{
			Destroy (gameObject, 2);
		}
	}
}
