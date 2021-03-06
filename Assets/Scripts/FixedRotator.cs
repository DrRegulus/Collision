﻿using UnityEngine;
using System.Collections;

public class FixedRotator : MonoBehaviour {

	public enum Direction{
		CLOCKWISE = -1,
		COUNTERCLOCKWISE = 1
	};

	public bool alwaysRotate = true;
	public float rotationInterval = 15.0f;
	public Direction dir = FixedRotator.Direction.CLOCKWISE;
	private float rotationCount = 0;
	
	void FixedUpdate()
	{
		if(alwaysRotate)
		{
			if(rotationCount < rotationInterval)
			{
				Rotate (dir);
				rotationCount++;
			}
			else if(rotationCount < 2 * rotationInterval)
			{
				rotationCount++;
			}
			else
			{
				rotationCount = 0;
			}
		}
	}

	public void Rotate(Direction direc)
	{
		transform.Rotate (0, 0, (float)direc);
	}

	public void Rotate(Direction direc, float angle)
	{
		transform.Rotate (0, 0, (float)direc * angle);
	}
}
