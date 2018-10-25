///<summary>
///		Script Manager:	Denver
///		Description:	Handles the functionality of the LevelSection.
///						Stores all enemies of its section and loops player.
///		Date Modified:	25/10/2018
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCompletionState
{
	NOT_CLEARED,
	CLEARED,
	FAILED
}

public class LevelSection : MonoBehaviour {

	[Tooltip("Number of attempts the player is given to complete a section before the section is skipped.")]
	[SerializeField] private int m_nNumberOfAttempts;

	[Tooltip("List of all the enemies in this section.")]
	public List<EnemyActor> m_enemiesList;

	private eCompletionState m_completionState;

	private int m_nCurrentSectionAttempts;

	void Start() {
		
		foreach(EnemyActor enemy in m_enemiesList) {
			enemy.m_section = this;
		}

		m_completionState = eCompletionState.NOT_CLEARED;

		m_nCurrentSectionAttempts = 0;

		// check tag
		if (gameObject.tag != "LevelSection") {
			Debug.LogError("tag is not LevelSection.", gameObject);
		}

	}

	void FixedUpdate() {
		
		int enemyKillCount = 0;

		//check if player has defeated all enemies in the section
		foreach(EnemyActor enemy in m_enemiesList) {
			if(!enemy.m_bIsAlive) {
				++enemyKillCount;
			}
		}

		if (m_nNumberOfAttempts != 0) {
			// if player has run out of attempts
			if (m_nCurrentSectionAttempts > m_nNumberOfAttempts) {
				m_completionState = eCompletionState.FAILED;
			}
		}
		
		// if all enemies have been killed
		if (enemyKillCount == m_enemiesList.Count) {
			m_completionState = eCompletionState.CLEARED;
		}

	}

	void OnTriggerEnter(Collider other) {

		// check that other is a player
		if (other.tag == "Player") {
			// set player's CurrentSection to this
			other.gameObject.GetComponent<PlayerActor>().m_currentSection = this;

			// activate all enemies of the section and set their target
			foreach(EnemyActor enemy in m_enemiesList) {
				enemy.Activate(other.gameObject, GameObject.FindGameObjectWithTag("Playfield"));
			}
		}

	}

	void OnTriggerExit(Collider other) {

		// check that other is a playerMovementArea
		if (other.tag == "Playfield") {
			// section is not cleared
			if (m_completionState == eCompletionState.NOT_CLEARED) {
				// get the PlayerActor
				GameObject playfield = other.gameObject;

				// move player to the beginning of the section
				playfield.transform.position = new Vector3(playfield.transform.position.x, playfield.transform.position.y, (transform.position.z - GetComponent<BoxCollider>().size.z / 2) - (playfield.GetComponent<BoxCollider>().size.z / 2));

				// increment attempts
				++m_nCurrentSectionAttempts;
			}

			// section is failed
			else if (m_completionState == eCompletionState.FAILED) {
				foreach(EnemyActor enemy in m_enemiesList) {
					enemy.Deactivate();
				}
			}
		}

	}
}