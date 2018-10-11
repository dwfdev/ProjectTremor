using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles Player inupts and life state.
///		Date Modified:	11/10/2018
///</summary>

public enum eLifeState {
	DEAD,
	DYING,
	NORMAL,
	SHIELDED,
	INVINCIBLE
}

public class PlayerActor : MonoBehaviour
{

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

	[Tooltip("The area in which the player can move.")]
	public GameObject m_movementArea;
	
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

	private PlayerSpellManager m_spellManager;

	[HideInInspector]
	public sPickUp m_currentPickUp;

	[HideInInspector]
	public bool m_bHasPickUp;

	private bool m_bFire2Down;

	private bool m_bFire3Down;

	private BombActor m_bomb;

	[HideInInspector]
	public int m_nCurrentBombCount;
	#endregion

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

		// set up bomb actor
		m_bomb = GetComponent<BombActor>();

		// set bomb count
		m_nCurrentBombCount = m_nInitialBombCount;

		// initialise hasPowerUp to false
		m_bHasPickUp = false;

		m_bFire2Down = false;

		m_bFire3Down = false;

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
		// activate pickup
		if (Input.GetAxis("Fire3") > 0 && m_bHasPickUp && !m_bFire3Down) {
			m_bFire3Down = true;
			StartCoroutine(ActivatePickUp());
		}
		
		if (Input.GetAxis("Fire3") == 0) {
			m_bFire3Down = false;
		}

		// pick spell type
		// FIRE
		if (Input.GetAxis("FireSwitch") > 0) {
			m_spellManager.m_eSpellType = eSpellType.FIRE;
		}

		// ICE
		if (Input.GetAxis("IceSwitch") > 0) {
			m_spellManager.m_eSpellType = eSpellType.ICE;
		}

		// LIGHTNING
		if (Input.GetAxis("LightningSwitch") > 0) {
			m_spellManager.m_eSpellType = eSpellType.LIGHTNING;
		}

		// shooting
		// normal spells
		if(Input.GetAxis("Fire1") > 0) {
			m_spellManager.Fire();
		}

		// bomb
		if (Input.GetAxis("Fire2") > 0 && !m_bFire2Down) {
			m_bFire2Down = true;
			ShootBomb();
		}
		
		if (Input.GetAxis("Fire2") == 0) {
			m_bFire2Down = false;
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
		transform.localPosition += m_v3MouseSmooth;
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
		}

	}

	void OnTriggerEnter(Collider other) {

		// check that collision was with a bullet...
		if (other.gameObject.tag == "EnemyBullet") {
			// ...and that the bullet was active
			if(other.gameObject.GetComponent<EnemySpellProjectile>().GetActive()) {

				Debug.Log(m_lifeState.ToString());

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

	public void StartDyingTimer()
	{

		// start dying timer
		m_bDyingTimerIsActive = true;
		m_fDyingTimer = m_fDyingTimerSeconds;

	}

	public void StartInvincibilityTimer(float duration)
	{

		// start respawn timer
		m_bRespawnInvincibilityTimerIsActive = true;
		m_fRespawnInvincibilityTimer = duration;

	}

	public void AddToPlayerBombCount(int adder)
	{

		// add adder to m_nCurrentBombCount
		m_nCurrentBombCount += adder;

		// clamp between 0 and maximum bomb count
		m_nCurrentBombCount = Mathf.Clamp(m_nCurrentBombCount, 0, m_nMaximumBombCount);

	}

	public void AddLives(int adder) {

		// add adder to m_nLives
		m_nLives += adder;

		// clamp between 0 and maxium lives
		m_nLives = Mathf.Clamp(m_nLives, 0, m_nMaximumLives);

	}

	public void SetPickUp(sPickUp newPickUp) {
		
		// if power up takes immediate effect
		if (newPickUp.type == ePickUpType.BOMB) {
			AddToPlayerBombCount(1);
		}

		if (newPickUp.type == ePickUpType.SHIELD) {
			m_lifeState = eLifeState.SHIELDED;
		}

		// if it must be activated manually by the player
		else {
			m_currentPickUp = newPickUp;

			// set HasPickUp to true
			m_bHasPickUp = true;
		}

	}

	IEnumerator ActivatePickUp()
	{

		Debug.Log(m_currentPickUp.type + " was activated.");

		// run pickups code based on its type
		switch(m_currentPickUp.type) {
			case ePickUpType.INCREASE_FIRE_RATE:
				// affect spell manager
				m_spellManager.m_fFireRate /= m_currentPickUp.magnitude;

				// clear m_currentPickUp
				m_bHasPickUp = false;

				// wait for duration
				yield return new WaitForSeconds(m_currentPickUp.duration);

				// reset
				m_spellManager.m_fFireRate *= m_currentPickUp.magnitude;
				Debug.Log(m_currentPickUp.type + " was deactivated.");
				break;

			case ePickUpType.HOMING_SPELLS:
				// affect spell manager
				m_spellManager.m_bIsHoming = true;

				// clear m_currentPickUp
				m_bHasPickUp = false;

				// wait for duration
				yield return new WaitForSeconds(m_currentPickUp.duration);

				// reset
				m_spellManager.m_bIsHoming = false;
				Debug.Log(m_currentPickUp.type + " was deactivated.");
				break;

			case ePickUpType.SCATTER_SPELLS:
				// affect spell manager
				m_spellManager.m_bIsScatter = true;

				// clear m_currentPickUp
				m_bHasPickUp = false;

				// wait for duration
				yield return new WaitForSeconds(m_currentPickUp.duration);

				// reset
				m_spellManager.m_bIsScatter = false;
				Debug.Log(m_currentPickUp.type + " was deactivated.");
				break;

			case ePickUpType.IMMUNITY:
				// clear m_currentPickUp
				m_bHasPickUp = false;

				// affect player
				StartCoroutine(BecomeImmune(m_lifeState, m_currentPickUp.duration));
				break;

			case ePickUpType.SLOW_DOWN_TIME:
				// affect time scale
				Time.timeScale = m_currentPickUp.magnitude;

				// scale fixed delta time
				Time.fixedDeltaTime = 0.02f * Time.timeScale;

				// clear m_currentPickUp
				m_bHasPickUp = false;

				// wait for duration
				yield return new WaitForSeconds(m_currentPickUp.duration);

				// reset
				Time.timeScale = 1;
				Time.fixedDeltaTime = 0.02f;
				Debug.Log(m_currentPickUp.type + " was deactivated.");
				break;

			default:
				Debug.LogError("Could not activate pick up.");

				// clear m_currentPickUp
				m_bHasPickUp = false;
				break;
		}

	}

	IEnumerator BecomeImmune(eLifeState returnLifeState, float duration) {
		
		// turn player invincible
		m_lifeState = eLifeState.INVINCIBLE;

		// wait for duration
		yield return new WaitForSeconds(duration);

		// reset
		m_lifeState = returnLifeState;

	}

	void ShootBomb()
	{

		// check that player has bombs
		if(m_nCurrentBombCount > 0) {
			// blow up bomb
			m_bomb.Boom();

			// decrement bomb count
			--m_nCurrentBombCount;
		}

	}


}
