using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager: Denver
///		Description:	Handles the functionality of the bomb.
///		Date Modified:	26/10/2018
///</summary>

public class BombActor : MonoBehaviour {

	[Header("Bomb")]
	[Tooltip("How much damage will be dealt to all active enemies.")]
	[SerializeField] private int m_nBombDamage;

	[Tooltip("How much score will be awarded for each bullet destroyed.")]
	[SerializeField] private int m_nBulletScore;

	[Tooltip("Maximum bomb range.")]
	[SerializeField] private float m_fMaximumRange;

	[Tooltip("Time bomb lasts.")]
	[SerializeField] private float m_fDuration;

	[Tooltip("Starting distance.")]
	[SerializeField] [Range(1f, float.MaxValue)] private float m_fStartRange;

	[Header("Camera Shake")]
	[Tooltip("Magnitude of shake.")]
	[SerializeField] private float m_fShakeMagnitude;

	[Tooltip("Roughness of shake.")]
	[SerializeField] private float m_fShakeRoughness;

	[Tooltip("Fade in time of shake.")]
	[SerializeField] private float m_fShakeFadeIn;

	[Tooltip("Fade out time of shake.")]
	[SerializeField] private float m_fShakeFadeOut;

	[Header("Slow Down Time")]
	[Tooltip("By how much do you want to slow down time.")]
	[SerializeField] [Range(0.0001f, 1)] private float m_fTimeMagnitude;

	[Tooltip("How long the effect will take place in seconds.")]
	[SerializeField] private float m_fTimeDuration;

	[HideInInspector]
	public bool m_bIsExploding;

	private float m_fCurrentTime;

	private float m_fChangeInValue;

	private float m_fCurrentRange;

	private float m_fOldRange;

	private PlayerActor m_player;

	private GameObject[] m_bullets;

	private CameraActor m_camera;

	private MeshRenderer m_renderer;

	private SphereCollider m_collider;

	void Start() {

		// get components
		m_renderer = GetComponent<MeshRenderer>();
		m_collider = GetComponent<SphereCollider>();

		// initialise components
		m_renderer.enabled = false;
		m_collider.enabled = false;

		// initialise easing function variables
		m_fCurrentTime = 0f;
		m_fChangeInValue = 0f;
		m_fCurrentRange = m_fStartRange;
		m_fOldRange = 0f;

		// calculate scaled slow down time duration
		m_fTimeDuration *= m_fTimeMagnitude;
	}

	void FixedUpdate() {

		// if bomb should blow up
		if (m_bIsExploding) {
			// enable renderer and collider
			m_renderer.enabled = true;
			m_collider.enabled = true;

			// calculate change in value
			m_fChangeInValue = m_fCurrentRange - m_fOldRange;

			m_fCurrentTime += Time.deltaTime;

			// calcualte current range using easing function
			m_fCurrentRange = EaseOutExpo(m_fCurrentTime, m_fStartRange, m_fChangeInValue, m_fDuration);

			// Clamp range
			m_fCurrentRange = Mathf.Clamp(m_fCurrentRange, m_fStartRange, m_fMaximumRange);

			// display range visually using bomb effect
			transform.localScale = new Vector3(m_fCurrentRange, 0.1f, m_fCurrentRange);
			Debug.Log(m_fCurrentRange);
		}
	}

	float EaseOutExpo(float t, float b, float c, float d) {

		// calculate new range using easing function
		return c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
	}

	public void StartBomb() {

		// invoke stop bomb
		Invoke("StopBomb", m_fDuration);

		// shake camera
		CameraActor cameraActor = GameObject.FindObjectOfType<CameraActor>();
		cameraActor.ShakeCamera(m_fShakeMagnitude, m_fShakeRoughness, m_fShakeFadeIn, m_fShakeFadeOut);

		// slow down time
		StartCoroutine(SlowDownTime());

		m_bIsExploding = true;
	}

	void StopBomb() {

		// set blow up to false
		m_bIsExploding = false;

		// disable components
		m_renderer.enabled = false;
		m_collider.enabled = false;

		// reset easing function variables
		m_fCurrentTime = 0f;
		m_fChangeInValue = 0f;
		m_fCurrentRange = m_fStartRange;
		m_fOldRange = 0f;

		// reset bomb effect
		transform.localScale = new Vector3(1, 0.1f, 1);

		Debug.Log("Bomb has stopped");
	}

	IEnumerator SlowDownTime() {

		// set time scale
		Time.timeScale = m_fTimeMagnitude;

		// scale fixedDeltaTime
		Time.fixedDeltaTime = 0.02f * Time.timeScale;

		yield return new WaitForSeconds(m_fTimeDuration);

		// reset
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;
	}

	void OnTriggerEnter(Collider other) {

		// check that other is an EnemyBullet
		if (other.tag == "EnemyBullet") {
			ScoreManager.Instance.AddScore(m_nBulletScore);
			Destroy(other.gameObject);
		}
	}

	void OnTriggerStay(Collider other) {

		// check that other is an Enemy
		if (other.tag == "Enemy") {
			other.gameObject.GetComponent<EnemyActor>().TakeDamage(m_nBombDamage);
		}
	}
}
