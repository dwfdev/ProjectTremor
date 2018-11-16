using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorActor : MonoBehaviour {

	[Header("Rotation")]

	[Tooltip("Rotation speed.")]
	[SerializeField] private float m_fRotationSpeed = 350f;
	
	[Header("Bobbing")]

	[Tooltip("Offset.")]
	[SerializeField] private float m_fOffset;

	[Tooltip("Amplitude.")]
	[SerializeField] private float m_fAmplitude = 8f;

	// Update is called once per frame
	void Update () {
		
		// locally rotate object
		transform.Rotate(Vector3.up, m_fRotationSpeed * Time.deltaTime);

		// bob up and down
		transform.localPosition = SinBob();
	}

	Vector3 SinBob() {

		// calculate new y level
		return new Vector3(
			transform.localPosition.x,
			Mathf.Sin(Time.timeSinceLevelLoad) / m_fAmplitude + m_fOffset,
			transform.localPosition.z
		);
	}
}
