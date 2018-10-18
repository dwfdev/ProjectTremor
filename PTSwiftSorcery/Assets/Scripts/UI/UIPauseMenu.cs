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

		if (GameObject.FindGameObjectWithTag("PauseMenu")) {
			GameObject pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
			pauseMenu.SetActive(false);
			SceneManager.Instance.PauseMenu = pauseMenu;
		}
	}

	public void OnContinuePressed() {
		SceneManager.Instance.SceneState = eSceneState.RUNNING;
	}

	public void OnOptionsPressed() {
		SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneWithName("OptionsMenu"), UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	public void OnQuitPressed() {
		SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneWithName("MainMenu"), UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
}
