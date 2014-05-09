using UnityEngine;
using System.Collections;

public class DropBomb : MonoBehaviour {
	public GameObject throwW;
	public float deltaX;


	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col)
	{
		//Damage Aliver on collision and stop moving
		if (col.tag == "Projectile") {
			deltaX = Random.Range (-(transform.localScale.x),(transform.localScale.x));
			Vector3 foo = transform.position;
			foo.x += deltaX * 2;
			Instantiate (throwW, foo, Quaternion.Euler (new Vector3 (0, 0, 0)));

		}
}
}