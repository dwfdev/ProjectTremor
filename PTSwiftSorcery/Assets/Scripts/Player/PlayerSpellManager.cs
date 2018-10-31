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

	[Tooltip("The maxmimum amount of degrees a fire shot can spread")]
	public float m_fFireSpread;

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

	[HideInInspector] public int m_nSpellLevel;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//update the timer
		m_fTimer += Time.deltaTime;

		if(Input.GetKeyDown(KeyCode.B))
		{
			m_nSpellLevel++;
		}
	}

	/*public void Fire()
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
	}*/

	public void Fire()
	{
		switch(m_eSpellType)
		{
			case eSpellType.FIRE:
				ShootFire();
				break;
			case eSpellType.ICE:
				ShootIce();
				break;
			case eSpellType.LIGHTNING:
				ShootLightning();
				break;
			default:
				break;
		}
	}

	private void ShootFire()
	{
		if(m_fTimer >= m_fFireRate * m_fFireMultiplier)
		{
			switch(m_nSpellLevel)
			{
				case 0:
					GameObject level1newBullet1 = Instantiate(m_firePrefab, transform.position, transform.rotation);

					level1newBullet1.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread));
					break;
				case 1:
					GameObject level2newBullet1 = Instantiate(m_firePrefab, transform.position, transform.rotation);
					GameObject level2newBullet2 = Instantiate(m_firePrefab, transform.position, transform.rotation);

					level2newBullet1.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread) - 5.0f);
					level2newBullet2.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread) + 5.0f);
					break;
				case 2:
					GameObject level3newBullet1 = Instantiate(m_firePrefab, transform.position, transform.rotation);
					GameObject level3newBullet2 = Instantiate(m_firePrefab, transform.position, transform.rotation);
					GameObject level3newBullet3 = Instantiate(m_firePrefab, transform.position, transform.rotation);

					level3newBullet1.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread) - 5.0f);
					level3newBullet2.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread) + 5.0f);
					level3newBullet3.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread));
					break;
				case 3:
					GameObject level4newBullet1 = Instantiate(m_firePrefab, transform.position, transform.rotation);
					GameObject level4newBullet2 = Instantiate(m_firePrefab, transform.position, transform.rotation);
					GameObject level4newBullet3 = Instantiate(m_firePrefab, transform.position, transform.rotation);
					GameObject level4newBullet4 = Instantiate(m_firePrefab, transform.position, transform.rotation);
					GameObject level4newBullet5 = Instantiate(m_firePrefab, transform.position, transform.rotation);

					level4newBullet1.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread));
					level4newBullet2.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread) - 5.0f);
					level4newBullet3.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread) - 10.0f);
					level4newBullet4.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread) + 5.0f);
					level4newBullet5.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread) + 10.0f);
					break;
				default:
					if (m_nSpellLevel < 0)
						m_nSpellLevel = 0;
					else if (m_nSpellLevel > 3)
						m_nSpellLevel = 3;
					break;
			}
			m_fTimer = 0.0f;
		}
	}

	private void ShootIce()
	{
		if(m_fTimer >= m_fFireRate * m_fIceMultiplier)
		{
			switch(m_nSpellLevel)
			{
				case 0:
					GameObject level1newBullet1 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					break;
				case 1:
					GameObject level2newBullet1 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					GameObject level2newBullet2 = Instantiate(m_icePrefab, transform.position, transform.rotation);

					Vector3 newPos = new Vector3(0.5f, 0.0f, 0.0f);

					level2newBullet1.transform.position += newPos;
					level2newBullet2.transform.position -= newPos;

					break;
				case 2:
					GameObject level3newBullet1 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					GameObject level3newBullet2 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					GameObject level3newBullet3 = Instantiate(m_icePrefab, transform.position, transform.rotation);

					Vector3 newPos2 = new Vector3(0.5f, 0.0f, 0.0f);

					level3newBullet2.transform.position += newPos2;
					level3newBullet3.transform.position -= newPos2;
					break;
				case 3:
					GameObject level4newBullet1 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					GameObject level4newBullet2 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					GameObject level4newBullet3 = Instantiate(m_icePrefab, transform.position, transform.rotation);

					Vector3 newPos3 = new Vector3(0.5f, 0.0f, 0.0f);

					level4newBullet2.transform.position += newPos3;
					level4newBullet3.transform.position -= newPos3;

					level4newBullet1.GetComponent<PlayerSpellProjectile>().m_bIsHoming = true;
					level4newBullet2.GetComponent<PlayerSpellProjectile>().m_bIsHoming = true;
					level4newBullet3.GetComponent<PlayerSpellProjectile>().m_bIsHoming = true;
					break;
				default:
					if (m_nSpellLevel < 0)
						m_nSpellLevel = 0;
					else if (m_nSpellLevel > 3)
						m_nSpellLevel = 3;
					break;
			}
			m_fTimer = 0.0f;
		}
	}

	private void ShootLightning()
	{
		Debug.LogWarning("Lightning is not implemented yet");
	}
}
