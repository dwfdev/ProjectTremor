using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkimming : MonoBehaviour
{
	[Tooltip("How many points should be gained per second while skimming")]
	[SerializeField] private long m_lSkimPoints;

	private bool m_bIsSkimming;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_bIsSkimming)
		{
			long skimPoints = (long)(m_lSkimPoints * Time.deltaTime);
			Debug.Log(skimPoints);
			ScoreManager.Instance.AddScore(skimPoints);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "EnemyBullet")
		{
			m_bIsSkimming = true;
			//Debug.Log("Skimming!");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "EnemyBullet")
		{
			m_bIsSkimming = false;
			//Debug.Log("Stopped skimming!");
		}
	}
}
