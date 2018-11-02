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
	LIGHTNING
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

	[Tooltip("The maxmimum amount of degrees a fire shot can spread")]
	public float m_fFireSpread;

	[Header("Prefabs")]

	[Tooltip("The prefab for fire shots")]
	[SerializeField] private GameObject m_firePrefab;

	[Tooltip("The prefab for ice shots")]
	[SerializeField] private GameObject m_icePrefab;

	[Tooltip("The prefab for lightning shots")]
	[SerializeField] private GameObject m_lightningPrefab;

	[Header("Fire Damage")]

	[Tooltip("How much base damage fire shots do")]
	[SerializeField] private int m_nFireDamage;

	[Tooltip("How much additional damage tier 2 fire shots do over base")]
	[SerializeField] private int m_nTier2FireDamageAddition;

	[Tooltip("How much additional damage tier 3 fire shots do over tier 2")]
	[SerializeField] private int m_nTier3FireDamageAddition;

	[Tooltip("How much additional damage tier 4 fire shots do over tier 3")]
	[SerializeField] private int m_nTier4FireDamageAddition;

	[Header("Ice Damage")]

	[Tooltip("How much base damage ice shots do")]
	[SerializeField] private int m_nIceDamage;

	[Tooltip("How much additional damage tier 2 ice shots do over base")]
	[SerializeField] private int m_nTier2IceDamageAddition;

	[Tooltip("How much additional damage tier 3 ice shots do over tier 2")]
	[SerializeField] private int m_nTier3IceDamageAddition;

	[Tooltip("How much additional damage tier 4 ice shots do over tier 3")]
	[SerializeField] private int m_nTier4IceDamageAddition;

	[Header("Lightning Damage")]

	[Tooltip("How much base damage lightning attacks do each frame")]
	[SerializeField] private int m_nLightningDamage;

	[Tooltip("How much additional damage tier 2 lightning attacks do per frame over base")]
	[SerializeField] private int m_nTier2LightningDamageAddition;

	[Tooltip("How much additional damage tier 3 lightning attacks do per frame over tier 2")]
	[SerializeField] private int m_nTier3LightningDamageAddition;

	[Tooltip("How much additional damage tier 4 lightning attacks do per frame over tier 3")]
	[SerializeField] private int m_nTier4LightningDamageAddition;

	[Header("Lightning")]

	[Tooltip("Lightning object")]
	[SerializeField] private GameObject m_lightningObject;

	[Tooltip("Base level lightning radius")]
	[SerializeField] private float m_fTier1LightningRadius;

	[Tooltip("Base level lightning radius")]
	[SerializeField] private float m_fTier2LightningRadius;

	[Tooltip("Base level lightning radius")]
	[SerializeField] private float m_fTier3LightningRadius;

	[Tooltip("Base level lightning radius")]
	[SerializeField] private float m_fTier4LightningRadius;

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

		if(Application.isEditor)
		{
			if(Input.GetKeyDown(KeyCode.B))
			{
				m_nSpellLevel++;
			}
		}
	}

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

	public void StopFiring()
	{
		if(m_eSpellType == eSpellType.LIGHTNING)
		{
			StopLightning();
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

					level1newBullet1.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage;

					level1newBullet1.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread));
					break;
				case 1:
					GameObject level2newBullet1 = Instantiate(m_firePrefab, transform.position, transform.rotation);
					GameObject level2newBullet2 = Instantiate(m_firePrefab, transform.position, transform.rotation);

					level2newBullet1.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage + m_nTier2FireDamageAddition;
					level2newBullet2.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage + m_nTier2FireDamageAddition;

					level2newBullet1.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread) - 5.0f);
					level2newBullet2.transform.Rotate(Vector3.up, Random.Range(-m_fFireSpread, m_fFireSpread) + 5.0f);
					break;
				case 2:
					GameObject level3newBullet1 = Instantiate(m_firePrefab, transform.position, transform.rotation);
					GameObject level3newBullet2 = Instantiate(m_firePrefab, transform.position, transform.rotation);
					GameObject level3newBullet3 = Instantiate(m_firePrefab, transform.position, transform.rotation);

					level3newBullet1.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage + m_nTier2FireDamageAddition + m_nTier3FireDamageAddition;
					level3newBullet2.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage + m_nTier2FireDamageAddition + m_nTier3FireDamageAddition;
					level3newBullet3.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage + m_nTier2FireDamageAddition + m_nTier3FireDamageAddition;

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

					level4newBullet1.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage + m_nTier2FireDamageAddition + m_nTier3FireDamageAddition + m_nTier4FireDamageAddition;
					level4newBullet2.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage + m_nTier2FireDamageAddition + m_nTier3FireDamageAddition + m_nTier4FireDamageAddition;
					level4newBullet3.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage + m_nTier2FireDamageAddition + m_nTier3FireDamageAddition + m_nTier4FireDamageAddition;
					level4newBullet4.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage + m_nTier2FireDamageAddition + m_nTier3FireDamageAddition + m_nTier4FireDamageAddition;
					level4newBullet5.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nFireDamage + m_nTier2FireDamageAddition + m_nTier3FireDamageAddition + m_nTier4FireDamageAddition;

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

					level1newBullet1.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage;
					break;
				case 1:
					GameObject level2newBullet1 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					GameObject level2newBullet2 = Instantiate(m_icePrefab, transform.position, transform.rotation);

					level2newBullet1.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition;
					level2newBullet2.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition;

					Vector3 newPos = new Vector3(0.5f, 0.0f, 0.0f);

					level2newBullet1.transform.position += newPos;
					level2newBullet2.transform.position -= newPos;

					break;
				case 2:
					GameObject level3newBullet1 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					GameObject level3newBullet2 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					GameObject level3newBullet3 = Instantiate(m_icePrefab, transform.position, transform.rotation);

					level3newBullet1.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition + m_nTier3IceDamageAddition;
					level3newBullet2.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition + m_nTier3IceDamageAddition;
					level3newBullet3.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition + m_nTier3IceDamageAddition;

					Vector3 newPos2 = new Vector3(0.5f, 0.0f, 0.0f);

					level3newBullet2.transform.position += newPos2;
					level3newBullet3.transform.position -= newPos2;
					break;
				case 3:
					GameObject level4newBullet1 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					GameObject level4newBullet2 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					GameObject level4newBullet3 = Instantiate(m_icePrefab, transform.position, transform.rotation);

					level4newBullet1.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition + m_nTier3IceDamageAddition + m_nTier4IceDamageAddition;
					level4newBullet2.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition + m_nTier3IceDamageAddition + m_nTier4IceDamageAddition;
					level4newBullet3.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition + m_nTier3IceDamageAddition + m_nTier4IceDamageAddition;

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
		switch(m_nSpellLevel)
		{
			case 0:
				Vector3 level1NewScale = new Vector3(m_fTier1LightningRadius * 2, 0.1f, m_fTier1LightningRadius * 2);

				m_lightningObject.transform.localScale = level1NewScale;
				m_lightningObject.GetComponent<LightningSpellProjectile>().m_nDamage = m_nLightningDamage;

				m_lightningObject.SetActive(true);
				break;
			case 1:
				Vector3 level2NewScale = new Vector3(m_fTier2LightningRadius * 2, 0.1f, m_fTier2LightningRadius * 2);

				m_lightningObject.transform.localScale = level2NewScale;
				m_lightningObject.GetComponent<LightningSpellProjectile>().m_nDamage = m_nLightningDamage + m_nTier2LightningDamageAddition;

				m_lightningObject.SetActive(true);
				break;
			case 2:
				Vector3 level3NewScale = new Vector3(m_fTier3LightningRadius * 2, 0.1f, m_fTier3LightningRadius * 2);

				m_lightningObject.transform.localScale = level3NewScale;
				m_lightningObject.GetComponent<LightningSpellProjectile>().m_nDamage = m_nLightningDamage + m_nTier2LightningDamageAddition + m_nTier3LightningDamageAddition;

				m_lightningObject.SetActive(true);
				break;
			case 3:
				Vector3 level4NewScale = new Vector3(m_fTier4LightningRadius * 2, 0.1f, m_fTier4LightningRadius * 2);

				m_lightningObject.transform.localScale = level4NewScale;
				m_lightningObject.GetComponent<LightningSpellProjectile>().m_nDamage = m_nLightningDamage + m_nTier2LightningDamageAddition + m_nTier3LightningDamageAddition + m_nTier4LightningDamageAddition;

				m_lightningObject.SetActive(true);
				break;
			default:
				if (m_nSpellLevel < 0)
					m_nSpellLevel = 0;
				else if (m_nSpellLevel > 3)
					m_nSpellLevel = 3;
				break;
		}
	}
	private void StopLightning()
	{
		m_lightningObject.SetActive(false);
	}
}
