using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct sBossPhase {
	public bool trackPlayer;
	public float rotationSpeed;
	public eEnemyAIType EnemyAIType;
	public float maxMovementSpeed;
	public float movementSmooth;
}

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
