using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkimming : MonoBehaviour
{
	[Tooltip("How many points should be gained per second while skimming")]
	[SerializeField] private long m_lSkimPoints;

	//if the player is currently skimming
	private bool m_bIsSkimming;
	
	// Update is called once per frame
	void Update ()
	{
		//if player is currently skimming, add score
		if(m_bIsSkimming)
		{
			ScoreManager.Instance.AddScore((long)(m_lSkimPoints * Time.deltaTime));
			m_bIsSkimming = false;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		//if near an enemy bullet, start skimming
		if (other.tag == "EnemyBullet")
			m_bIsSkimming = true;
	}
}
