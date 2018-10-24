using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Script Manager: Drake
///Description:
///
///</summary>
public class ScoreManager : MonoBehaviour 
{
	//the current score
	[HideInInspector]
	public long m_lScore;

	//the current multiplier
	[HideInInspector]
	public float m_fMultiplier;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void AddScore(long scoreValue)
	{
		m_lScore += (long)Mathf.Round(scoreValue * m_fMultiplier);
	}

	public void AddMultiplier(float multiValue)
	{
		m_fMultiplier += multiValue;
	}
}
