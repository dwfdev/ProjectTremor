using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsActor : MonoBehaviour {

	void FixedUpdate() {

		if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel") || Input.GetButtonDown("Jump")) {
			// load next scene
			SceneManager.Instance.LoadScene(SceneManager.Instance.GameScenes[SceneManager.Instance.CurrentScene.index + 1], UnityEngine.SceneManagement.LoadSceneMode.Single);
		}
	}
}
