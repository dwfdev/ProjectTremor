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
	MELEE_SWING,
	BEAM,
	HOMING_PROJECTILE
};


public class EnemyBulletPatternChild : MonoBehaviour
{
	[Tooltip("How long since the pattern fires until this object should fire it's projectile in seconds")]
	[SerializeField] private float m_fTimer;

	//The current time in seconds
	private float m_fCurrentTimer;

	[Tooltip("How fast the projectile this fires moves")]
	[SerializeField] private float m_fMoveSpeed;

	[Tooltip("What type of shot this pattern child shoots")]
	[SerializeField] private eBulletType m_eBulletType;

	//Whether or not this pattern child is currently active
	private bool m_bActive;

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
		//code for firing shot
	}
}
