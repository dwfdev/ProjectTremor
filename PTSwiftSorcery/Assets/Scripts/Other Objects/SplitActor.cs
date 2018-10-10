///<summary>
///		Script Manager:	Denver
///		Description:	Handles the functionality of the split mechanic
///						of the game.  moves player down chosen path based
///						on player's position relative to the split.
///		Date Modified:	04/10/2018
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitActor : MonoBehaviour {

	[Tooltip("Speed at which the split will move the player movement area.")]
	[SerializeField] private float m_fMoveSpeed;

	[Tooltip("Position of the centre of the left side path. (X axis)")]
	[SerializeField] private float m_fLeftSideX;

	[Tooltip("Position of the centre of the right side path. (X axis)")]
	[SerializeField] private float m_fRightSideX;

	[Tooltip("Left side level difficulty.")]
	[SerializeField] private eLevelDifficulty m_leftDifficulty;

	[Tooltip("Right side level difficulty.")]
	[SerializeField] private eLevelDifficulty m_rightDifficulty;

	private float m_fDesiredX;

	private eLevelDifficulty m_desiredDifficulty;

	void OnTriggerEnter(Collider other) {

		// check that other is the player
		if (other.gameObject.tag == "Player") {
			// calculate which side the player is on.
			//if the player is on the right
			if (other.gameObject.transform.position.x - transform.position.x > 0f) {
				m_fDesiredX = m_fRightSideX;
				m_desiredDifficulty = m_rightDifficulty;
			}
			else {
				m_fDesiredX = m_fLeftSideX;
				m_desiredDifficulty = m_leftDifficulty;
			}

			// set new difficulty
			FindObjectOfType<LevelManager>().SetLevelDifficulty(m_desiredDifficulty);
		}

	}

	void OnTriggerStay(Collider other) {

		// check that other is the player
		if (other.gameObject.tag == "Player") {
			// get player movement area
			GameObject moveArea = other.GetComponent<PlayerActor>().m_movementArea;

			// find direction to new centre
			Vector3 v3DesiredDirection = new Vector3(m_fDesiredX, 0f, 0f) - moveArea.transform.position;
			v3DesiredDirection = new Vector3(v3DesiredDirection.x, 0f, 0f);
			v3DesiredDirection.Normalize();

			// move in direction
			moveArea.transform.Translate(v3DesiredDirection * m_fMoveSpeed * Time.deltaTime);

			// if moveArea is more-or-less at destination, set its position to exactly that destination
			if (Vector2.Distance(new Vector2(m_fDesiredX, 0f), new Vector2(moveArea.transform.position.x, 0f)) <= 0.1f) {
				moveArea.transform.position = new Vector3(m_fDesiredX, moveArea.transform.position.y, moveArea.transform.position.z);
			}

		}

	}
}
