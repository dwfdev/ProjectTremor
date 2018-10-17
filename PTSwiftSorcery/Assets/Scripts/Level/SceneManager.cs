using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

///<summary>
///		Script Manager: Denver
///		Description:	Singleton class that handles the state of the scene and switching
///						between scenes
///		Date Modified:	17/10/2018
///</summary>

public enum eSceneState {
	RUNNING,
	BOSS_FIGHT,
	PAUSED,
	COMPLETE,
	FAILED
}

public class SceneManager : MonoBehaviour {

	#region Make a Singleton Class
	private static SceneManager instance = null;
	public static SceneManager Instance {
		get {
			// if SceneManager doesn't already exist
			if (instance == null) {
				// check that a GameObject doesn't already have a SceneManager component
				instance = GameObject.FindObjectOfType<SceneManager>();

				// if there isn't such a GameObject
				if (instance == null) {
					// create a GameObject for itself that won't be able to be replaced
					GameObject go = new GameObject();
					go.name = "SceneManager";
					instance = go.AddComponent<SceneManager>();
				}
			}
			return instance;
		}
	}

	void Awake() {
		if (instance == null) {
			// set instance
			instance = this;

			// get pause menu
			m_pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
			m_pauseMenu.SetActive(false);

			// get player
			m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActor>();
		}
		else {
			Destroy(gameObject);
		}
	}

	private SceneManager() {}
	#endregion

	private eSceneState m_sceneState = eSceneState.RUNNING;
	public eSceneState m_SceneState {
		get {
			return m_sceneState;
		}

		set {
			// if value is a scene state
			if (value.GetType() == typeof(eSceneState)) {
			// change value
			m_sceneState = value;

			// run respective code
			Invoke("SceneStateChangedTo" + value.ToString(), 0);
			}
		}
	}

	private GameObject m_pauseMenu;

	private PlayerActor m_player;

	void SceneStateChangedToRUNNING() {
		
		// change time scale and fixed delta time
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;

		// let player move
		m_player.m_bCanMove = true;

		// stop displaying
		m_pauseMenu.SetActive(false);

		// lock cursor
		Cursor.lockState = CursorLockMode.Locked;
	}

	void SceneStateChangedToPAUSED() {
	
		// change time scale
		Time.timeScale = 0f;

		// stop player from moving
		m_player.m_bCanMove = false;

		// display pause menu
		m_pauseMenu.SetActive(true);

		// unlock cursor
		Cursor.lockState = CursorLockMode.None;
	}
}
