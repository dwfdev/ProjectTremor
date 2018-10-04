﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager: Denver
///		Description:	Handles the difficulty of the level, scroll direction and speed as well as moves the player
///		Date Modified:	03/10/2018
///</summary>

///<summary>
///		This enum will handle the difficulty of the level
/// </summary>
public enum eLevelDifficulty
{
	EASY,
	NORMAL,
	HARD
}

public class LevelManager : MonoBehaviour
{

	[Tooltip("The player movement area of the level.")]
	[SerializeField]
	private GameObject m_playerMovementArea;

	[Tooltip("The speed at which the level will scroll.")]
	public float m_fLevelScrollSpeed;

	[Tooltip("The direction the level will scroll.")]
	public Vector3 m_v3LevelScrollDirection;

	private eLevelDifficulty m_levelDifficulty;

	void Start()
	{

		if (gameObject.tag != "LevelManager") {
			Debug.LogError("Tag must be LevelManager", gameObject);
		}

		// initialise level difficulty to EASY
		m_levelDifficulty = eLevelDifficulty.EASY;

		// hide cursor
		Cursor.lockState = CursorLockMode.Locked;

	}

	void FixedUpdate()
	{

		// move the player's movement area
		m_playerMovementArea.transform.Translate(m_v3LevelScrollDirection * m_fLevelScrollSpeed * Time.deltaTime);

	}

	public void SetLevelDifficulty(eLevelDifficulty difficulty)
	{

		// change level difficulty
		m_levelDifficulty = difficulty;

	}
}