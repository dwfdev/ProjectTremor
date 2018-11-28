using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>
///		Script Manager: Denver
///		Description:	Singleton class that handles the state of the scene and switching
///						between scenes
///		Date Modified:	25/10/2018
///</summary>

public enum eSceneState {
	RUNNING,
	BOSS_FIGHT,
	PAUSED,
	COMPLETE,
	FAILED
}

public enum eSceneType {
	MAIN_MENU,
	LEVEL_1,
	LEVEL_2,
	LEVEL_3,
	CREDITS,
	NULL
}

[System.Serializable]
public struct sGameScene {
	public string name;
	public eSceneType type;
	public int index;
	public Scene scene;
	public CursorLockMode lockMode;
}

[System.Serializable]
public struct sGameDifficulty {
	public string name;
	public int numOfLives;
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

			// set current scene
			m_currentScene.scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			m_currentScene.name = m_currentScene.scene.name;
			m_currentScene.index = m_currentScene.scene.buildIndex;

			// initialise game scene data
			for(int i = 0; i < m_gameScenes.Length; ++i) {
			m_gameScenes[i].scene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(m_gameScenes[i].index); 
			}

			// let SceneManager trascend scenes
			DontDestroyOnLoad(instance);
		}
		else {
			Destroy(gameObject);
		}
	}

	private SceneManager() {}
	#endregion

	private eSceneState m_sceneState = eSceneState.RUNNING;
	public eSceneState SceneState {
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

	private sGameScene m_currentScene;
	public sGameScene CurrentScene {
		get {
			return m_currentScene;
		}
	}

	private sGameScene m_previousScene;

	[Header("Design Settings")]

	[Tooltip("Difficulties")]
	[SerializeField] private sGameDifficulty[] m_difficulties;
	public sGameDifficulty[] Difficulties {
		get {
			return m_difficulties;
		}
	}

	[Header("")]

	[Tooltip("List of all scenes in the game")]
	[SerializeField] private sGameScene[] m_gameScenes;
	public sGameScene[] GameScenes {
		get {
			return m_gameScenes;
		}
	}

	#region Screen Objects
	private GameObject m_pauseMenu;
	public GameObject PauseMenu {
		get {
			return m_pauseMenu;
		}

		set {
			if (value) {
				m_pauseMenu = value;
			}
		}
	}

	private GameObject m_deathScreen;
	public GameObject DeathScreen {
		get {
			return m_deathScreen;
		}

		set {
			if(value) {
				m_deathScreen = value;
			}
		}
	}

	private GameObject m_winScreen;
	public GameObject WinScreen {
		get {
			return m_winScreen;
		}

		set {
			if (value) {
				m_winScreen = value;
			}
		}
	}
	#endregion

	#region User Settings
	private bool m_bIsWitch = true;
	public bool IsWitch {
		get {
			return m_bIsWitch;
		}
		set {
			m_bIsWitch = value;
		}
	}

	private sGameDifficulty m_currentDifficulty;
	public sGameDifficulty CurrentDifficulty {
		get {
			return m_currentDifficulty;
		}
		set {
			m_currentDifficulty = value;
		}
	}
	
	private float m_mouseSensitivity = 0.5f;
	public float MouseSensitivity {
		get {
			return m_mouseSensitivity;
		}
		set {
			m_mouseSensitivity = value;
		}
	}
	
	private bool m_bBloomOn = true;
	public bool BloomOn {
		get {
			return m_bBloomOn;
		}
		set {
			m_bBloomOn = value;
		}
	}
	#endregion

	#region Player Stats
	private int m_nSpellLevel;
	public int SpellLevel {
		get {
			return m_nSpellLevel;
		}
		set {
			m_nSpellLevel = value;
		}
	}
	#endregion

	void SceneStateChangedToRUNNING() {
		
		// change time scale and fixed delta time
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;

		// lock cursor
		Cursor.lockState = m_currentScene.lockMode;
		Cursor.visible = false;
		
		if (PauseMenu) {
			PauseMenu.SetActive(false);
		}

		if (DeathScreen) {
			DeathScreen.SetActive(false);
		}

		GameObject player;
		if ((player = GameObject.FindGameObjectWithTag("Player"))) {
			player.GetComponent<PlayerActor>().m_bCanMove = true;
		}
	}

	void SceneStateChangedToBOSS_FIGHT() {

		// change to boss music
		MusicManager musicManager;
		if ((musicManager = GameObject.FindObjectOfType<MusicManager>())) {
			if (!musicManager.BossMusic.isPlaying) {
				musicManager.StartBossMusic();
			}
		}
	}

	void SceneStateChangedToPAUSED() {
	
		// unlock cursor
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		
		// change time scale
		Time.timeScale = 0f;

		// display pause menu
		if (PauseMenu) {
		PauseMenu.SetActive(true);
		}

		// check if a player GameObject exists
		GameObject player;
		if ((player = GameObject.FindGameObjectWithTag("Player"))) {
			player.GetComponent<PlayerActor>().m_bCanMove = false;
		}
	}

	void SceneStateChangedToCOMPLETE() {

		// unlock cursor
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		
		// change time scale
		Time.timeScale = 0f;

		// display winScreen
		if (WinScreen) {
			WinScreen.SetActive(true);
		}

		// check if a player GameObject exists
		GameObject player;
		if ((player = GameObject.FindGameObjectWithTag("Player"))) {
			player.GetComponent<PlayerActor>().m_bCanMove = false;
		}

		// start victory music
		MusicManager musicManager;
		if((musicManager = GameObject.FindObjectOfType<MusicManager>())) {
			musicManager.StartVictoryMusic();
		}
	}

	void SceneStateChangedToFAILED() {

		// unlock cursor
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		// change time scale
		Time.timeScale = 0f;

		// display deathScreen
		if (DeathScreen) {
			DeathScreen.SetActive(true);
		}

		// check if a player GameObject exists
		GameObject player;
		if ((player = GameObject.FindGameObjectWithTag("Player"))) {
			player.GetComponent<PlayerActor>().m_bCanMove = false;
		}

		// play failed music
		MusicManager musicManager;
		if((musicManager = GameObject.FindObjectOfType<MusicManager>())) {
			musicManager.StartFailedMusic();
		}
	}

	public void LoadScene(sGameScene newScene, LoadSceneMode loadSceneMode) {
		
		// change previous scene to what used to be the current scene
		m_previousScene = m_currentScene;

		// change current scene to desired scene
		m_currentScene = newScene;

		// load new scene
		UnityEngine.SceneManagement.SceneManager.LoadScene(m_currentScene.index, loadSceneMode);
	}

	public void UnloadCurrentScene() {

		// make sure there is a previous scene to return to
		if (m_previousScene.scene.isLoaded) {
			// save m_currentScene in an intermediate variable
			sGameScene intermediate = m_currentScene;

			// set currentScene to previousScene
			m_currentScene = m_previousScene;

			// set previousScene to intermediate
			m_previousScene = intermediate;

			// unload previous scene
			UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_previousScene.index);
		}
	}

	public void ReloadCurrentScene() {

		// load current scene
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	[Obsolete("Use GetGameSceneByType method instead.")]
	public sGameScene GetGameSceneWithName(string name) {

		foreach (sGameScene scene in m_gameScenes) {
			if (scene.name == name) {
				return scene;
			}
		}

		// if no found scene, return null sGameScene
		return new sGameScene() {
			name = "NULL",
			type = eSceneType.NULL,
			index = -1,
		};
	}

	public sGameScene GetGameSceneByType(eSceneType type) {

		foreach (sGameScene scene in m_gameScenes) {
			if (scene.type == type) {
				return scene;
			}
		}

		// if no scene of type exists, return null game scene
		return new sGameScene() {
			name = "NULL",
			type = eSceneType.NULL,
			index = -1,
		};
	}
}
