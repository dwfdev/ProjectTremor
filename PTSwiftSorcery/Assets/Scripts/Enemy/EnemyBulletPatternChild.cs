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

	[Tooltip("How fast the projectile this fires moves")]
	[SerializeField] private float m_fMoveSpeed;

	[Tooltip("What type of shot this pattern child shoots")]
	[SerializeField] private eBulletType m_eBulletType;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
