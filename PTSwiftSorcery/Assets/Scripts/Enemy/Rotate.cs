using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	[Tooltip("How fast this rotates")]
	public float m_fRotationSpeed;

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(Vector3.up, m_fRotationSpeed);
	}
}
