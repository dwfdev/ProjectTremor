///<summary>
///		Script Manager:	Denver
///		Description:	Handles the movement of the player using the mouse,
///						the life state of the player and shooting mechancis.
///		Date Modified:	03/10/2018
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
	[SerializeField] private float m_fMouseSensitivity;

	[Tooltip("Lessens jitter. Too high a value makes it unresponsive.")]
	[SerializeField] private float m_fMouseSmoothing;

	[Tooltip("The area in which the player can move.")]
	public GameObject m_movementArea;

	[HideInInspector]
	public LevelSection m_currentSection;

	private Vector3 m_v3MouseSmooth;

	private float m_fMovementBoundsX;
	private float m_fMovementBoundsZ;

	private float m_fDyingTimer;
	private bool m_bDyingTimerIsActive;

	private float m_fRespawnInvincibilityTimer;
	private bool m_bRespawnInvincibilityTimerIsActive;

	private PlayerSpellManager m_spellManager;

	// Use this for initialization
	void Start() {

		// make sure tag is set to 'Player'
		gameObject.tag = "Player";

		// initialise lifeState as NORMAL
		m_lifeState = eLifeState.NORMAL;

		// calculate movement boundaries
		m_fMovementBoundsX = m_movementArea.transform.localScale.x * transform.localScale.x / 2;
		m_fMovementBoundsZ = m_movementArea.transform.localScale.z * transform.localScale.z / 2;

		// Get spell manager
		m_spellManager = GetComponent<PlayerSpellManager>();

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
		v3MouseMovement = Vector3.Scale(v3MouseMovement, new Vector3(m_fMouseSensitivity * (1 / m_movementArea.transform.localScale.x), 0, m_fMouseSensitivity * (1 / m_movementArea.transform.localScale.z)));

		// create a smoothed movement vector to move the player by
		m_v3MouseSmooth.x = Mathf.Lerp(m_v3MouseSmooth.x, v3MouseMovement.x, 1 / m_fMouseSmoothing);
		m_v3MouseSmooth.z = Mathf.Lerp(m_v3MouseSmooth.z, v3MouseMovement.z, 1 / m_fMouseSmoothing);

		// move player
		transform.localPosition += m_v3MouseSmooth * Time.deltaTime;
		#endregion

		#region Keeping Player within Boundaries
		// right side
		if(transform.localPosition.x > m_fMovementBoundsX) {
			transform.localPosition = new Vector3(m_fMovementBoundsX, transform.localPosition.y, transform.localPosition.z);
		}

		// left side
		if(transform.localPosition.x < -m_fMovementBoundsX) {
			transform.localPosition = new Vector3(-m_fMovementBoundsX, transform.localPosition.y, transform.localPosition.z);
		}

		// forward side
		if(transform.localPosition.z > m_fMovementBoundsZ) {
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, m_fMovementBoundsZ);
		}

		// back side
		if(transform.localPosition.z < -m_fMovementBoundsZ) {
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -m_fMovementBoundsZ);
		}
		#endregion

		// shooting
		if (Input.GetAxisRaw("Fire1") > 0) {
			m_spellManager.Fire();
		}

		// if the player is dead
		if(m_lifeState == eLifeState.DEAD) {
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
