using UnityEngine;
using System.Collections;

public class BoardShip : MonoBehaviour {

	public bool takeoff = false;
	private float distance = 20f;

	void Update()
	{
		if (takeoff)
		{
			transform.Translate(0.1f, 0f, 0f);
			distance -= 0.1f;

			if (distance <= 0)
			{
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			col.transform.parent = transform;
			takeoff = true;
		}
	}
}
