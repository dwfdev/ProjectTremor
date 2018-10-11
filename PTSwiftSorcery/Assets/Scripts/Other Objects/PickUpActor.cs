using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager: Denver
///		Description:	Handles the behaviour of the pick-up
///		Date Modified:	11/10/2018
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
	NULL
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

	// increased fire rate
	[Header("Increased Fire Rate.")]

	[Tooltip("How long the player will have an increased fire rate.")]
	[SerializeField] private float m_fIFRDuration;

	[Tooltip("How many times faster the fire rate will be.")]
	[SerializeField] private float m_fIFRMagnitude;

	[Tooltip("Probablity of Increased Fire Rate pickup. Makue sure all probabilties sum to 100.")]
	[SerializeField] [Range(0, 100)] private int m_nIFRProbability;

	// immunity pick up
	[Header("Immunity.")]

	[Tooltip("How long the player will be immune to enemy spells.")]
	[SerializeField] private float m_fImmunityDuration;

	[Tooltip("Probability of Immunity pickup. Makue sure all probabilties sum to 100.")]
	[SerializeField] [Range(0, 100)] private int m_nImmunityProbabilty;

	// slow down time pick up
	[Header("Slow Down Time.")]

	[Tooltip("Time in seconds that slow down time pick up lasts.")]
	[SerializeField] private float m_fSDTDuration;

	[Tooltip("The rate time will pass while time is slowed.")]
	[SerializeField] [Range(0.00001f, 1)] float m_fSTDMagnitude;
	
	[Tooltip("Probability of Slow Down Time pickup. Makue sure all probabilties sum to 100.")]
	[SerializeField] [Range(0, 100)] private int m_nSTDProbabilty;

	// homing spells pick up
	[Header("Homing Spells.")]

	[Tooltip("How long the player's spells will be homing.")]
	[SerializeField] private float m_fHomingSpellDuration;

	[Tooltip("Probabilty of homing spells pickup. Makue sure all probabilties sum to 100.")]
	[SerializeField] [Range(0, 100)] private int m_nHomingSpellProbabilty;

	// scatter spells pick up
	[Header("Scatter Spells.")]

	[Tooltip("How long the player's spells will be scatter.")]
	[SerializeField] private float m_fScatterSpellDuration;

	[Tooltip("Probabilty of scatter spell pickup. Makue sure all probabilties sum to 100.")]
	[SerializeField] [Range(0, 100)] private int m_nScatterSpellProbabilty;

	// shield pick up
	[Header("Shield.")]

	[Tooltip("Probability of shield pick up. Make sure all probabilities sum to 100.")]
	[SerializeField] [Range(0, 100)] private int m_nShieldProbability;

	// bomb pick up
	[Header("Bomb.")]
	[SerializeField] [Range(0, 100)] private int m_nBombProbability;

	private sPickUp m_pickUp;

	private Dictionary<sPickUp, int> m_probabilities = new Dictionary<sPickUp, int>();

	// Use this for initialization
	void Start () {

		// set to child of the playfield
		transform.parent = GameObject.FindGameObjectWithTag("Playfield").transform;

		int totalProbability = m_nIFRProbability + m_nImmunityProbabilty + m_nShieldProbability + m_nBombProbability + m_nHomingSpellProbabilty + m_nScatterSpellProbabilty + m_nSTDProbabilty;

		if (totalProbability == 100) {
			Randomise();
		}
		else {
			m_pickUp.type = ePickUpType.NULL;
			Debug.LogError("Probabilities do not equal 100, but " + totalProbability, gameObject);
		}

		Debug.Log(m_pickUp.type);

	}

	private void OnTriggerEnter(Collider other)
	{

		// check that collided with player
		if (other.gameObject.tag == "Player") {
			// try to give player pick up
			try {
				if (m_pickUp.type != ePickUpType.NULL) {
					if (m_pickUp.type == ePickUpType.BOMB) {
						other.gameObject.GetComponent<PlayerActor>().AddToPlayerBombCount(1);
					}
					else if (m_pickUp.type == ePickUpType.SHIELD) {
						other.gameObject.GetComponent<PlayerActor>().m_lifeState = eLifeState.SHIELDED;
					}
					else {
						other.gameObject.GetComponent<PlayerActor>().SetPickUp(m_pickUp);
					}
				}
			}
			catch (Exception e) {
				Debug.LogError(e.Message, gameObject);
			}

			// destroy the pick up
			Destroy(gameObject);
		}

	}

	private void Randomise() {

		// scale slow down time effect time
		m_fSDTDuration *= m_fSTDMagnitude;

		// set up pick ups
		sPickUp ifr = new sPickUp() { duration = m_fIFRDuration, magnitude = m_fIFRMagnitude, type = ePickUpType.INCREASE_FIRE_RATE };
		sPickUp immunity = new sPickUp() { duration = m_fImmunityDuration, type = ePickUpType.IMMUNITY };
		sPickUp shield = new sPickUp() { type = ePickUpType.SHIELD };
		sPickUp sdt = new sPickUp() { duration = m_fSDTDuration, magnitude = m_fSTDMagnitude, type = ePickUpType.SLOW_DOWN_TIME };
		sPickUp homing = new sPickUp() { duration = m_fHomingSpellDuration, type = ePickUpType.HOMING_SPELLS };
		sPickUp scatter = new sPickUp() { duration = m_fScatterSpellDuration, type = ePickUpType.SCATTER_SPELLS };
		sPickUp bomb = new sPickUp() { type = ePickUpType.BOMB };

		// set up probabilities
		m_probabilities.Add(ifr, m_nIFRProbability);
		m_probabilities.Add(immunity, m_nImmunityProbabilty);
		m_probabilities.Add(shield, m_nShieldProbability);
		m_probabilities.Add(sdt, m_nSTDProbabilty);
		m_probabilities.Add(homing, m_nHomingSpellProbabilty);
		m_probabilities.Add(scatter, m_nScatterSpellProbabilty);
		m_probabilities.Add(bomb, m_nBombProbability);

		try {
			// create list of possibilities
			List<sPickUp> possibilites = new List<sPickUp>();

			// fill list:  Fills a list of 100 pickups with the right proportions
			foreach(KeyValuePair<sPickUp, int> pickUp in m_probabilities) {
				for (int i = 0; i < pickUp.Value; ++i) {
					possibilites.Add(pickUp.Key);
				}
			}

			// pick random pick up
			m_pickUp = possibilites[UnityEngine.Random.Range(0, 100)];
		}
		catch (Exception e) {
			Debug.Log(e.Message, gameObject);
		}

	}
}