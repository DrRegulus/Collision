using UnityEngine;
using System.Collections;

public class BoardShip : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			col.transform.parent = transform;
			AliverController aliver = col.transform.GetComponent<AliverController>();
			aliver.Freeze();
			PlayerPrefs.SetInt("Lives", aliver.lives);
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
