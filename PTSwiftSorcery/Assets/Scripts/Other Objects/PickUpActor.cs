using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager: Denver
///		Description:	Handles the behaviour of the pick-up
///		Date Modified:
///</summary>

public enum ePickUpType
{
	INCREASE_FIRE_RATE,
	IMMUNITY,
	SHIELD,
	SLOW_DOWN_TIME,
	HOMING_SPELLS,
	SCATTER_SPELLS,
	BOMB,
	ERROR
}

public struct sPickUp
{
	// how long the effect lasts
	public float duration;

	// how much the effect affects the player
	public float magnitude;

	// what type of pickup it is
	public ePickUpType type;
}

public class PickUpActor : MonoBehaviour {

	#region PickUp variables
	// increase fire rate pick up
	[Tooltip("How long the player will have an increased fire rate.")]
	[SerializeField] private float m_fIncreaseFireRateEffectDuration;

	[Tooltip("How many times faster the fire rate will be.")]
	[SerializeField] private float m_fIncreaseFireRateEffectMagnitude;

	// immunity pick up
	[Tooltip("How long the player will be immune to enemy spells.")]
	[SerializeField] private float m_fImmunityEffectDuration;

	// slow down time pick up
	[Tooltip("Time in seconds that slow down time pick up lasts.")]
	[SerializeField] private float m_fSlowDownTimeEffectDuration;

	[Tooltip("The rate time will pass while time is slowed.")]
	[SerializeField] [Range(0.05f, 1)] float m_fSlowedTimeEffectMagnitude;

	// homing spells pick up
	[Tooltip("How long the player's spells will be homing.")]
	[SerializeField] private float m_fHomingSpellsEffectDuration;

	// scatter spells pick up
	[Tooltip("How long the player's spells will be scatter.")]
	[SerializeField] private float m_fScatterSpellsEffectDuration;
	#endregion

	private sPickUp m_pickUp;

	private float m_fSlowedTime;

	// Use this for initialization
	void Start () {

		// scale slow down time effect time
		m_fSlowedTime = m_fSlowDownTimeEffectDuration * m_fSlowedTimeEffectMagnitude;

		// randomly determine pickup type
		switch(UnityEngine.Random.Range(0, 6)) {
			case 0:
				m_pickUp.type = ePickUpType.INCREASE_FIRE_RATE;
				m_pickUp.duration = m_fIncreaseFireRateEffectDuration;
				m_pickUp.magnitude = m_fIncreaseFireRateEffectMagnitude;
				break;

			case 1:
				m_pickUp.type = ePickUpType.IMMUNITY;
				m_pickUp.duration = m_fImmunityEffectDuration;
				break;

			case 2:
				m_pickUp.type = ePickUpType.SLOW_DOWN_TIME;
				m_pickUp.duration = m_fSlowedTime;
				m_pickUp.magnitude = m_fSlowedTimeEffectMagnitude;
				break;

			case 3:
				m_pickUp.type = ePickUpType.HOMING_SPELLS;
				m_pickUp.duration = m_fHomingSpellsEffectDuration;
				break;

			case 4:
				m_pickUp.type = ePickUpType.SCATTER_SPELLS;
				m_pickUp.duration = m_fScatterSpellsEffectDuration;
				break;

			case 5:
				m_pickUp.type = ePickUpType.BOMB;
				break;

			case 6:
				m_pickUp.type = ePickUpType.SHIELD;
				break;

			default:
				m_pickUp.type = ePickUpType.ERROR;
				break;
		}

	}

	private void OnTriggerEnter(Collider other)
	{

		// check that collided with player
		if (other.gameObject.tag == "Player") {
			// try to give player pick up
			try {
				if (m_pickUp.type == ePickUpType.BOMB) {
					other.gameObject.GetComponent<PlayerActor>().AddToPlayerBombCount((int)m_pickUp.magnitude);
				}
				else if (m_pickUp.type == ePickUpType.SHIELD) {
					other.gameObject.GetComponent<PlayerActor>().m_lifeState = eLifeState.SHIELDED;
				}
				else {
					other.gameObject.GetComponent<PlayerActor>().m_currentPickUp = m_pickUp;
				}
			}
			catch (Exception e) {
				Debug.LogError(e.Message, gameObject);
			}

			// destroy the pick up
			Destroy(gameObject);
		}

	}
}