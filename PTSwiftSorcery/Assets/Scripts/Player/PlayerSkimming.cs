using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkimming : MonoBehaviour
{
	[Tooltip("How many points should be gained per second while skimming")]
	[SerializeField] private long m_lSkimPoints;

	[Tooltip("The particle system skimming is attached to")]
	[SerializeField] private ParticleSystem m_particleSystem;

	//if the player is currently skimming
	private bool m_bIsSkimming;

	private void Awake()
	{
		ParticleSystem.MainModule psmain = m_particleSystem.main;
		psmain.customSimulationSpace = GameObject.FindGameObjectWithTag("Playfield").transform;
	}

	// Update is called once per frame
	void Update ()
	{
		//if player is currently skimming, add score
		if(m_bIsSkimming)
		{
			ScoreManager.Instance.AddScore((long)(m_lSkimPoints * Time.deltaTime));
			m_bIsSkimming = false;
			m_particleSystem.Emit(1);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		//if near an enemy bullet, start skimming
		if (other.tag == "EnemyBullet")
			m_bIsSkimming = true;
	}
}
