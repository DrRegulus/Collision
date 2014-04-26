using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.tag == "Weapon")
		{
			Destroy(col.gameObject);
		}
	}
}
