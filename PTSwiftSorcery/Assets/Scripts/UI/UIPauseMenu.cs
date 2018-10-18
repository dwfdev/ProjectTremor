using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles the functionality of the pause menu
///		Date Modified:	18/10/2018
///</summary>

public class UIPauseMenu : MonoBehaviour {

	void Start() {

		// find pause menu
		GameObject pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
		
		// tell the scene manager of the object
		SceneManager.Instance.PauseMenu = pauseMenu;

		// deactivate the object
		pauseMenu.SetActive(false);
	}

	public void OnContinuePressed() {
		SceneManager.Instance.SceneState = eSceneState.RUNNING;
	}

	public void OnOptionsPressed() {
		SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneWithName("OptionsMenu"), UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	public void OnStartPressed() {
		SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneWithName("Level1"), UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	public void OnQuitPressed() {
		SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneWithName("MainMenu"), UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
}
