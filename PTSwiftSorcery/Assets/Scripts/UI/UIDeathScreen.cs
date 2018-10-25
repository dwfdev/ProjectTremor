using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles functionality of the Death Screen
///		Date Modified:	19/10/2018
///</summary>

public class UIDeathScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if (GameObject.FindGameObjectWithTag("DeathScreen")) {
			GameObject deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
			deathScreen.SetActive(false);
			SceneManager.Instance.DeathScreen = deathScreen;
		}
		else {
			Debug.LogError("Could not find Death Screen. Make sure tags are set.", gameObject);
		}
	}
	
	public void OnRestartPressed() {

		// load current scene again
		SceneManager.Instance.ReloadCurrentScene();
	}

	public void OnQuitPressed() {

		// load Main Menu scene
		SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneWithName("MainMenu"), UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
}
