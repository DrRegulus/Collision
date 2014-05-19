using UnityEngine;
using System.Collections;

public class DropBomb : MonoBehaviour {
	public GameObject throwW;
	public float deltaX;
	private float shootTime = 0;
	private float cooldown = 2f;
	
	// Use this for initialization
	void FixedUpdate() 
	{
		//Damage Aliver on collision and stop moving
		if(Time.time - shootTime > cooldown){
			deltaX = Random.Range (-(transform.localScale.x),(transform.localScale.x));
			Vector3 foo = transform.position;
			foo.x += deltaX * 2;
			Instantiate (throwW, foo, Quaternion.Euler (new Vector3 (0, 0, 0)));
			shootTime = Time.time;
		}
	}
}