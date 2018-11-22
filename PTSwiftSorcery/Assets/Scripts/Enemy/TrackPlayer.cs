using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour {

	private GameObject m_player;

	[Tooltip("How fast this object rotates")]
	[SerializeField] private float m_fRotationSpeed;

	// Use this for initialization
	void Start ()
	{
		m_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 lookPos = m_player.transform.position - transform.position;
		lookPos.y = transform.position.y;

		Quaternion targetRotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_fRotationSpeed * Time.deltaTime);
	}
}
