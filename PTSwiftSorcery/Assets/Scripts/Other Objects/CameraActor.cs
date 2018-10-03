using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraActor : MonoBehaviour {

	[Tooltip("Speed at which the camera will move to follow the player.")]
	[SerializeField] private float m_fLerpSpeed;

	[Tooltip("The magnitude of the movement of the camera as it follows the player.")]
	[SerializeField] [Range(0f, 1f)] private float m_fLerpMagnitude;

	[Tooltip("Magnitude of camera shake. The bigger the value, the larger the shake.")]
	[SerializeField] private float m_fShakeMagnitude;

	[Tooltip("Severity of camera shake.  The bigger the value, the more intense the shake.")]
	[SerializeField] private float m_fShakeRoughness;

	[Tooltip("Time taken to ramp up to full shake.")]
	[SerializeField] private float m_fFadeInTime;

	[Tooltip("Time taken to ramp down from full shake.")]
	[SerializeField] private float m_fFadeOutTime;

	private GameObject m_player;
	private float m_fOriginX;

	// Use this for initialization
	void Start () {

		m_player = GameObject.FindGameObjectWithTag("Player");

		m_fOriginX = transform.parent.transform.localPosition.x;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float desiredX = (m_player.transform.localPosition.x + m_fOriginX) * m_fLerpMagnitude;

		// move to desired position
		transform.parent.transform.localPosition = Vector3.Lerp(transform.parent.transform.localPosition, new Vector3(desiredX, transform.parent.transform.localPosition.y, transform.parent.transform.localPosition.z), m_fLerpSpeed * Time.deltaTime);
		
	}

	public void ShakeCamera() {

		CameraShaker.Instance.ShakeOnce(m_fShakeMagnitude, m_fShakeRoughness, m_fFadeInTime, m_fFadeOutTime);

	}
}
