using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWinScreen : MonoBehaviour {

	void Start() {

		if (GameObject.FindGameObjectWithTag("WinScreen")) {
			GameObject winScreen = GameObject.FindGameObjectWithTag("WinScreen");
			winScreen.SetActive(false);
			SceneManager.Instance.WinScreen = winScreen;
		}
	}

	public void OnNextLevelPressed() {

		// move to next level
		if (SceneManager.Instance.CurrentScene.index + 1 <= SceneManager.Instance.GameScenes.Length - 1) {
			SceneManager.Instance.LoadScene(SceneManager.Instance.GameScenes[SceneManager.Instance.CurrentScene.index + 1], UnityEngine.SceneManagement.LoadSceneMode.Single);
		}
		else {
			SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneWithName("MainMenu"), UnityEngine.SceneManagement.LoadSceneMode.Single);
		}
	}

	public void OnQuitPressed() {

		// move to main menu
		SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneWithName("MainMenu"), UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
}
