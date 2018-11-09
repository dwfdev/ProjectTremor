using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActor : EnemyActor
{
	[Tooltip("The health values at which the boss changes phases, going down")]
	[SerializeField] private int[] m_nHealthThresholds;

	private int m_nCurrentPhase;


	// Use this for initialization
	void Start ()
	{
		m_nCurrentPhase = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
