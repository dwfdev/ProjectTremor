///<summary>
///		Script Manager:	Denver
///		Description:	Handles the enemy's AI.
///		Date Modiefied:	03/10/2018
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : MonoBehaviour {

	[HideInInspector]
	public bool m_bIsActive;

	[HideInInspector]
	public LevelSection m_section;

	[Tooltip("An integer amount of health the enemy has.  If this is lower than 0, the enemy will die.")]
	[SerializeField] private int m_nHealth;

	[Tooltip("How much score this enemy drops on death.")]
	[SerializeField] private float m_nWorth;

	[Tooltip("Maximum speed at which the enemy will move.")]
	[SerializeField] private float m_fMaxMovementSpeed;

	[Tooltip("How far infront the enemy will try to be from its target.")]
	[SerializeField] private float m_fOffset;

	[Tooltip("How smoothed the enemy's movement will be.")]
	[SerializeField] private float m_fMovementSmoothing;

	private Vector3 m_v3Velocity = Vector3.zero;
	private GameObject m_target;

	void Start()
	{

		// initialise the enemy as deactive
		m_bIsActive = false;

	}

	void Update()
	{

		if(m_bIsActive) {

			// desired position
			Vector3 desiredPosition = new Vector3(m_target.transform.localPosition.x, transform.localPosition.y, m_target.transform.localPosition.z + m_fOffset * transform.localScale.z);

			// smoothly move to that position
			transform.localPosition = Vector3.SmoothDamp(transform.localPosition, desiredPosition, ref m_v3Velocity, 1 / m_fMovementSmoothing, m_fMaxMovementSpeed * Time.deltaTime);
		}

	}

	public void Activate(GameObject target, GameObject newParent)
	{

		// set isActive to true
		m_bIsActive = true;

		// set target
		m_target = target;

		// make enemy position and movement relative to the player's movement area
		transform.parent = newParent.transform;

	}

	public void TakeDamage(int damage)
	{
		// deal the damage
		m_nHealth -= damage;

		// if enemy runs out of health
		if(m_nHealth <= 0) {
			Die();
		}
	}

	public void Die()
	{

		// remove enemy from the section
		m_section.m_enemiesList.Remove(this);

		Destroy(gameObject);
		
	}

	void CreateChild()
	{
		//try {
		//	Instantiate(childPrefab);

		//	childPrefab.GetComponent<EnemyActor>().m_section = m_section;
		//}
		//catch (Exception e) {
		//	Debug.Log(e.Message);
		//}
	}
}
