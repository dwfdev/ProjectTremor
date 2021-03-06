﻿using System.Collections;
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
		else {
			Debug.LogError("Could not find Pause Menu. Make sure tags are set.", gameObject);
		}
	}

	public void OnContinuePressed() {
		SceneManager.Instance.SceneState = eSceneState.RUNNING;
	}

	public void OnQuitPressed() {
		ScoreManager.Instance.ResetScore();
		SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneByType(eSceneType.MAIN_MENU), UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
}
