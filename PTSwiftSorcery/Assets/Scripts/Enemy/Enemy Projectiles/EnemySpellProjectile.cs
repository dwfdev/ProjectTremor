using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Script Manager: Drake
///Description:
///
///</summary>
public class EnemySpellProjectile : MonoBehaviour
{
	[Tooltip("What type of projectile this is")]
	public eBulletType m_eBulletType;

	[Tooltip("How fast this projectile moves")]
	public float m_fMoveSpeed;

	//whether or not this projectile is active
	protected bool m_bActive;

	// Use this for initialization
	virtual protected void Awake ()
	{
		m_bActive = true;
	}
	
	// Update is called once per frame
	virtual protected void Update ()
	{

	}

	virtual protected void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && m_eBulletType != eBulletType.BEAM)
		{
			Destroy(gameObject);
		}
	}

	virtual protected void OnTriggerExit(Collider other)
	{
		if(other.tag == "Playfield")
		{
			Destroy(gameObject);
		}
	}

	public bool GetActive()
	{
		return m_bActive;
	}
}
