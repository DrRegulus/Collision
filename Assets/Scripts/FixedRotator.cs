using UnityEngine;
using System.Collections;

public class FixedRotator : MonoBehaviour {

	public void Rotate(float angle)
	{
		transform.Rotate (0, 0, angle);
	}
}
