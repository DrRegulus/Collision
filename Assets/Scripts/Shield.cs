using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	void FixedUpdate()
	{
		transform.Rotate (0, 0, -1.0f);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.tag == "Weapon")
		{
			Destroy(col.gameObject);
		}
	}
}
