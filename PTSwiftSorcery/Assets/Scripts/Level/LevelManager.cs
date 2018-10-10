using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager: Denver
///		Description:	Handles the difficulty of the level, scroll direction and speed as well as moves the player
///		Date Modified:	03/10/2018
///</summary>

public enum eLevelDifficulty
{
	EASY,
	NORMAL,
	HARD
}

public class LevelManager : MonoBehaviour
{

	[Tooltip("The play field of the level.")]
	public GameObject m_playField;

	[Tooltip("The speed at which the level will scroll.")]
	public float m_fLevelScrollSpeed;

	[Tooltip("The direction the level will scroll.")]
	public Vector3 m_v3LevelScrollDirection;

	[HideInInspector]
	public eLevelDifficulty m_levelDifficulty;

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
		m_playField.transform.Translate(m_v3LevelScrollDirection * m_fLevelScrollSpeed * Time.deltaTime);

	}

	public void SetLevelDifficulty(eLevelDifficulty difficulty)
	{

		// change level difficulty
		m_levelDifficulty = difficulty;

	}
}
