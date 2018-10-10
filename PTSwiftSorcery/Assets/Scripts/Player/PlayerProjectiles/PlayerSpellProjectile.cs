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

	[Tooltip("Whether or not the player is firing scatter shots")]
	public bool m_bIsScatter;

	[Tooltip("How much damage this shot does")]
	public int m_nDamage;

	// Use this for initialization
	void Start ()
	{
		transform.parent = GameObject.FindGameObjectWithTag("Playfield").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_bIsHoming)
		{
			if(m_target == null)
			{
				m_target = GameObject.FindGameObjectWithTag("Enemy");
				if(m_target == null)
				{
					m_bIsHoming = false;
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
