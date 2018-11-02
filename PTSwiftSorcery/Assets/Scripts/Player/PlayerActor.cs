using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles Player inupts and life state.
///		Date Modified:	1/11/2018
///</summary>

public enum eLifeState {
	DEAD,
	DYING,
	NORMAL,
	SHIELDED,
	INVINCIBLE
}

public class PlayerActor : MonoBehaviour {

	#region Member Variables
	[Header("Lives.")]
	[Tooltip("Amount of lives the player has.")]
	public int m_nLives;

	[Tooltip("Maximum amount of lives.")]
	[SerializeField] private int m_nMaximumLives;

	[Header("Bomb.")]
	[Tooltip("Initial amount of bombs.")]
	[SerializeField] private int m_nInitialBombCount;

	[Tooltip("Maximum amount of bombs the player can have.")]
	[SerializeField] private int m_nMaximumBombCount;

	[Header("Timers.")]
	[Tooltip("Time, in seconds player has to recover from being hit.")]
	[SerializeField] private float m_fDyingTimerSeconds;

	[Tooltip("Time, in seconds player has invinciblity after respawn.")]
	[SerializeField] private float m_fRespawnInvincibilityTimerSeconds;

	[Header("Mouse Input.")]
	[Tooltip("Scales raw mouse movement.")]
	[SerializeField] private float m_fMouseSensitivity;

	[Tooltip("Lessens jitter. Too high a value makes it unresponsive.")]
	[SerializeField] private float m_fMouseSmoothing;

	[Header("")]

	[Tooltip("Position where the player will respawn.")]
	[SerializeField] private Vector3 m_v3RespawnLocation;

	[HideInInspector]
	public GameObject m_movementArea;

	[Tooltip("Bomb Actor Prefab.")]
	[SerializeField] private GameObject m_bombActorPrefab;
	
	[HideInInspector]
	public eLifeState m_lifeState;

	[HideInInspector]
	public LevelSection m_currentSection;

	private Vector3 m_v3MouseSmooth;

	private float m_fMovementBoundsX;
	private float m_fMovementBoundsZ;

	private float m_fDyingTimer;
	private bool m_bDyingTimerIsActive;

	private float m_fRespawnInvincibilityTimer;
	private bool m_bRespawnInvincibilityTimerIsActive;

	[HideInInspector]
	public PlayerSpellManager m_spellManager;

	[HideInInspector]
	public bool m_bHasPickUp;

	[HideInInspector]
	public bool m_bCanMove;

	[HideInInspector]
	public int m_nCurrentBombCount;
	#endregion

