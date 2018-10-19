using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles Main Menu functionality
///		Date Modified:	18/10/2018

public class UIMainMenu : MonoBehaviour {

	public void OnStartPressed() {
		SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneWithName("Level1"), UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	public void OnQuitPressed() {
		Application.Quit();
	}
}
