///<summary>
///Script Manager: Drake
///Description:
///Handles enemy movement AI, health, and score value. 
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyAIType
{
	STATIC,
	FOLLOW_PLAYER,
	FOLLOW_WAYPOINT
};

public class EnemyActor : MonoBehaviour
{
	[Header("Global Variables")]
	[Tooltip("The maximum health this enemy has")]
	public int m_nHealth;

	[HideInInspector]
	public int m_nCurrentHealth;

	[Tooltip("Whether or not this enemy should rotate to face the player")]
	[SerializeField] private bool m_bTrackPlayer;

	[Tooltip("How fast this enemy should rotate, if 0, will not rotate")]
	[SerializeField] private float m_fRotationSpeed;

	[Tooltip("What type of AI this enemy should have")]
	[SerializeField] private eEnemyAIType m_enemyAIType;

	//Whether or not this enemy is currently active
	[HideInInspector]
	public bool m_bIsActive;

	//Whether or not this enemy is currently alive
	[HideInInspector]
	public bool m_bIsAlive;

	//The current level section of this enemy
	[HideInInspector]
	public LevelSection m_section;

	//Reference to the player
	private GameObject m_player;

	[Header("Follow Player Variables")]
	[Tooltip("How far this enemy should try and stay from the player")]
	[SerializeField] private Vector3 m_v3Offset;

	[Tooltip("Maximum speed the enemy will move at")]
	[SerializeField] private float m_fMaxMovementSpeed;

	[Tooltip("How smoothed the enemy's movement will be, less is more smoothed")]
	[SerializeField] private float m_fMovementSmoothing;

	//The current speed of the enemy
	private Vector3 m_v3Velocity;

	[Header("Follow Waypoint Variables")]
	[Tooltip("The transforms of the waypoints this enemy should go between, should have the same size as Delays")]
	[SerializeField] private Transform[] m_waypoints;

	[Tooltip("How long this enemy should spend at each waypoint, should have the same size as Waypoints")]
	[SerializeField] private float[] m_delays;

	[Tooltip("Whether or not the enemy should loop through the waypoints, or if it should just go through them once")]
	[SerializeField] private bool m_bLoopWaypoints;

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

	private ScoreManager m_ScoreManager;

	private void Start()
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
				Quaternion targetRotation = Quaternion.LookRotation(m_player.transform.position - transform.position);
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

	public void TakeDamage(int damage)
	{
		//deal the damage
		m_nCurrentHealth -= damage;

		GetComponent<UIHitEffect>().Show();

		// if enemy runs out of health
		if(m_nCurrentHealth <= 0) {
			Die();
		}
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

	private void OnTriggerEnter(Collider other)
	{
		//Check if enemy has collided with player projectile
		if (other.tag == "PlayerProjectile")
		{
			TakeDamage(other.GetComponent<PlayerSpellProjectile>().m_nDamage);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		
	}
}