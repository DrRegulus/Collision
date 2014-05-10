using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float lives = 1f;
	public bool alive = true;

	public float Lives {
		get {
			return lives;
		}
		set {
			lives = value;
		}
	}
	public void Hurt(int damage)
	{
		lives -= damage;
		if(lives <= 0)
		{
			alive = false;
		}
	}
}
