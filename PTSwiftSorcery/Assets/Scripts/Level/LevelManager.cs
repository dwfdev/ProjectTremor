using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager: Denver
///		Description:	Handles the difficulty of the level, scroll direction and speed as well as moves the player
///		Date Modified:	31/10/2018
///</summary>

public enum eLevelDifficulty
{
	EASY,
	NORMAL,
	HARD
}

public class LevelManager : MonoBehaviour
{

	[Header("Scrolling Level")]
	[Tooltip("The play field of the level.")]
	public GameObject m_playField;

	[Tooltip("The speed at which the level will scroll.")]
	public float m_fLevelScrollSpeed;

	[Tooltip("The direction the level will scroll.")]
	public Vector3 m_v3LevelScrollDirection;

	[Header("Slow Down Time Effect")]
	[Tooltip("How sclow the game will become due to amount of bullets on screen.")]
	[SerializeField] private float m_fMinTimeScale;

	[Tooltip("How many bullets that need to be on screen until time scale reaches 0.")]
	[SerializeField] private float m_fTimeScaleCurve;

	[Tooltip("Controls how long it takes until slow down effect begins.")]
	[SerializeField] private float m_fTimeScaleCurveDropOff;

	[HideInInspector]
	public eLevelDifficulty m_levelDifficulty;

	[HideInInspector]
	public float m_fTimeScale;

	void Start()
	{

		// check that tag has been set
		if (gameObject.tag != "LevelManager") {
			Debug.LogError("Tag must be LevelManager", gameObject);
		}

		if (m_playField.tag != "Playfield") {
			Debug.LogError("Tag must be Playfield", m_playField);
		}

		// initialise level difficulty to EASY
		m_levelDifficulty = eLevelDifficulty.EASY;

		// set scene state to running
		SceneManager.Instance.SceneState = eSceneState.RUNNING;

	}

	void FixedUpdate()
	{

		// move the player's movement area
		m_playField.transform.Translate(m_v3LevelScrollDirection * m_fLevelScrollSpeed * Time.deltaTime);

		BombActor bombActor = GameObject.FindObjectOfType<BombActor>();
		if (!bombActor || !bombActor.m_bIsExploding) {
			// get all enemy bullets in scene
			GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
			
			// scale time scale
			m_fTimeScale = (1 / m_fTimeScaleCurve) * -Mathf.Log10(bullets.Length + 1) + m_fTimeScaleCurveDropOff;

			m_fTimeScale = Mathf.Clamp(m_fTimeScale, m_fMinTimeScale, 1f);

			Time.timeScale = m_fTimeScale;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;

			Debug.Log(Time.timeScale);
		}

	}

	public void SetLevelDifficulty(eLevelDifficulty difficulty)
	{

		// change level difficulty
		m_levelDifficulty = difficulty;

	}
}
