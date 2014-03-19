using UnityEngine;
using System.Collections;

public class SteamVent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		float hForce = 1000f;

		if (col.transform.position.x <= transform.position.x)
			hForce *= -1;

		col.rigidbody.rigidbody2D.AddForce (new Vector2 (hForce, 500f));
	}
}
