using UnityEngine;
using System.Collections;


public class throwableWeapon : MonoBehaviour {
	private Vector3 startPosition;
	private Vector3 clickedPosition;
	private float attackPower; // do we need this?
	private float speed;
	//speed accesstor 
	public float Speed {
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}
	public void throwWeapon(){
	startPosition = this.transform.position;
	clickedPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
	clickedPosition = Camera.main.ScreenToWorldPoint(clickedPosition);
	this.rigidbody2D.velocity = findVec(clickedPosition - startPosition) * this.Speed;
	//Debug.Log (this.Speed);
	}
	//get the direction and normalize the vector
	float findVecDelta(Vector3 vec){
		float vecDelta;
		float x = vec.x;
		float y = vec.y;
		vecDelta = Mathf.Sqrt((x * x) + (y * y));
		return vecDelta;
	}
	Vector3 findVec(Vector3 vec){
		while (findVecDelta(vec) > 1f)
			vec *= 0.01f;
		while (findVecDelta(vec) < 1f)
			vec *= 1.01f;
		return vec;
	}
	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(col.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().Hurt();
			// Destroy the rocket.
			Destroy (gameObject);
		}
		
		// Otherwise if the player manages to shoot himself...
		else if(col.gameObject.tag != "Playe.r" && col.gameObject.tag != "weapon")
		{
			Destroy (gameObject);
		}
		
	}
}