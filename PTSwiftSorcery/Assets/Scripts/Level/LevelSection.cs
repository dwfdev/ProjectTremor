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

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class LevelSection : MonoBehaviour {

	[Tooltip("Number of attempts the player is given to complete a section before the section is skipped.")]
	[SerializeField] private int m_nNumberOfAttempts;

	[Tooltip("List of all the enemies in this section.")]
	public List<EnemyActor> m_enemiesList;

	[Tooltip("If this section is the final boss battle.")]
	[SerializeField] private bool m_bIsBossFight;

	[Tooltip("Whether scrolling should stop when in boss fight.")]
	[SerializeField] private bool m_bStopScrollingOnBossFight;

	private eCompletionState m_completionState;

	private int m_nCurrentSectionAttempts;

	void Start() {

		// guarantee collider and rb settings
		GetComponent<Collider>().isTrigger = true;
		GetComponent<Rigidbody>().isKinematic = true;
		
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

		// if designer wants infinite attempts
		if (m_nNumberOfAttempts != 0) {
			// if player has run out of attempts
			if (m_nCurrentSectionAttempts > m_nNumberOfAttempts) {
				m_completionState = eCompletionState.FAILED;
			}
		}

		// if designer wants no looping
		if (m_nNumberOfAttempts == -1) {
			m_completionState = eCompletionState.CLEARED;
		}
		
		// if all enemies have been killed
		if (enemyKillCount == m_enemiesList.Count) {
			m_completionState = eCompletionState.CLEARED;

			// if this was a stationary level section
			if (m_bStopScrollingOnBossFight) {
				// continue scrolling
				LevelManager levelManager = FindObjectOfType<LevelManager>();
				levelManager.m_fLevelScrollSpeed = levelManager.MaxScrollSpeed;
			}
		}

	}

	void OnTriggerEnter(Collider other) {

		// check that other is a player
		if (other.tag == "TriggerOperator") {
			// set player's CurrentSection to this
			FindObjectOfType<PlayerActor>().m_currentSection = this;

			// activate all enemies of the section and set their target
			if (m_nCurrentSectionAttempts == 0) {
				foreach(EnemyActor enemy in m_enemiesList) {
					enemy.Activate(other.gameObject, GameObject.FindGameObjectWithTag("Playfield"));

					if (enemy.GetComponent<UIHealthBar>()) {
						enemy.GetComponent<UIHealthBar>().enabled = true;
					}
				}
			}

			// if this is a boss fight
			if (m_bIsBossFight) {
				// set scene state to BOSS_FIGHT
				SceneManager.Instance.SceneState = eSceneState.BOSS_FIGHT;
			}

			// if LevelSection is non scrolling
			if (m_bStopScrollingOnBossFight) {
				LevelManager levelManager = FindObjectOfType<LevelManager>();
				levelManager.m_fLevelScrollSpeed = 0f;
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

				return;
			}

			// section is failed
			else if (m_completionState == eCompletionState.FAILED) {
				foreach(EnemyActor enemy in m_enemiesList) {
					enemy.Deactivate();
					enemy.GetComponent<UIHealthBar>().DestroyHealthBar();
				}
			}

			foreach(EnemyActor enemy in m_enemiesList) {
				Destroy(enemy.gameObject, 3f);
			}

			Destroy(gameObject, 3f);
		}

	}
}