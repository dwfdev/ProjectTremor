using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour {

	void OnContinuePressed() {
		SceneManager.Instance.m_SceneState = eSceneState.RUNNING;
	}
}
