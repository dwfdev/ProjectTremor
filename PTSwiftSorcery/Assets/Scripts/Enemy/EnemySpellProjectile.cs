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

	[Tooltip("What object this projectile seeks towards, assuming it's a homing projectile")]
	public GameObject m_target;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
