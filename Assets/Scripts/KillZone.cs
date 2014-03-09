using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			GameOver ();
		}
		else
			Destroy (col.gameObject);
	}
	
	public void GameOver()
	{
		Application.LoadLevel ("GameOver");
	}
}
