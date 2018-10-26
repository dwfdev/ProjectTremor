using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Script Manager: Drake
///Description:
///This script manages a point on an enemy where bullets can be spawned. 
///</summary>


///<summary>
///This enum controls what types of enemy projectiles exist
///</summary>
public enum eBulletType
{
	BASIC_PROJECTILE,
	BEAM,
	HOMING_PROJECTILE
};


public class EnemyBulletPatternChild : MonoBehaviour
{
	[Tooltip("How long since the pattern fires until this object should fire it's projectile in seconds")]
	[SerializeField] private float m_fTimer;

	//The current time in seconds
	private float m_fCurrentTimer;

	[HideInInspector]
	public bool m_bEnvironment;

	[Tooltip("How fast the projectile this fires moves")]
	[SerializeField] private float m_fMoveSpeed;

	[Tooltip("What type of shot this pattern child shoots")]
	[SerializeField] private eBulletType m_eBulletType;

	[Tooltip("Prefab for the bullet this fires")]
	[SerializeField] private GameObject m_bulletPrefab;

	[Tooltip("How long until a beam becomes active, only used if this child fires a beam")]
	[SerializeField] private float m_fBeamActiveTimer;

	[Tooltip("How long until a beam disappears, only used if this child fires a beam")]
	[SerializeField] private float m_fBeamStayTimer;

	[Tooltip("Whether or not the projectile this child shoots should follow the child")]
	[SerializeField] private bool m_bSpawnChild;

	[Tooltip("The lifespan of this projectile, if set to zero, projectile lasts forever")]
	[SerializeField] private float m_fLifespan;

	//Whether or not this pattern child is currently active
	private bool m_bActive;

	[HideInInspector]
	public EnemyBulletPatternManager m_parent;

	private void Start()
	{
		if(m_fLifespan == 0.0f && m_fMoveSpeed == 0.0f && m_eBulletType != eBulletType.BEAM)
		{
			Debug.LogError("A child of " + m_parent.gameObject.name + " has projectiles that will not delete themselves");
		}
	}

	// Use this for initialization
	void Awake()
	{
		m_fCurrentTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(m_bActive)
		{
			m_fCurrentTimer += Time.deltaTime;

			if(m_fCurrentTimer >= m_fTimer)
			{
				Fire();
				m_fCurrentTimer = 0.0f;
				m_bActive = false;
			}
		}
	}

	//Called at the start of the pattern
	public void StartPattern()
	{
		m_bActive = true;
	}


	public float GetTimer()
	{
		return m_fTimer;
	}

	private void Fire()
	{
		GameObject newBullet = Instantiate(m_bulletPrefab, transform.position, transform.rotation);
		newBullet.GetComponent<EnemySpellProjectile>().m_eBulletType = m_eBulletType;

		switch(m_eBulletType)
		{
			case eBulletType.BEAM:
				newBullet.GetComponent<EnemyBeamSpell>().m_fBeamActiveTimer = m_fBeamActiveTimer;
				newBullet.GetComponent<EnemyBeamSpell>().m_fBeamStayTimer = m_fBeamStayTimer;
				break;
			case eBulletType.BASIC_PROJECTILE:
				newBullet.GetComponent<EnemyBasicSpell>().m_fLifespan = m_fLifespan;
				break;
			case eBulletType.HOMING_PROJECTILE:
				newBullet.GetComponent<EnemyHomingSpell>().m_fLifespan = m_fLifespan;
				break;
		}

		if(m_bSpawnChild)
			newBullet.transform.parent = transform;
		else if(!m_bEnvironment)
			newBullet.transform.parent = GameObject.FindGameObjectWithTag("Playfield").transform;

		newBullet.GetComponent<EnemySpellProjectile>().m_fMoveSpeed = m_fMoveSpeed;
	}
}
