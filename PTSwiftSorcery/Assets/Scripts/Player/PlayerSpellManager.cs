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

	//current timer in seconds
	private float m_fTimer;

	[Header("Movement Speed")]

	[Tooltip("How fast a fire shot moves")]
	public float m_fFireMoveSpeed;

	[Tooltip("How fast an ice shot moves")]
	public float m_fIceMoveSpeed;

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

	[Tooltip("Level 1 lightning radius")]
	[SerializeField] private float m_fTier1LightningRadius;

	[Tooltip("Level 2 lightning radius")]
	[SerializeField] private float m_fTier2LightningRadius;

	[Tooltip("Level 3 level lightning radius")]
	[SerializeField] private float m_fTier3LightningRadius;

	[Tooltip("Level 4 level lightning radius")]
	[SerializeField] private float m_fTier4LightningRadius;

	[Header("Audio")]

	[Tooltip("Fire Spell Audio")]
	[SerializeField] private AudioClip m_fireAudioClip;

	[Tooltip("Fire Spell Audio Volume")]
	[SerializeField] [Range(0f, 1f)] private float m_fFireAudioClipVolume;

	[Tooltip("Ice Spell Audio")]
	[SerializeField] private AudioClip m_iceAudioClip;

	[Tooltip("Ice Spell Audio Volume")]
	[SerializeField] [Range(0f, 1f)] private float m_fIceAudioClipVolume;

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
		StopLightning();
	}

	private void ShootFire()
	{
		if(m_fTimer >= m_fFireRate * m_fFireMultiplier)
		{
			// create new audio source at point
			PlayClipAt(m_fireAudioClip, transform.position, m_fFireAudioClipVolume);

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
			// create audio clip at point
			PlayClipAt(m_iceAudioClip, transform.position, m_fIceAudioClipVolume);

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
					GameObject level4newBullet4 = Instantiate(m_icePrefab, transform.position, transform.rotation);
					GameObject level4newBullet5 = Instantiate(m_icePrefab, transform.position, transform.rotation);

					level4newBullet1.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition + m_nTier3IceDamageAddition + m_nTier4IceDamageAddition;
					level4newBullet2.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition + m_nTier3IceDamageAddition + m_nTier4IceDamageAddition;
					level4newBullet3.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition + m_nTier3IceDamageAddition + m_nTier4IceDamageAddition;
					level4newBullet4.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition + m_nTier3IceDamageAddition + m_nTier4IceDamageAddition;
					level4newBullet5.GetComponent<PlayerSpellProjectile>().m_nDamage = m_nIceDamage + m_nTier2IceDamageAddition + m_nTier3IceDamageAddition + m_nTier4IceDamageAddition;

					Vector3 newPos3 = new Vector3(0.5f, 0.0f, 0.0f);
					Vector3 newPos4 = new Vector3(1.0f, 0.0f, 0.0f);

					level4newBullet2.transform.position += newPos3;
					level4newBullet3.transform.position -= newPos3;
					level4newBullet4.transform.position += newPos4;
					level4newBullet5.transform.position -= newPos4;

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
				m_lightningObject.GetComponent<SphereCollider>().radius = m_fTier1LightningRadius;
				m_lightningObject.GetComponent<LightningSpellProjectile>().m_nDamage = m_nLightningDamage;

				ParticleSystem.MainModule Tier1LightningParticles = m_lightningObject.GetComponent<LightningSpellProjectile>().m_lightningParticle.main;
				Tier1LightningParticles.startSize = m_fTier1LightningRadius * 3.0f;

				ParticleSystem.MainModule Tier1PerimiterParticles = m_lightningObject.GetComponent<LightningSpellProjectile>().m_perimiterParticle.main;
				Tier1PerimiterParticles.startSize = m_fTier1LightningRadius * 4.5f;

				m_lightningObject.SetActive(true);
				break;
			case 1:
				m_lightningObject.GetComponent<SphereCollider>().radius = m_fTier2LightningRadius;
				m_lightningObject.GetComponent<LightningSpellProjectile>().m_nDamage = m_nLightningDamage + m_nTier2LightningDamageAddition;

				ParticleSystem.MainModule Tier2LightningParticles = m_lightningObject.GetComponent<LightningSpellProjectile>().m_lightningParticle.main;
				Tier2LightningParticles.startSize = m_fTier2LightningRadius * 3.0f;

				ParticleSystem.MainModule Tier2PerimiterParticles = m_lightningObject.GetComponent<LightningSpellProjectile>().m_perimiterParticle.main;
				Tier2PerimiterParticles.startSize = m_fTier2LightningRadius * 4.5f;

				m_lightningObject.SetActive(true);
				break;
			case 2:
				m_lightningObject.GetComponent<SphereCollider>().radius = m_fTier3LightningRadius;
				m_lightningObject.GetComponent<LightningSpellProjectile>().m_nDamage = m_nLightningDamage + m_nTier2LightningDamageAddition + m_nTier3LightningDamageAddition;

				ParticleSystem.MainModule Tier3LightningParticles = m_lightningObject.GetComponent<LightningSpellProjectile>().m_lightningParticle.main;
				Tier3LightningParticles.startSize = m_fTier3LightningRadius * 3.0f;

				ParticleSystem.MainModule Tier3PerimiterParticles = m_lightningObject.GetComponent<LightningSpellProjectile>().m_perimiterParticle.main;
				Tier3PerimiterParticles.startSize = m_fTier3LightningRadius * 4.5f;

				m_lightningObject.SetActive(true);
				break;
			case 3:
				m_lightningObject.GetComponent<SphereCollider>().radius = m_fTier4LightningRadius;
				m_lightningObject.GetComponent<LightningSpellProjectile>().m_nDamage = m_nLightningDamage + m_nTier2LightningDamageAddition + m_nTier3LightningDamageAddition + m_nTier4LightningDamageAddition;

				ParticleSystem.MainModule Tier4LightningParticles = m_lightningObject.GetComponent<LightningSpellProjectile>().m_lightningParticle.main;
				Tier4LightningParticles.startSize = m_fTier4LightningRadius * 3.0f;

				ParticleSystem.MainModule Tier4PerimiterParticles = m_lightningObject.GetComponent<LightningSpellProjectile>().m_perimiterParticle.main;
				Tier4PerimiterParticles.startSize = m_fTier4LightningRadius * 4.5f;

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

	AudioSource PlayClipAt(AudioClip clip, Vector3 pos, float vol) {

		// create temp GameObject
		GameObject tempGO = new GameObject("tempAudio");
		
		// set position
		tempGO.transform.position = pos;

		// add audio source component to temp game object
		AudioSource audioSource = tempGO.AddComponent<AudioSource>();

		// define audio clip
		audioSource.clip = clip;

		// volume
		audioSource.volume = vol;

		// Spatial Blen
		audioSource.spatialBlend = 0f;

		// start sound
		audioSource.Play();

		// destroy temp object after clip duration 
		Destroy(tempGO, clip.length);

		// return the AudioSource reference
		return audioSource;
	}
}
