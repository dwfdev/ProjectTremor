﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Script Manager: Drake
///Description:
///This script manages each enemy's patterns, and controls when the enemy should shoot. 
///</summary>
public class EnemyBulletPatternManager : MonoBehaviour
{
	[Tooltip("What this pattern fires from")]
	[SerializeField] private GameObject m_parent;

	[Tooltip("Whether or not this pattern moves with the playfield or not")]
	[SerializeField] private bool m_bEnvironment;

	[Tooltip("How long between pattern firing in seconds. \nThis should always be higher or equal to the longest child timer, or else bad stuff can happen")]
	[SerializeField] private float m_fTimer;

	[Tooltip("How long until this pattern activates since the object became active in seconds")]
	[SerializeField] private float m_fDelay;

	[Tooltip("How long since becoming active until this pattern stops firing")]
	[SerializeField] private float m_fDisableDelay;

	[Tooltip("Whether this pattern should start shooting immediately")]
	[SerializeField] private bool m_bStartShooting;

	//Whether or not this pattern is currently active
	private bool m_bIsActive;

	//Whether or not this pattern has been disabled
	private bool m_bIsDisabled;

	//The current time in seconds
	private float m_fCurrentTimer;

	//timer for how long this pattern will stay active(?)
	private float m_fActiveTimer;

	[Tooltip("All spawn locations of bullets in this pattern")]
	[SerializeField] private GameObject[] m_children;

	//Debug code to check if timer is longer than all child timers
	void Start()
	{
		foreach (GameObject child in m_children)
		{
			EnemyBulletPatternChild patternChild = child.GetComponent<EnemyBulletPatternChild>();

			patternChild.m_parent = this;
			patternChild.m_bEnvironment = m_bEnvironment;

			if (m_fTimer < patternChild.GetTimer())
				Debug.LogError("Timer on " + gameObject.name + " is shorter than " + child.name + ", you should fix this immediately!");
		}
		if (m_fDelay == 0.0f)
			m_bIsActive = true;
		else
			m_bIsActive = false;
		m_bIsDisabled = false;
	}

	// Use this for initialization
	void Awake()
	{
		if (m_bStartShooting)
			m_fCurrentTimer = m_fTimer;
		else
			m_fCurrentTimer = 0.0f;
		m_fActiveTimer = 0.0f;
	}
	
	// Update is called once per frame
	//WARNING: Pasta below
	void Update()
	{
		if(m_bIsActive && !m_bIsDisabled)
		{
			if(m_parent != null)
			{
				if(m_parent.GetComponent<EnemyActor>() != null)
				{
					if(m_parent.GetComponent<EnemyActor>().m_bIsShooting)
					{
						if (m_fDisableDelay != 0.0f)
						{
							m_fActiveTimer += Time.deltaTime;
							if (m_fActiveTimer >= m_fDisableDelay)
							{
								m_bIsActive = false;
								m_bIsDisabled = true;
							}
						}

						m_fCurrentTimer += Time.deltaTime;

						if (m_fCurrentTimer >= m_fTimer)
						{
							m_fCurrentTimer = 0.0f;

							foreach(GameObject child in m_children)
							{
								EnemyBulletPatternChild patternChild = child.GetComponent<EnemyBulletPatternChild>();

								patternChild.StartPattern();
							}
						}
					}
				}
			}
			else
			{
				if (m_fDisableDelay != 0.0f)
				{
					m_fActiveTimer += Time.deltaTime;
					if (m_fActiveTimer >= m_fDisableDelay)
					{
						m_bIsActive = false;
						m_bIsDisabled = true;
					}
				}

				m_fCurrentTimer += Time.deltaTime;

				if (m_fCurrentTimer >= m_fTimer)
				{
					m_fCurrentTimer = 0.0f;

					foreach (GameObject child in m_children)
					{
						EnemyBulletPatternChild patternChild = child.GetComponent<EnemyBulletPatternChild>();

						patternChild.StartPattern();
					}
				}
			}
		}
		else
		{
			if(m_parent != null)
			{
				if(m_parent.GetComponent<EnemyActor>() != null)
				{
					if(m_parent.GetComponent<EnemyActor>().m_bIsShooting)
					{
						m_fCurrentTimer += Time.deltaTime;
						if (m_fCurrentTimer >= m_fDelay)
						{
							m_bIsActive = true;
							m_fCurrentTimer = 0.0f;
						}
					}
				}
				else
				{
					m_fCurrentTimer += Time.deltaTime;
					if (m_fCurrentTimer >= m_fDelay)
					{
						m_bIsActive = true;
						m_fCurrentTimer = 0.0f;
					}
				}
			}
			else
			{
				m_fCurrentTimer += Time.deltaTime;
				if (m_fCurrentTimer >= m_fDelay)
				{
					m_bIsActive = true;
					m_fCurrentTimer = 0.0f;
				}
			}
		}
	}
}
