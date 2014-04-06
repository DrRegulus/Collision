using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int lives = 1;

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D obj = col.collider;
		
		if(obj.tag == "Player")
		{
			obj.GetComponent<AliverController>().Recoil(0);
		}
		else if(obj.tag == "Powered" || obj.tag == "Weapon")
		{
			UnityEngine.Debug.Log("Destroyed!");
			Destroy(gameObject);
		}
	}

	public void Hurt(int damage)
	{
		lives -= damage;
		if(lives <= 0)
		{
			Destroy (gameObject);
		}
	}
}
