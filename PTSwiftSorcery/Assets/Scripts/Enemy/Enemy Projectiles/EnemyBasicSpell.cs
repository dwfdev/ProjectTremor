using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicSpell : EnemySpellProjectile
{
	[HideInInspector]
	public float m_fLifespan;

	private float m_fTimer;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	override protected void Update ()
	{
		m_fTimer += Time.deltaTime;

		if(m_fLifespan != 0 && m_fTimer >= m_fLifespan)
			Destroy(gameObject);

		transform.position += transform.forward * m_fMoveSpeed * Time.deltaTime;
	}

	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
	}

	protected override void OnTriggerExit(Collider other)
	{
		base.OnTriggerExit(other);
	}
}
