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
		switch(m_eBulletType)
		{
			case eBulletType.BASIC_PROJECTILE:
				transform.position += transform.forward * m_fMoveSpeed * Time.deltaTime;
				break;
			case eBulletType.BEAM:
				//beam code
				break;
			case eBulletType.HOMING_PROJECTILE:
				//home towards player
				break;
			case eBulletType.MELEE_SWING:
				//melee code
				break;
			default:
				Debug.LogWarning(gameObject.name + " has no bullet type, this should be fixed");
				break;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Playfield")
		{
			Destroy(gameObject);
		}
	}
}
