﻿using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		col.transform.parent = transform;
	}
}
