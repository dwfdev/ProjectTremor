using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager: Denver
///		Description:	Handles the difficulty of the level, scroll direction and speed as well as moves the player
///		Date Modified:	1/11/2018
///</summary>

public class LevelManager : MonoBehaviour
{

	[Tooltip("The play field of the level.")]
	public GameObject m_playField;

	[Tooltip("The speed at which the level will scroll.")]
	public float m_fLevelScrollSpeed;

	[Tooltip("Maximum scroll speed.")]
	private float m_fMaxLevelScrollSpeed;
	public float MaxScrollSpeed {
		get {
			return m_fMaxLevelScrollSpeed;
		}
	}

	[Tooltip("The direction the level will scroll.")]
	public Vector3 m_v3LevelScrollDirection;

	void Start()
	{

		// check that tag has been set
		if (gameObject.tag != "LevelManager") {
			Debug.LogError("Tag must be LevelManager", gameObject);
		}

		if (m_playField.tag != "Playfield") {
			Debug.LogError("Tag must be Playfield", m_playField);
		}

		// set Max scroll speed
		m_fMaxLevelScrollSpeed = m_fLevelScrollSpeed;

		// set scene state to running
		SceneManager.Instance.SceneState = eSceneState.RUNNING;

	}

	void FixedUpdate()
	{

		// move the player's movement area
		m_playField.transform.Translate(m_v3LevelScrollDirection * m_fLevelScrollSpeed * Time.deltaTime);

	}
}
