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

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
