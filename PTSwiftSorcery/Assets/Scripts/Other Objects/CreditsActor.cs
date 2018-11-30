using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsActor : MonoBehaviour {

	void Update() {

		if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel") || Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) {
			// load next scene
			SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneByType(eSceneType.MAIN_MENU), UnityEngine.SceneManagement.LoadSceneMode.Single);
		}
	}
}
