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
	FIRE,
	ICE,
	LIGHTNING,
	ARCANE
};

public class PlayerSpellManager : MonoBehaviour
{
	[Tooltip("How long between player shots in seconds")]
	public float m_fFireRate;

	//current timer in seconds
	private float m_fTimer;

	[Tooltip("How fast a shot moves")]
	public float m_fMoveSpeed;

	[Tooltip("What type of spell is being fired")]
	public eSpellType m_eSpellType;

	[Tooltip("Whether or not the player is firing homing shots")]
	public bool m_isHoming;

	[Tooltip("Whether or not the player is firing scatter shots")]
	public bool m_isScatter;

	#region Spell prefabs
	[Tooltip("The prefab for fire shots")]
	[SerializeField] private GameObject m_firePrefab;

	[Tooltip("The prefab for ice shots")]
	[SerializeField] private GameObject m_icePrefab;

	[Tooltip("The prefab for lightning shots")]
	[SerializeField] private GameObject m_lightningPrefab;
	#endregion

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//update the timer
		m_fTimer += Time.deltaTime;
	}

	public void Fire()
	{
		if (m_fTimer >= m_fFireRate)
		{
			switch(m_eSpellType)
			{
				case eSpellType.FIRE:
					//instantiate fire shot prefab
					m_fTimer = 0.0f;
					break;
				case eSpellType.ICE:
					//instantiate ice shot prefab
					m_fTimer = 0.0f;
					break;
				case eSpellType.LIGHTNING:
					//instantiate lightning shot prefab
					m_fTimer = 0.0f;
					break;
				default:
					Debug.LogWarning(gameObject.name + " has no spell type, this should be fixed");
					m_fTimer = 0.0f;
					break;
			}
		}
	}
}
