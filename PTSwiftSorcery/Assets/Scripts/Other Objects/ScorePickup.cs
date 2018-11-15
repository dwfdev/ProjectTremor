using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{

	//how much the score pickup is worth
	[HideInInspector] public long m_lScoreValue;

	//how fast the score pickup moves
	[HideInInspector] public float m_fMoveSpeed;

	//the rigidbody of this object
	private Rigidbody m_rigidbody;

	// Use this for initialization
	void Start()
	{
		//get rigidbody
		m_rigidbody = gameObject.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		//push score pickup
		m_rigidbody.AddForce(Vector3.back * m_fMoveSpeed);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//add score and destroy this pickup
			ScoreManager.Instance.AddScore(m_lScoreValue);
			Destroy(gameObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Playfield")
		{
			//destroy this pickup when it leaves playfield
			Destroy(gameObject);
		}
	}
}
