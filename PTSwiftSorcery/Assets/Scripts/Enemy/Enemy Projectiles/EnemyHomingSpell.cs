using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingSpell : EnemySpellProjectile
{

	[Tooltip("What object this projectile seeks towards")]
	public GameObject m_target;

	[Tooltip("How strong the homing is")]
	public float m_fMaxVelocity;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	override protected void Update ()
	{
		Vector3 homingVector = m_target.transform.position - transform.position;
		homingVector.Normalize();
		homingVector *= m_fMaxVelocity;
		transform.position += homingVector * m_fMoveSpeed * Time.deltaTime;
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
