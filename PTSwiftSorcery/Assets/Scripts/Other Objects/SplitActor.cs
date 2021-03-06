﻿///<summary>
///		Script Manager:	Denver
///		Description:	Handles the functionality of the split mechanic
///						of the game.  moves player down chosen path based
///						on player's position relative to the split.
///		Date Modified:	04/10/2018
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class SplitActor : MonoBehaviour {

	[Tooltip("Speed at which the split will move the player movement area.")]
	[SerializeField] private float m_fMoveSpeed;

	[Tooltip("Position of the centre of the left side path. (X axis)")]
	[SerializeField] private float m_fLeftSideX;

	[Tooltip("Position of the centre of the right side path. (X axis)")]
	[SerializeField] private float m_fRightSideX;

	private float m_fDesiredX;

	void Start() {

		// guarantee collider and rb settings
		GetComponent<Collider>().isTrigger = true;
		GetComponent<Rigidbody>().isKinematic = true;

		// check tag
		if (gameObject.tag != "LevelSplit") {
			Debug.LogError("Tag is not LevelSplit", gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {

		// check that other is the player
		if (other.gameObject.tag == "Player") {
			// calculate which side the player is on.
			//if the player is on the right
			if (other.gameObject.transform.position.x - transform.position.x > 0f) {
				m_fDesiredX = m_fRightSideX;
			}
			else {
				m_fDesiredX = m_fLeftSideX;
			}
		}

	}

	void OnTriggerStay(Collider other) {

		// check that other is the player
		if (other.gameObject.tag == "Player") {
			// get player movement area
			GameObject playfield = GameObject.FindGameObjectWithTag("Playfield");

			// find direction to new centre
			Vector3 v3DesiredDirection = new Vector3(m_fDesiredX, 0f, 0f) - playfield.transform.position;
			v3DesiredDirection = new Vector3(v3DesiredDirection.x, 0f, 0f);
			v3DesiredDirection.Normalize();

			// move in direction
			playfield.transform.Translate(v3DesiredDirection * m_fMoveSpeed * Time.deltaTime);

			// if moveArea is more-or-less at destination, set its position to exactly that destination
			if (Vector2.Distance(new Vector2(m_fDesiredX, 0f), new Vector2(playfield.transform.position.x, 0f)) <= 0.1f) {
				playfield.transform.position = new Vector3(m_fDesiredX, playfield.transform.position.y, playfield.transform.position.z);
			}

		}

	}
}
