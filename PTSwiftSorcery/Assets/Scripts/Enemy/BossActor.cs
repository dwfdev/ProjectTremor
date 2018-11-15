using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//all variables the boss has that change with phase
[System.Serializable]
public struct sBossPhase
{
	public bool trackPlayer;
	public float rotationSpeed;
	public eEnemyAIType EnemyAIType;
	public float maxMovementSpeed;
	public float movementSmooth;
	public Vector3 offset;
	public Transform[] waypoints;
	public float[] delays;
	public bool loopWaypoints;
	public GameObject[] patterns;
}

public class BossActor : EnemyActor
{
	[Header("Boss Variables")]
	[Tooltip("The initial patterns of the boss")]
	[SerializeField] private GameObject[] m_Patterns;

	[Tooltip("The health values at which the boss changes phases, in descending order")]
	[SerializeField] private int[] m_nHealthThresholds;

	[Tooltip("The list of phases for a boss")]
	[SerializeField] private sBossPhase[] m_BossPhases;


	private int m_nNextPhase;

	private bool m_bLastPhase;

	// Use this for initialization
	protected override void Start()
	{
		base.Start();

		m_nNextPhase = 0;

		if (m_nHealthThresholds.Length != m_BossPhases.Length)
		{
			Debug.LogError(gameObject.name + " has mismatching health thresholds and phases!");
		}
	}

	public override void TakeDamage(int damage)
	{
		//deal the damage
		m_nCurrentHealth -= damage;

		if(!m_bLastPhase)
		{
			if (m_nCurrentHealth <= m_nHealthThresholds[m_nNextPhase])
			{
				ChangePhase();

				if (m_nNextPhase >= m_BossPhases.Length)
				{
					m_bLastPhase = true;
					Debug.Log("This is the last phase");
				}
			}
		}

		// if enemy runs out of health
		if (m_nCurrentHealth <= 0)
		{
			Die();
		}

		GetComponent<UIHitEffect>().Show();
	}

	private void ChangePhase()
	{
		sBossPhase nextPhase = m_BossPhases[m_nNextPhase];

		m_bTrackPlayer = nextPhase.trackPlayer;
		m_fRotationSpeed = nextPhase.rotationSpeed;
		m_enemyAIType = nextPhase.EnemyAIType;
		m_fMaxMovementSpeed = nextPhase.maxMovementSpeed;
		m_fMovementSmoothing = nextPhase.movementSmooth;
		m_v3Offset = nextPhase.offset;
		m_waypoints = nextPhase.waypoints;
		m_delays = nextPhase.delays;
		m_bLoopWaypoints = nextPhase.loopWaypoints;

		foreach(GameObject pattern in m_Patterns)
		{
			pattern.SetActive(false);
		}

		m_Patterns = nextPhase.patterns;

		foreach(GameObject pattern in m_Patterns)
		{
			pattern.SetActive(true);
		}

		++m_nNextPhase;
	}
}
