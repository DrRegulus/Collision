using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
