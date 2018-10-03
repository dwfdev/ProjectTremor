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

	[Tooltip("How long until this beam should become active in seconds")]
	public float m_fBeamActiveTimer;

	//whether or not a beam is active
	private bool m_bBeamActive;

	[Tooltip("How long this should stay active in seconds, if it is a beam")]
	public float m_fBeamTimer;

	//Current time in seconds
	private float m_fTimer;


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_fTimer += Time.deltaTime;
		switch(m_eBulletType)
		{
			case eBulletType.BASIC_PROJECTILE:
				transform.position += transform.forward * m_fMoveSpeed * Time.deltaTime;
				break;
			case eBulletType.BEAM:
				//beam code
				if(m_fTimer >= m_fBeamActiveTimer && !m_bBeamActive)
				{
					m_fTimer = 0.0f;
					m_bBeamActive = true;
					Color color = gameObject.GetComponent<Renderer>().material.color;
					color.a = 1.0f;
					gameObject.GetComponent<Renderer>().material.color = color;
				}
				else if (m_fTimer >= m_fBeamTimer && m_bBeamActive)
				{
					Destroy(gameObject);
				}
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
		if(other.tag == "Player" && m_eBulletType != eBulletType.BEAM && m_eBulletType != eBulletType.MELEE_SWING)
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
