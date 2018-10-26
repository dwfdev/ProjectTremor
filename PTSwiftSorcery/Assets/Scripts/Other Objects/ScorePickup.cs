using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{

	[HideInInspector] public long m_lScoreValue;

	[HideInInspector] public float m_fMoveSpeed;

	private Rigidbody m_rigidbody;
	// Use this for initialization
	void Start()
	{
		m_rigidbody = gameObject.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		m_rigidbody.AddForce(Vector3.back * m_fMoveSpeed);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			ScoreManager.Instance.AddScore(m_lScoreValue);
			Destroy(gameObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Playfield")
		{
			Destroy(gameObject);
		}
	}
}
