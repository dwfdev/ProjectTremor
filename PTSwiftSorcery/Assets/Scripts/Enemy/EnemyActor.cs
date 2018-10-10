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
	[Tooltip("How much health this enemy has")]
	[SerializeField] private int m_nHealth;

	[HideInInspector]
	public bool m_bIsActive;

	[HideInInspector]
	public bool m_bIsAlive;

	[HideInInspector]
	public LevelSection m_section;

	private void Start()
	{
		m_bIsActive = false;
		m_bIsAlive = true;
	}

	private void Update()
	{
		
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
		m_section.m_enemiesList.Remove(this);

		m_bIsAlive = false;

		Destroy(gameObject);
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