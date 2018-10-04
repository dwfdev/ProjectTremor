using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicSpell : EnemySpellProjectile
{
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	override protected void Update ()
	{
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
