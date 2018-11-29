///<summary>
///Script Manager: Drake
///Description:
///Handles enemy movement AI, health, and score value. 
///</summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyAIType
{
	STATIC,
	FOLLOW_PLAYER,
	FOLLOW_WAYPOINT
};

[RequireComponent(typeof(UIHitEffect))]
public class EnemyActor : MonoBehaviour
{
	[Header("Global Variables")]
	[Tooltip("The maximum health this enemy has")]
	public int m_nHealth;

	[HideInInspector]
	public int m_nCurrentHealth;

	[Tooltip("Whether or not this enemy should rotate to face the player")]
	[SerializeField] protected bool m_bTrackPlayer;

	[Tooltip("How fast this enemy should rotate, if 0, will not rotate")]
	[SerializeField] protected float m_fRotationSpeed;

	[Tooltip("What type of AI this enemy should have")]
	[SerializeField] protected eEnemyAIType m_enemyAIType;

	//Whether or not this enemy is currently active
	[HideInInspector]
	public bool m_bIsActive;

	//Whether or not this enemy is currently alive
	[HideInInspector]
	public bool m_bIsAlive;

	[HideInInspector]
	public bool m_bIsShooting;

	//The current level section of this enemy
	[HideInInspector]
	public LevelSection m_section;

	//Reference to the player
	private GameObject m_player;

	[Tooltip("Maximum speed the enemy will move at")]
	[SerializeField] protected float m_fMaxMovementSpeed;

	[Tooltip("How smoothed the enemy's movement will be, less is more smoothed")]
	[SerializeField] protected float m_fMovementSmoothing;

	[Header("Follow Player Variables")]
	[Tooltip("How far this enemy should try and stay from the player")]
	[SerializeField] protected Vector3 m_v3Offset;

	//The current speed of the enemy
	private Vector3 m_v3Velocity;

	[Header("Follow Waypoint Variables")]
	[Tooltip("The transforms of the waypoints this enemy should go between, should have the same size as Delays")]
	[SerializeField] protected Transform[] m_waypoints;

	[Tooltip("How long this enemy should spend at each waypoint, should have the same size as Waypoints")]
	[SerializeField] protected float[] m_delays;

	[Tooltip("Whether or not the enemy should loop through the waypoints, or if it should just go through them once")]
	[SerializeField] protected bool m_bLoopWaypoints;

	//The current/previous waypoint the enemy was at
	private int m_nCurrentWaypoint = 0;

	//The waypoint the enemy is moving towards
	private int m_nDesiredWaypoint = 0;
	
	//Timer in seconds
	private float m_fTimer;
	
	//Whether or not the enemy is currently waiting at a waypoint
	private bool m_bWaitingAtWaypoint;

	[Header("Score")]
	[Tooltip("The score value of this enemy immediately upon being killed")]
	public long m_lRawScore;

	[Tooltip("The score value of the pickups this enemy drops upon death")]
	public long m_lPickupScore;

	[Tooltip("How much additional multiplier this enemy is worth")]
	public float m_fMultiplier;

	[Header("Death")]
	[Tooltip("Fire Death Particle System")]
	public GameObject m_fireDeathPS;

	[Tooltip("Ice Death Particle System")]
	public GameObject m_iceDeathPS;

	[Tooltip("Lightning Death Particle System")]
	public GameObject m_lightningDeathPS;

	private ScoreManager m_ScoreManager;

	protected virtual void Start()
	{
		//Start out inactive but alive
		m_bIsActive = false;
		m_bIsAlive = true;

		//Set current health to maximum health
		m_nCurrentHealth = m_nHealth;


		m_ScoreManager = ScoreManager.Instance;
		//Get player
		m_player = GameObject.FindGameObjectWithTag("Player");

		//Check if waypoints and delays match
		if (m_waypoints.Length != m_delays.Length)
			Debug.LogError(name + " has mismatching delays and waypoints");
	}

