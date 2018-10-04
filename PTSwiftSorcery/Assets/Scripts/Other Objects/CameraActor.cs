///<summary>
///		Script Manager:	Denver
///		Description:	Handles the movement of the camera
///		Date Modified:	04/10/2018
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraActor : MonoBehaviour {

	[Tooltip("Speed at which the camera will move to follow the player.")]
	[SerializeField] private float m_fLerpSpeed;

	[Tooltip("How far the camera can move to follow the player.")]
	[SerializeField] [Range(0f, 1f)] private float m_fLerpRange;

	[Tooltip("Magnitude of camera shake. The bigger the value, the larger the shake.")]
	[SerializeField] private float m_fDefaultShakeMagnitude;

	[Tooltip("Severity of camera shake.  The bigger the value, the more intense the shake.")]
	[SerializeField] private float m_fDefaultShakeRoughness;

	[Tooltip("Time taken to ramp up to full shake.")]
	[SerializeField] private float m_fDefaultFadeInTime;

	[Tooltip("Time taken to ramp down from full shake.")]
	[SerializeField] private float m_fDefaultFadeOutTime;

	private GameObject m_player;
	private float m_fOriginX;

	// Use this for initialization
	void Start () {

		m_player = GameObject.FindGameObjectWithTag("Player");

		m_fOriginX = transform.parent.transform.localPosition.x;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float desiredX = (m_player.transform.localPosition.x + m_fOriginX) * m_fLerpRange;

		// move to desired position
		transform.parent.transform.localPosition = Vector3.Lerp(transform.parent.transform.localPosition, new Vector3(desiredX, transform.parent.transform.localPosition.y, transform.parent.transform.localPosition.z), m_fLerpSpeed * Time.deltaTime);
		
	}

	public void ShakeCamera(float shakeMagnitude = 0, float shakeRougness = 0, float fadeInTime = float.MinValue, float fadeOutTime = float.MinValue) {

		// check to see if no values were inputted
		if (shakeMagnitude == 0) {
			shakeMagnitude = m_fDefaultShakeMagnitude;
		}

		if (shakeRougness == 0) {
			shakeRougness = m_fDefaultShakeRoughness;
		}

		if (fadeInTime == float.MinValue) {
			fadeInTime = m_fDefaultFadeInTime;
		}

		if (fadeOutTime == float.MinValue) {
			fadeOutTime = m_fDefaultFadeOutTime;
		}

		// shake camera
		CameraShaker.Instance.ShakeOnce(shakeMagnitude, shakeRougness, fadeInTime, fadeOutTime);

	}
}
