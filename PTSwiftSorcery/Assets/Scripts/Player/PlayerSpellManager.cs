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
	[Header("Fire Rate")]

	[Tooltip("How long between player shots in seconds")]
	public float m_fFireRate;

	[Tooltip("Multiplier for fire shots fire rate, the lower the multiplier, the faster this type of shot fires")]
	public float m_fFireMultiplier;

	[Tooltip("Multiplier for ice shots fire rate, the lower the multiplier, the faster this type of shot fires")]
	public float m_fIceMultiplier;

	[Tooltip("Multiplier for lightning shots fire rate, the lower the multiplier, the faster this type of shot fires")]
	public float m_fLightningMultiplier;

	//current timer in seconds
	private float m_fTimer;

	[Header("Movement Speed")]

	[Tooltip("How fast a fire shot moves")]
	public float m_fFireMoveSpeed;

	[Tooltip("How fast an ice shot moves")]
	public float m_fIceMoveSpeed;

	[Tooltip("How fast a lightning shot moves")]
	public float m_fLightningMoveSpeed;

	[Header("Misc Config")]

	[Tooltip("What type of spell is being fired")]
	public eSpellType m_eSpellType;

	[Tooltip("Whether or not the player is firing homing shots")]
	public bool m_bIsHoming;

	[Tooltip("Whether or not the player is firing scatter shots")]
	public bool m_bIsScatter;

	[Tooltip("How many degrees out scatter shots should spread")]
	public float m_fScatterSpread;

	[Header("Prefabs")]

	[Tooltip("The prefab for fire shots")]
	[SerializeField] private GameObject m_firePrefab;

	[Tooltip("The prefab for ice shots")]
	[SerializeField] private GameObject m_icePrefab;

	[Tooltip("The prefab for lightning shots")]
	[SerializeField] private GameObject m_lightningPrefab;

	[Tooltip("How much damage fire shots do")]
	[SerializeField] private int m_nFireDamage;

	[Tooltip("How much damage ice shots do")]
	[SerializeField] private int m_nIceDamage;

	[Tooltip("How much damage lightning shots do")]
	[SerializeField] private int m_nLightningDamage;
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
		switch(m_eSpellType)
		{
			case eSpellType.FIRE:
				if (m_fTimer >= m_fFireRate * m_fFireMultiplier)
				{
					//instantiate fire shot prefab
					GameObject newBullet = Instantiate(m_firePrefab, transform.position, transform.rotation);
					if(m_bIsScatter)
					{
						GameObject leftBullet = Instantiate(m_firePrefab, transform.position, transform.rotation);
						leftBullet.transform.Rotate(Vector3.up, -m_fScatterSpread);
						leftBullet.GetComponent<PlayerSpellProjectile>().m_bIsHoming = m_bIsHoming;
						leftBullet.GetComponent<PlayerSpellProjectile>().m_bIsScatter = true;
						leftBullet.GetComponent<PlayerSpellProjectile>().m_fMoveSpeed = m_fFireMoveSpeed;
						leftBullet.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage;

						GameObject rightBullet = Instantiate(m_firePrefab, transform.position, transform.rotation);
						rightBullet.transform.Rotate(Vector3.up, m_fScatterSpread);
						rightBullet.GetComponent<PlayerSpellProjectile>().m_bIsHoming = m_bIsHoming;
						rightBullet.GetComponent<PlayerSpellProjectile>().m_bIsScatter = true;
						rightBullet.GetComponent<PlayerSpellProjectile>().m_fMoveSpeed = m_fFireMoveSpeed;
						rightBullet.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage;

						newBullet.GetComponent<PlayerSpellProjectile>().m_bIsScatter = true;
					}
					newBullet.GetComponent<PlayerSpellProjectile>().m_bIsHoming = m_bIsHoming;
					newBullet.GetComponent<PlayerSpellProjectile>().m_fMoveSpeed = m_fFireMoveSpeed;
					newBullet.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage;

					m_fTimer = 0.0f;
				}
				break;
			case eSpellType.ICE:
				//instantiate ice shot prefab
				if (m_fTimer >= m_fFireRate * m_fIceMultiplier)
				{
					//instantiate fire shot prefab
					GameObject newBullet = Instantiate(m_icePrefab, transform.position, transform.rotation);
					if (m_bIsScatter)
					{
						GameObject leftBullet = Instantiate(m_icePrefab, transform.position, transform.rotation);
						leftBullet.transform.Rotate(Vector3.up, -m_fScatterSpread);
						leftBullet.GetComponent<PlayerSpellProjectile>().m_bIsHoming = m_bIsHoming;
						leftBullet.GetComponent<PlayerSpellProjectile>().m_bIsScatter = true;
						leftBullet.GetComponent<PlayerSpellProjectile>().m_fMoveSpeed = m_fIceMoveSpeed;
						leftBullet.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage;

						GameObject rightBullet = Instantiate(m_icePrefab, transform.position, transform.rotation);
						rightBullet.transform.Rotate(Vector3.up, m_fScatterSpread);
						rightBullet.GetComponent<PlayerSpellProjectile>().m_bIsHoming = m_bIsHoming;
						rightBullet.GetComponent<PlayerSpellProjectile>().m_bIsScatter = true;
						rightBullet.GetComponent<PlayerSpellProjectile>().m_fMoveSpeed = m_fIceMoveSpeed;
						rightBullet.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage;

						newBullet.GetComponent<PlayerSpellProjectile>().m_bIsScatter = true;
					}
					newBullet.GetComponent<PlayerSpellProjectile>().m_bIsHoming = m_bIsHoming;
					newBullet.GetComponent<PlayerSpellProjectile>().m_fMoveSpeed = m_fIceMoveSpeed;
					newBullet.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage;

					m_fTimer = 0.0f;
				}
				break;
			case eSpellType.LIGHTNING:
				//instantiate lightning shot prefab
				if (m_fTimer >= m_fFireRate * m_fLightningMultiplier)
				{
					//instantiate fire shot prefab
					GameObject newBullet = Instantiate(m_lightningPrefab, transform.position, transform.rotation);
					if (m_bIsScatter)
					{
						GameObject leftBullet = Instantiate(m_lightningPrefab, transform.position, transform.rotation);
						leftBullet.transform.Rotate(Vector3.up, -m_fScatterSpread);
						leftBullet.GetComponent<PlayerSpellProjectile>().m_bIsHoming = m_bIsHoming;
						leftBullet.GetComponent<PlayerSpellProjectile>().m_bIsScatter = true;
						leftBullet.GetComponent<PlayerSpellProjectile>().m_fMoveSpeed = m_fLightningMoveSpeed;
						leftBullet.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nLightningDamage;

						GameObject rightBullet = Instantiate(m_lightningPrefab, transform.position, transform.rotation);
						rightBullet.transform.Rotate(Vector3.up, m_fScatterSpread);
						rightBullet.GetComponent<PlayerSpellProjectile>().m_bIsHoming = m_bIsHoming;
						rightBullet.GetComponent<PlayerSpellProjectile>().m_bIsScatter = true;
						rightBullet.GetComponent<PlayerSpellProjectile>().m_fMoveSpeed = m_fLightningMoveSpeed;
						rightBullet.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nLightningDamage;

						newBullet.GetComponent<PlayerSpellProjectile>().m_bIsScatter = true;
					}
					newBullet.GetComponent<PlayerSpellProjectile>().m_bIsHoming = m_bIsHoming;
					newBullet.GetComponent<PlayerSpellProjectile>().m_fMoveSpeed = m_fLightningMoveSpeed;
					newBullet.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nLightningDamage;

					m_fTimer = 0.0f;
				}
				break;
			default:
				Debug.LogWarning(gameObject.name + " has an invalid spell type, this should be fixed");
				m_fTimer = 0.0f;
				break;
		}
	}
}
