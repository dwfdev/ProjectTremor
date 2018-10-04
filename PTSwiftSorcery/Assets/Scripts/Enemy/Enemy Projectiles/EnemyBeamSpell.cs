using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeamSpell : EnemySpellProjectile
{

	[Tooltip("How long until this beam should become active in seconds")]
	public float m_fBeamActiveTimer;

	[Tooltip("How long this should stay active in seconds, if it is a beam")]
	public float m_fBeamStayTimer;

	//Current time in seconds
	private float m_fTimer;

	// Use this for initialization
	override protected void Awake()
	{
		m_bActive = false;
	}
	
	// Update is called once per frame
	override protected void Update()
	{
		m_fTimer += Time.deltaTime;
		if (m_fTimer >= m_fBeamActiveTimer && !m_bActive)
		{
			m_fTimer = 0.0f;
			m_bActive = true;
			Color color = gameObject.GetComponent<Renderer>().material.color;
			color.a = 1.0f;
			gameObject.GetComponent<Renderer>().material.color = color;
		}
		else if (m_fTimer >= m_fBeamStayTimer && m_bActive)
		{
			Destroy(gameObject);
		}
	}
}
