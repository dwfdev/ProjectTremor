using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSection : MonoBehaviour {

	[Tooltip("List of all the enemies in this section.")]
	public List<EnemyActor> m_enemiesList;

	private bool m_bIsCleared;

	void Start() {
		
		foreach(EnemyActor enemy in m_enemiesList) {
			enemy.m_section = this;
		}

		m_bIsCleared = false;

	}

	void FixedUpdate() {
		if (m_enemiesList.Count == 0) {
			m_bIsCleared = true;
		}
	}

	void OnTriggerEnter(Collider other) {

		// check that other is a player
		if (other.tag == "Player") {
			// set player's CurrentSection to this
			other.gameObject.GetComponent<PlayerActor>().m_currentSection = this;

			// activate all enemies of the section and set their target
			foreach(EnemyActor enemy in m_enemiesList) {
				enemy.Activate(other.gameObject, other.gameObject.GetComponent<PlayerActor>().m_movementArea);
			}
		}

	}

	void OnTriggerExit(Collider other) {

		// check that other is a player
		if (other.tag == "PlayerMovementArea") {
			// section is not cleared
			if (!m_bIsCleared) {
				// get the PlayerActor
				GameObject playerMovementArea = other.gameObject;

				// move player to the beginning of the section
				playerMovementArea.transform.position = new Vector3(playerMovementArea.transform.position.x, playerMovementArea.transform.position.y, transform.position.z - (transform.localScale.z / 2f + playerMovementArea.transform.localScale.z / 2f));
			}
		}

	}
}