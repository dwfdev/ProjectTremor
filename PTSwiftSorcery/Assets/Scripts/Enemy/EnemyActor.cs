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
	[Tooltip("How much health this enemy has")]
	[SerializeField] private int m_nHealth;

	[Tooltip("Whether or not this enemy should rotate to face the player")]
	[SerializeField] private bool m_bTrackPlayer;

	[Tooltip("How fast this enemy should rotate, if 0, will not rotate")]
	[SerializeField] private float m_fRotationSpeed;

	[Tooltip("What type of AI this enemy should have")]
	[SerializeField] private eEnemyAIType m_enemyAIType;

	[HideInInspector]
	public bool m_bIsActive;

	[HideInInspector]
	public bool m_bIsAlive;

	[HideInInspector]
	public LevelSection m_section;

	private GameObject m_player;

	[Header("Follow Player Variables")]
	[Tooltip("How far this enemy should try and stay from the player")]
	[SerializeField] private Vector3 m_v3Offset;

	[Tooltip("Maximum speed the enemy will move at")]
	[SerializeField] private float m_fMaxMovementSpeed;

	[Tooltip("How smoothed the enemy's movement will be, less is more smoothed")]
	[SerializeField] private float m_fMovementSmoothing;

	private Vector3 m_v3Velocity;

	[Header("Follow Waypoint Variables")]
	[Tooltip("The transforms of the waypoints this enemy should go between")]
	[SerializeField] private Transform[] m_waypoints;

	[Tooltip("How long this enemy should spend at each waypoint")]
	[SerializeField] private float[] m_delays;

	[Tooltip("Whether or not the enemy should loop through the waypoints, or if it should just go through them once")]
	[SerializeField] private bool m_bLoopWaypoints;

	private int m_nCurrentWaypoint;

	private void Start()
	{
		m_bIsActive = true;
		m_bIsAlive = true;
		m_player = GameObject.FindGameObjectWithTag("Player");

		if (m_waypoints.Length != m_delays.Length)
			Debug.LogError(name + " has mismatching delays and waypoints");
	}

	private void Update()
	{
		if(m_bIsActive)
		{
			if (m_bTrackPlayer)
			{
				Quaternion targetRotation = Quaternion.LookRotation(m_player.transform.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_fRotationSpeed * Time.deltaTime);
			}
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
					//follow waypoint code
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
		m_nHealth -= damage;

		// if enemy runs out of health
		if(m_nHealth <= 0) {
			Die();
		}
	}

	public void Die()
	{
		// remove enemy from the section

		m_bIsAlive = false;

		gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PlayerProjectile")
		{
			TakeDamage(other.GetComponent<PlayerSpellProjectile>().m_nDamage);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		
	}
}