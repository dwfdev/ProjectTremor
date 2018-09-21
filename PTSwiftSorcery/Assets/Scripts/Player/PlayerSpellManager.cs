using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Script Manager: Drake
///Description:
///This script handles attributes of player spells and handles shooting them
///</summary>


///<summary>
///This enum controls what elements a spell can have, and what attributes it can have. 
///</summary>
public enum eSpellType
{
	BASIC_FIRE,
	BASIC_ICE,
	BASIC_LIGHTNING,
	HOMING_FIRE,
	HOMING_ICE,
	HOMING_LIGHTNING,
	SCATTER_FIRE,
	SCATTER_ICE,
	SCATTER_LIGHTNING,
	HOMING_SCATTER_FIRE,
	HOMING_SCATTER_ICE,
	HOMING_SCATTER_LIGHTNING
};

public class PlayerSpellManager : MonoBehaviour
{
	[Tooltip("How long between player shots in seconds")]
	public float m_fFireRate;

	[Tooltip("How fast a shot moves")]
	[SerializeField] private float m_fMoveSpeed;

	[Tooltip("What type of spell is being fired")]
	public eSpellType m_eSpellType;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
