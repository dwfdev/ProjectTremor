using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsActor : MonoBehaviour {

	void FixedUpdate() {

		if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel") || Input.GetButtonDown("Jump")) {
			// load next scene
			SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneByType(eSceneType.MAIN_MENU), UnityEngine.SceneManagement.LoadSceneMode.Single);
		}
	}
}
