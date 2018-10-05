using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager: Denver
///		Description:	Handles the functionality of the bomb.
///		Date Modified:	05/10/2018
///</summary>

public class BombActor : MonoBehaviour {

	[Tooltip("How long before the bomb explodes.")]
	[SerializeField] private float m_fFuseTime;

	[Tooltip("Where, relative to the player, the bomb will travel to.")]
	[SerializeField] private Vector3 m_v3BombDestination;

	[Tooltip("How long before the bomb reaches its destination.")]
	[SerializeField] private float m_fDestinationTime;

	[Tooltip("How much damage the bomb does to effected enemies.")]
	[SerializeField] private int m_nBombDamage;

	[Tooltip("Range of the bomb.")]
	[SerializeField] private float m_fBombRange;

	[Tooltip("How quickly spells are sucked into the bomb.")]
	[SerializeField] private float m_fBombSuckSpeed;

	private GameObject m_player;

	private float m_fTimer;

	private List<EnemySpellProjectile> m_allSpells;

	// Use this for initialization
	void Start () {

		// get the player
		m_player = GameObject.FindGameObjectWithTag("Player");

		// set parent
		transform.parent = FindObjectOfType<LevelManager>().m_playField.transform;
		
		// initialise timer
		m_fTimer = 0f;

	}
	
	// Update is called once per frame
	void Update () {
		
		// Move bomb to desired position
		transform.localPosition = Vector3.Lerp(transform.localPosition, m_v3BombDestination, (1 / m_fDestinationTime) * Time.deltaTime);

		// attract bullets
		m_allSpells = new List<EnemySpellProjectile>(FindObjectsOfType<EnemySpellProjectile>());

		foreach(EnemySpellProjectile spell in m_allSpells) {
			// calculate direction
			Vector3 desiredBulletDirection = (spell.transform.localPosition - transform.localPosition).normalized;

			// move in that direction
			spell.transform.localPosition += desiredBulletDirection * m_fBombSuckSpeed * Time.deltaTime;

			// disable spell if near
			if (Vector3.Distance(spell.transform.localPosition, transform.localPosition) <= 0.1f) {
				spell.enabled = false;

				// add score
			} 
		}

		// timer
		m_fTimer += Time.deltaTime;

		// if end of fuse
		if (m_fTimer >= m_fFuseTime) {
			BlowUp();
		}

	}

	void BlowUp() {

		// iterate through each enemy in the section
		foreach(EnemyActor enemy in m_player.GetComponent<PlayerActor>().m_currentSection.m_enemiesList) {
			// if enemy is within range
			if (Vector3.Distance(enemy.transform.localPosition, transform.localPosition) < m_fBombRange) {
				enemy.TakeDamage(m_nBombDamage);
			}
		}

		// clear list of spells
		m_allSpells.Clear();

		Destroy(gameObject);

	}
}
