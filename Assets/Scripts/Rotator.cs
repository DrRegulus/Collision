using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float rotationSpeed = .5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		transform.Rotate (0, 0, rotationSpeed);
	}
}
