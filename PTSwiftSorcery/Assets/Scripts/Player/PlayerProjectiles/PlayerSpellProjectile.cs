using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Script Manager: Drake
///Description:
///This script handles movement and other effects of player projectiles, such as homing
///</summary>
public class PlayerSpellProjectile : MonoBehaviour
{
	[Tooltip("How fast this shots moves")]
	public float m_fMoveSpeed;

	[Tooltip("What type of spell this shot is")]
	public eSpellType m_spellType;

	[Tooltip("What this shot seeks towards, assuming it's homing")]
	public GameObject m_target;

	[Tooltip("How much damage this shot does")]
	public int m_nDamage;

	// Use this for initialization
	void Start ()
	{
		transform.parent = GameObject.FindGameObjectWithTag("Playfield").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += transform.forward * m_fMoveSpeed * Time.deltaTime;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Playfield")
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy")
		{
			Destroy(gameObject);
		}
	}
}
