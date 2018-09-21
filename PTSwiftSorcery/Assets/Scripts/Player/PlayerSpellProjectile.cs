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
	public bool m_isHoming;

	[Tooltip("Whether or not the player is firing scatter shots")]
	public bool m_isScatter;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_isHoming)
		{
			//seek towards m_target
			Vector3 homingVector = m_target.transform.position - transform.position;
			homingVector.Normalize();

			transform.position += homingVector * m_fMoveSpeed * Time.deltaTime;
		}
		else
		{
			transform.position += Vector3.forward * m_fMoveSpeed * Time.deltaTime;
		}
	}
}
