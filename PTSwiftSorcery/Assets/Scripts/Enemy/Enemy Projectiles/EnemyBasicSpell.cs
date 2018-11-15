using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicSpell : EnemySpellProjectile
{
	//lifespan of this projectile
	[HideInInspector]
	public float m_fLifespan;

	//internal timer
	private float m_fTimer;
	
	// Update is called once per frame
	override protected void Update ()
	{
		//increment timer
		m_fTimer += Time.deltaTime;

		//if timer exceeds lifespan, destroy object
		if(m_fLifespan != 0.0f && m_fTimer >= m_fLifespan)
			Destroy(gameObject);

		//move projectile forward
		transform.position += transform.forward * m_fMoveSpeed * Time.deltaTime;
	}

	protected override void OnTriggerEnter(Collider other)
	{
		//call base collision code
		base.OnTriggerEnter(other);
	}

	protected override void OnTriggerExit(Collider other)
	{
		//call base exit code
		base.OnTriggerExit(other);
	}
}
