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
		//start out inactive
		m_bActive = false;
	}
	
	// Update is called once per frame
	override protected void Update()
	{
		//increment timer
		m_fTimer += Time.deltaTime;

		//if timer reaches active timer and isn't already active, start activating
		if (m_fTimer >= m_fBeamActiveTimer && !m_bActive)
		{
			//reset timer
			m_fTimer = 0.0f;

			//set active to true
			m_bActive = true;
			
			//get colour
			Color color = gameObject.GetComponent<Renderer>().material.color;
			//set colour alpha to opaque
			color.a = 1.0f;
			//set new colour
			gameObject.GetComponent<Renderer>().material.color = color;
		}

		//if timer reaches end of stay timer and is already active, destroy self
		else if (m_fTimer >= m_fBeamStayTimer && m_bActive)
		{
			Destroy(gameObject);
		}
	}
}
