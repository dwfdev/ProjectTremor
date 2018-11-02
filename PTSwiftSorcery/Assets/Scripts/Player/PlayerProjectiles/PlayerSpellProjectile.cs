using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Script Manager: Drake
///Description:
///This script handles movement and other effects of player projectiles, such as homing
///</summary>
public class PlayerSpellProjectile : MonoBehaviour
{
	[Tooltip("How fast this shots moves")]
	public float m_fMoveSpeed;

	[Tooltip("What type of spell this shot is")]
	public eSpellType m_spellType;

	[Tooltip("What this shot seeks towards, assuming it's homing")]
	public GameObject m_target;

	[Tooltip("Whether or not the player is firing homing shots")]
	public bool m_bIsHoming;

	[Tooltip("How much damage this shot does")]
	public int m_nDamage;

	private GameObject m_player;

	// Use this for initialization
	void Start ()
	{
		transform.parent = GameObject.FindGameObjectWithTag("Playfield").transform;
		m_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_bIsHoming)
		{
			if(m_target == null || !m_target.activeSelf)
			{
				GameObject target = null;
				//m_target = GameObject.FindGameObjectWithTag("Enemy");
				for(int i = 0; i < m_player.GetComponent<PlayerActor>().m_currentSection.m_enemiesList.Count; i++)
				{
					if (m_player.GetComponent<PlayerActor>().m_currentSection.m_enemiesList[i].gameObject.activeSelf)
					{
						target = m_player.GetComponent<PlayerActor>().m_currentSection.m_enemiesList[i].gameObject;
						break;
					}
				}
				if (target != null)
					m_target = target;
				else
				{
					m_bIsHoming = false;
					return;
				}
			}
			//seek towards m_target
			Vector3 homingVector = m_target.transform.position - transform.position;
			homingVector.Normalize();

			transform.position += homingVector * m_fMoveSpeed * Time.deltaTime;
		}
		else
		{
			transform.position += transform.forward * m_fMoveSpeed * Time.deltaTime;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Playfield")
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy")
		{
			Destroy(gameObject);
		}
	}
}