	private void Update()
	{
		//If enemy is active
		if(m_bIsActive)
		{
			//If this enemy tracks player, turn towards them
			if (m_bTrackPlayer)
			{
				Vector3 lookPos = m_player.transform.position - transform.position;
				lookPos.y = 0;
				Quaternion targetRotation = Quaternion.LookRotation(lookPos);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_fRotationSpeed * Time.deltaTime);
			}
			//If enemy does not track player, and has rotation, rotate enemy clockwise at constant rate
			else if (m_fRotationSpeed != 0.0f)
			{
				transform.Rotate(Vector3.up, m_fRotationSpeed * Time.deltaTime);
			}

			switch(m_enemyAIType)
			{
				case eEnemyAIType.FOLLOW_PLAYER:
					// desired position
					Vector3 desiredPosition = new Vector3
						(
							m_player.transform.position.x + m_v3Offset.x * transform.localScale.x, 
							m_player.transform.position.y + m_v3Offset.y * transform.localScale.y, 
							m_player.transform.position.z + m_v3Offset.z * transform.localScale.z
						);
					// smoothly move to that position
					transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref m_v3Velocity, 1 / m_fMovementSmoothing, m_fMaxMovementSpeed * Time.deltaTime);
					break;
				case eEnemyAIType.FOLLOW_WAYPOINT:
					//If looping waypoints and enemy has reached the last waypoint, set desired waypoint to 0
					if(m_bLoopWaypoints && m_nDesiredWaypoint >= m_waypoints.Length)
						m_nDesiredWaypoint = 0;
					//If not looping and enemy has reached the last waypoint, deactivate
					else if (!m_bLoopWaypoints && m_nDesiredWaypoint >= m_waypoints.Length)
					{
						Deactivate();
						return;
					}

					//If currently waiting at a waypoint, start incrementing timer
					if(m_bWaitingAtWaypoint)
					{
						m_fTimer += Time.deltaTime;
						//When timer reaches delay, reset timer, stop waiting at waypoint, and increment desired waypoint
						if(m_fTimer >= m_delays[m_nCurrentWaypoint])
						{
							m_fTimer = 0.0f;
							m_bWaitingAtWaypoint = false;
							m_nDesiredWaypoint++;
						}
					}
					//Otherwise, smoothly move to the next waypoint
					else
					{
						transform.position = Vector3.SmoothDamp(transform.position, m_waypoints[m_nDesiredWaypoint].position, ref m_v3Velocity, 1 / m_fMovementSmoothing, m_fMaxMovementSpeed * Time.deltaTime);
					}

					//If not waiting at waypoint and desired waypoint is valid, check if close to waypoint
					if(!m_bWaitingAtWaypoint && m_nDesiredWaypoint < m_waypoints.Length)
					{
						//If close to desired waypoint, start waiting at waypoint
						if (Vector3.Distance(transform.position, m_waypoints[m_nDesiredWaypoint].position) < 0.1f)
						{
							m_nCurrentWaypoint = m_nDesiredWaypoint;
							m_bWaitingAtWaypoint = true;
						}
					}

					break;
				case eEnemyAIType.STATIC:
					break;
				default:
					break;
			}
		}
	}

	public void Activate(GameObject target, GameObject newParent)
	{
		// set isActive to true
		m_bIsActive = true;

		// make enemy position and movement relative to the player's movement area
		transform.parent = newParent.transform;
	}

	public virtual void TakeDamage(int damage)
	{
		//deal the damage
		m_nCurrentHealth -= damage;

		// if enemy runs out of health
		if(m_nCurrentHealth <= 0) {
			Die();
		}

		GetComponent<UIHitEffect>().Show();
	}

	public void Die()
	{
		//Set inactive and dead
		m_bIsActive = false;
		m_bIsAlive = false;

		//Give multiplier and score
		m_ScoreManager.AddMultiplier(m_fMultiplier);
		m_ScoreManager.AddScore(m_lRawScore);
		m_ScoreManager.DropScorePickup(m_lPickupScore, transform);

		// Destroy health bar if present
		if (GetComponent<UIHealthBar>()) {
			GetComponent<UIHealthBar>().DestroyHealthBar();
		}

		StartDeathAnimation();

		//Disable enemy
		gameObject.SetActive(false);
	}

	public void Deactivate()
	{
		//Set inactive and dead
		m_bIsActive = false;
		m_bIsAlive = false;

		if (GetComponent<UIHealthBar>())
		{
			GetComponent<UIHealthBar>().DestroyHealthBar();
		}

		//Disable enemy
		gameObject.SetActive(false);
	}

	#region StartDeathAnimation
	///<summary> Code by Denver Lacey ///</summary>

	void StartDeathAnimation() {

		// get reference to spell manager
		PlayerSpellManager spellManager = FindObjectOfType<PlayerSpellManager>();

		// instantiate death particle system based on current spell type
		try {
			switch (spellManager.m_eSpellType) {
				case eSpellType.FIRE:
					// instantiate new fire death animation
					GameObject fireGO = Instantiate(m_fireDeathPS, transform.position, Quaternion.identity, transform.parent);

					// destroy it once animation complete
					Destroy(fireGO, fireGO.GetComponent<ParticleSystem>().main.duration);
					break;
				
				case eSpellType.ICE:
					// instantiate new ice death animation
					GameObject iceGO = Instantiate(m_iceDeathPS, transform.position, Quaternion.identity, transform.parent);

					// destroy it once animation complete
					Destroy(iceGO, iceGO.GetComponent<ParticleSystem>().main.duration);
					break;
				
				case eSpellType.LIGHTNING:
					// instantiate new lightning death animation
					GameObject lightningGO = Instantiate(m_lightningDeathPS, transform.position, Quaternion.identity, transform.parent);

					// destroy it once animation complete
					Destroy(lightningGO, lightningGO.GetComponent<ParticleSystem>().main.duration);
					break;
				
				default:
					Debug.LogError("Couldn't find spell type.", gameObject);
					break;
			}
		}
		catch (Exception e) {
			Debug.LogError(e.Message);
		}
	}
	#endregion

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "LightningAttack")
		{
			TakeDamage(other.GetComponent<LightningSpellProjectile>().m_nDamage);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		//Check if enemy has collided with player projectile
		if (other.tag == "PlayerProjectile")
		{
			TakeDamage(other.GetComponent<PlayerSpellProjectile>().m_nDamage);
		}

		if (other.tag == "Playfield")
		{
			m_bIsShooting = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Playfield")
		{
			m_bIsShooting = false;
		}
	}
}