	// Use this for initialization
	void Start() {

		// get movementArea
		m_movementArea = GameObject.FindGameObjectWithTag("PlayerMovementArea");

		// check player tag
		if (gameObject.tag != "Player") {
			Debug.LogError("Tag is not Player", gameObject);
		}

		// check movement area tag
		if (m_movementArea.tag != "PlayerMovementArea") {
			Debug.LogError("Tag is not PlayerMovementArea", gameObject);
		}

		// initialise lifeState as NORMAL
		m_lifeState = eLifeState.NORMAL;

		// calculate movement boundaries
		m_fMovementBoundsX = m_movementArea.GetComponent<BoxCollider>().size.x / 2;
		m_fMovementBoundsZ = m_movementArea.GetComponent<BoxCollider>().size.z / 2;

		// Get spell manager
		m_spellManager = GetComponent<PlayerSpellManager>();

		// set bomb count
		m_nCurrentBombCount = m_nInitialBombCount;

		// initialise hasPowerUp to false
		m_bHasPickUp = false;

		m_bCanMove = true;
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

		#region Other Inputs
		// pick spell type
		// FIRE
		if (Input.GetButtonDown("FireSwitch")) {
			m_spellManager.m_eSpellType = eSpellType.FIRE;
			m_spellManager.StopFiring();
		}

		// ICE
		if (Input.GetButtonDown("IceSwitch")) {
			m_spellManager.m_eSpellType = eSpellType.ICE;
			m_spellManager.StopFiring();
		}

		// LIGHTNING
		if (Input.GetButtonDown("LightningSwitch")) {
			m_spellManager.m_eSpellType = eSpellType.LIGHTNING;
			m_spellManager.StopFiring();
		}

		// shooting
		// normal spells
		if(Input.GetButton("Fire1") && m_bCanMove) {
			m_spellManager.Fire();
		}

		if (Input.GetButtonUp("Fire1")) {
			m_spellManager.StopFiring();
		}

		// bomb
		if (Input.GetButtonDown("Fire2")) {
			// stop player from dying
			if (m_lifeState == eLifeState.DYING) {
				m_lifeState = eLifeState.NORMAL;
				m_bDyingTimerIsActive = false;
				m_nCurrentBombCount = 1;
			}

			ShootBomb();
		}

		// pause
		if (Input.GetKeyDown(KeyCode.P)) {
			SceneManager.Instance.SceneState = eSceneState.PAUSED;
		}
		#endregion
		
		#region Mouse Movement
		if (m_bCanMove) {
			// get the movement of the mouse
			Vector3 v3MouseMovement = new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y"));

			// scale it by sensitivity
			v3MouseMovement = Vector3.Scale(v3MouseMovement, new Vector3(m_fMouseSensitivity * (1 / m_movementArea.transform.localScale.x), 0, m_fMouseSensitivity * (1 / m_movementArea.transform.localScale.z)));

			// create a smoothed movement vector to move the player by
			m_v3MouseSmooth.x = Mathf.Lerp(m_v3MouseSmooth.x, v3MouseMovement.x, 1 / m_fMouseSmoothing);
			m_v3MouseSmooth.z = Mathf.Lerp(m_v3MouseSmooth.z, v3MouseMovement.z, 1 / m_fMouseSmoothing);

			// move player
			transform.localPosition += m_v3MouseSmooth;
		}
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

		// if the player is dead
		if(m_lifeState == eLifeState.DEAD) {
			Debug.Log("Player is DEAD!");
			SceneManager.Instance.SceneState = eSceneState.FAILED;
		}
	}

	void OnTriggerEnter(Collider other) {

		// check that collision was with a bullet...
		if (other.gameObject.tag == "EnemyBullet") {
			// ...and that the bullet was active
			if(other.gameObject.GetComponent<EnemySpellProjectile>().GetActive()) {
				// if player isn't invincible
				if(m_lifeState != eLifeState.INVINCIBLE) {
					Debug.Log(name + " has collided with an EnemyBullet");

					//if player is shielded
					if(m_lifeState == eLifeState.SHIELDED) {
						// set life state to normal
						m_lifeState = eLifeState.NORMAL;
					}
					else if(m_lifeState == eLifeState.NORMAL) {
						// set life state to dying
						m_lifeState = eLifeState.DYING;
						StartDyingTimer();
					}
				}
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
			// move the player to the respawn position
			transform.localPosition = m_v3RespawnLocation;

			// give player invinciblity
			m_lifeState = eLifeState.INVINCIBLE;
			StartInvincibilityTimer(m_fRespawnInvincibilityTimerSeconds);
		}
	}

	public void StartDyingTimer() {

		// start dying timer
		m_bDyingTimerIsActive = true;
		m_fDyingTimer = m_fDyingTimerSeconds;
	}

	public void StartInvincibilityTimer(float duration) {

		// start respawn timer
		m_bRespawnInvincibilityTimerIsActive = true;
		m_fRespawnInvincibilityTimer = duration;
	}

	public void AddToPlayerBombCount(int adder) {

		// add adder to m_nCurrentBombCount
		if(m_nCurrentBombCount + adder <= m_nMaximumBombCount) {
			m_nCurrentBombCount += adder;
		}
	}

	public void AddLives(int adder) {

		// add adder to m_nLives
		m_nLives += adder;

		// clamp between 0 and maxium lives
		m_nLives = Mathf.Clamp(m_nLives, 0, m_nMaximumLives);
	}

	void ShootBomb() {

		// check that player has bombs
		if(m_nCurrentBombCount > 0) {
			// decrement bomb count
			--m_nCurrentBombCount;

			BombActor bombActor = Instantiate(m_bombActorPrefab, transform.position, Quaternion.identity).GetComponent<BombActor>();
			bombActor.transform.parent = GameObject.FindGameObjectWithTag("Playfield").transform;
			bombActor.StartBomb();
		}
	}
}
