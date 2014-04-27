using UnityEngine;
using System.Collections;

public class BoardShip : MonoBehaviour {

	public bool takeoff = false;
	private float distance = 20f;

	void FixedUpdate()
	{
		if (takeoff)
		{
			transform.Translate(0.1f, 0f, 0f);
			distance -= 0.1f;

			if (distance <= 0)
			{
				Application.LoadLevel(Application.loadedLevel + 1);
				//Application.LoadLevel("MainMenu");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			col.transform.parent = transform;
			AliverController aliver = col.transform.GetComponent<AliverController>();
			aliver.Freeze();
			PlayerPrefs.SetInt("Lives", aliver.lives);
			takeoff = true;
		}
	}
}
