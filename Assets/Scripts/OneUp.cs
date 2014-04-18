using UnityEngine;
using System.Collections;

public class OneUp : MonoBehaviour {


	void FixedUpdate()
	{
		transform.Rotate (0, 4, 0);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			col.GetComponent<AliverController>().GainLife();
			Destroy(gameObject);
		}
	}
}
