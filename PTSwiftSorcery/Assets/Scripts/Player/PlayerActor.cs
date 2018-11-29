using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

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

[RequireComponent(typeof(PlayerSpellManager))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerActor : MonoBehaviour {

	#region Member Variables
	[Header("Bomb.")]
	[Tooltip("Initial amount of bombs.")]
	[SerializeField] private int m_nInitialBombCount;

	[Tooltip("Maximum amount of bombs the player can have.")]
	[SerializeField] private int m_nMaximumBombCount;

	[Header("Timers.")]

	[Tooltip("Time, in seconds player has invinciblity after respawn.")]
	[SerializeField] private float m_fRespawnInvincibilityTimerSeconds;

	[Header("Mouse Input.")]
	
	private float m_fMouseSensitivity;

	[Tooltip("Lessens jitter. Too high a value makes it unresponsive.")]
	[SerializeField] private float m_fMouseSmoothing;

	[Header("Hit Juicing")]

	[Tooltip("Number of freeze frames.")]
	[SerializeField] private int m_nNumOfFreezeFrames;

	[Tooltip("Magnitude of camera shake.")]
	[SerializeField] private float m_fHitShakeMagnitude;

	[Tooltip("Roughness of camera shake.")]
	[SerializeField] private float m_fHitShakeRoughness;

	[Tooltip("Fade in time of camera shake.")]
	[SerializeField] private float m_fHitShakeFadeIn = -1f;

	[Tooltip("Fade out time of camera shake.")]
	[SerializeField] private float m_fHitShakeFadeOut = -1f;

	[HideInInspector]
	public GameObject m_movementArea;

	[Tooltip("Bomb Actor Prefab.")]
	[SerializeField] private GameObject m_bombActorPrefab;

	[Tooltip("HitBox Indicator.")]
	[SerializeField] private GameObject m_hitBoxIndicator;

	[HideInInspector]
	public int m_nLives;

	private int m_nMaximumLives;
	
	[HideInInspector]
	public eLifeState m_lifeState;

	[HideInInspector]
	public LevelSection m_currentSection;

	private Vector3 m_v3MouseSmooth;

	private float m_fMovementBoundsX;
	private float m_fMovementBoundsZ;

	private bool m_bHasBombSaved;

	private float m_fRespawnInvincibilityTimer;
	private bool m_bRespawnInvincibilityTimerIsActive;

	[HideInInspector]
	public PlayerSpellManager m_spellManager;

	private Animator m_animator;
	private float m_fAttackAnimWeight;

	[HideInInspector]
	public bool m_bHasPickUp;

	[HideInInspector]
	public bool m_bCanMove;

	[HideInInspector]
	public int m_nCurrentBombCount;
	#endregion

	// Use this for initialization
	void Start() {

		// guarantee collider and rb settings
		GetComponent<Collider>().isTrigger = true;
		GetComponent<Rigidbody>().isKinematic = true;

		// if current difficulties num of lives has been set
		if (SceneManager.Instance.CurrentDifficulty.numOfLives > 0) {
			// set lives
			m_nMaximumLives = SceneManager.Instance.CurrentDifficulty.numOfLives;
			m_nLives = m_nMaximumLives;
		}
		else {
			// send debug message
			Debug.LogWarning("Couldn't find current difficulties number of lives, defaulting to 3.", gameObject);

			// default to 3
			m_nMaximumLives = 3;
			m_nLives = 3;
		}

		// if scene manager has a mouse sensitivity
		if (SceneManager.Instance.MouseSensitivity > 0) {
			// set mouse sensitivity
			m_fMouseSensitivity = SceneManager.Instance.MouseSensitivity;
		}
		else {
			// send warning
			Debug.LogWarning("Couldn't find mouse sensitivity, defaulting to 0.25", gameObject);

			// default to 0.25
			m_fMouseSensitivity = 0.25f;
		}

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

		// get spell manager
		m_spellManager = GetComponent<PlayerSpellManager>();

		// get animator
		m_animator = GetComponent<Animator>();

		// turn off hit box indicator renderer
		m_hitBoxIndicator.GetComponent<MeshRenderer>().enabled = false;

		// set bomb count
		m_nCurrentBombCount = m_nInitialBombCount;

		// initialise hasPowerUp to false
		m_bHasPickUp = false;

		m_bCanMove = true;
	}

	// Update is called once per frame
	void Update() {

		#region Timers
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

			// start attacking in animator
			m_animator.SetBool("IsAttacking", true);
		}

		if (Input.GetButtonUp("Fire1")) {
			m_spellManager.StopFiring();

			// stop attacking in animator
			m_animator.SetBool("IsAttacking", false);
		}

		// bomb
		if (Input.GetButtonDown("Fire2")) {
			if (m_nCurrentBombCount > 0) {
				// stop player from dying
				if (m_lifeState == eLifeState.DYING) {
					m_lifeState = eLifeState.NORMAL;
					m_nCurrentBombCount = 1;
					m_bHasBombSaved = true;
				}

				ShootBomb();
			}
		}

		// show hit box indicator
		if (Input.GetButtonDown("ShowHitBox")) {
			MeshRenderer renderer = m_hitBoxIndicator.GetComponent<MeshRenderer>();
			renderer.enabled = !renderer.enabled;
		}

		// pause
		if (Input.GetButtonDown("Pause")) {
			SceneManager.Instance.SceneState = eSceneState.PAUSED;
		}
		#endregion
		
		#region Mouse Movement
		if (m_bCanMove) {
			// get the movement of the mouse
			Vector3 v3MouseMovement = new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y"));

			// animate movement
			m_animator.SetFloat("hInput", v3MouseMovement.x);

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
			SceneManager.Instance.SceneState = eSceneState.FAILED;
		}
	}

	void OnTriggerEnter(Collider other) {

		// check that collision was with a bullet...
		if (other.gameObject.tag == "EnemyBullet") {

			// ...and that the bullet was active
			if(other.gameObject.GetComponent<EnemySpellProjectile>().GetActive()) {

				// if player isn't invincible
				if(m_lifeState != eLifeState.INVINCIBLE && m_lifeState != eLifeState.DYING) {

					// shake camera
					CameraActor cameraActor = FindObjectOfType<CameraActor>();
					cameraActor.ShakeCamera(m_fHitShakeMagnitude, m_fHitShakeRoughness, m_fHitShakeFadeIn, m_fHitShakeFadeOut);

					//if player is shielded
					if(m_lifeState == eLifeState.SHIELDED) {
						// set life state to normal
						m_lifeState = eLifeState.NORMAL;
					}
					else if(m_lifeState == eLifeState.NORMAL) {
						// set life state to dying
						m_lifeState = eLifeState.DYING;

						// freeze frames
						StartCoroutine(FreezeFrames(m_nNumOfFreezeFrames));
					}
				}
			}
		}
	}

	IEnumerator FreezeFrames(int numOfFrames) {

		// freeze game
		Time.timeScale = 0f;

		// freeze player
		m_bCanMove = false;
		
		int i = 0;

		while (i < numOfFrames) {
			// wait for next frame
			yield return new WaitForEndOfFrame();

			// increment iterations
			i++;
		}

		// unfreeze player
		m_bCanMove = true;

		// if player hasn't used the bomb to save themselves
		if (!m_bHasBombSaved) {
			
			// reset time scale
			Time.timeScale = 1f;
			
			Respawn();
		}
		else {
			// reset boolean
			m_bHasBombSaved = false;
		}
	}

	void Respawn() {
		
		// decrement lives
		--m_nLives;

		// if player has no more lives
		if (m_nLives == 0) {
			// player is dead
			m_lifeState = eLifeState.DEAD;

			// reset score
			ScoreManager.Instance.ResetScore();
		}
		else {
			// give player invinciblity
			m_lifeState = eLifeState.INVINCIBLE;
			StartInvincibilityTimer(m_fRespawnInvincibilityTimerSeconds);
		}
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
		if (m_nLives + adder <= m_nMaximumLives) {
			m_nLives += adder;
		}
	}

	void ShootBomb() {

		// check that player has bombs
		if(m_nCurrentBombCount > 0) {
			// decrement bomb count
			--m_nCurrentBombCount;

			// create bomb
			BombActor bombActor = Instantiate(m_bombActorPrefab, transform.position, Quaternion.identity).GetComponent<BombActor>();
			
			// parent bomb to Playfield
			bombActor.transform.parent = GameObject.FindGameObjectWithTag("Playfield").transform;
			
			// Start bomb
			bombActor.StartBomb();
		}
	}
}
