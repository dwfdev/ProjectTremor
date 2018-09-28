///<summary>
///		Script Manager:	Denver
///		Description:	Handles the movement of the player using the mouse
///		Date Modified:	28/09/2018
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eLifeState {
	DEAD,
	DYING,
	NORMAL,
	SHIELDED,
	INVINCIBLE
}

public class PlayerActor : MonoBehaviour
{
	[Tooltip("Amount of lives the player has.")]
	public int m_nLives;

	[Tooltip("Time, in seconds player has to recover from being hit.")]
	[SerializeField] private float m_fDyingTimerSeconds;

	[Tooltip("Time, in seconds player has invinciblity after respawn.")]
	[SerializeField] private float m_fRespawnInvincibilityTimerSeconds;

	[HideInInspector]
	public eLifeState m_lifeState;

	[Tooltip("Scales raw mouse movement.")]
	[SerializeField] private float fMouseSensitivity;

	[Tooltip("Lessens jitter. Too high a value makes it unresponsive.")]
	[SerializeField] private float fMouseSmoothing;

	private Vector3 v3MouseSmooth;

	private float m_fDyingTimer;
	private bool m_bDyingTimerIsActive;

	private float m_fRespawnInvincibilityTimer;
	private bool m_bRespawnInvincibilityTimerIsActive;

	// Use this for initialization
	void Start() {

		gameObject.tag = "Player";

		m_lifeState = eLifeState.NORMAL;

	}

	// Update is called once per frame
	void Update() {

		#region Timers
		// if player is dying
		if (m_bDyingTimerIsActive) {
			// count down dying timer
			m_fDyingTimer -= Time.deltaTime;
		
			// if timer is up
			if (m_fDyingTimer <= 0f) {
				// deactivate timer
				m_bDyingTimerIsActive = false;

				Respawn();
			}
		}

		// if player has just respawned
		if (m_bRespawnInvincibilityTimerIsActive) {
			// count down invincibility timer
			m_fRespawnInvincibilityTimer -= Time.deltaTime;
		
			// if timer is up
			if (m_fRespawnInvincibilityTimer <= 0f) {
				// return player to normal
				m_lifeState = eLifeState.NORMAL;

				// deactivate timer
				m_bRespawnInvincibilityTimerIsActive = false;
			}
		}

		#endregion

		#region Mouse Movement
		// get the movement of the mouse
		Vector3 v3MouseMovement = new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y"));

		// scale it by sensitivity
		v3MouseMovement = Vector3.Scale(v3MouseMovement, new Vector3(fMouseSensitivity, 0, fMouseSensitivity));

		// create a smoothed movement vector to move the player by
		v3MouseSmooth.x = Mathf.Lerp(v3MouseSmooth.x, v3MouseMovement.x, 1 / fMouseSmoothing);
		v3MouseSmooth.z = Mathf.Lerp(v3MouseSmooth.z, v3MouseMovement.z, 1 / fMouseSmoothing);

		// move player
		transform.Translate(v3MouseSmooth * Time.deltaTime);
		#endregion

		// if the player is dead
		if (m_lifeState == eLifeState.DEAD) {
			// destroy the player
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider info) {

		// check that collision was with a bullet
		if (info.gameObject.tag == "EnemyBullet") {

			Debug.Log(name + " has collided with an EnemyBullet");

			// if player is shielded
			if (m_lifeState == eLifeState.SHIELDED) {
				// set life state to normal
				m_lifeState = eLifeState.NORMAL;
			}
			else if (m_lifeState == eLifeState.NORMAL) {
				// set life state to dying
				m_lifeState = eLifeState.DYING;

				// start dying timer
				m_bDyingTimerIsActive = true;
				m_fDyingTimer = m_fDyingTimerSeconds;
			}
		}
		
	}

	void Respawn() {
		
		// decrement lives
		--m_nLives;

		// if player has no more lives
		if (m_nLives == 0) {
			// player is dead
			m_lifeState = eLifeState.DEAD;
		}
		else {
			// reset poisition

			// give player invinciblity
			m_lifeState = eLifeState.INVINCIBLE;
			
			// start respawn timer
			m_bRespawnInvincibilityTimerIsActive = true;
			m_fRespawnInvincibilityTimer = m_fRespawnInvincibilityTimerSeconds;
		}

	}
}
