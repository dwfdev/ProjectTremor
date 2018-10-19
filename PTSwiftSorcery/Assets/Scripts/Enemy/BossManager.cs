using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {

	[Tooltip("Enemy that is the final boss.")]
	[SerializeField] private GameObject m_boss;

	[Tooltip("Delay after defeating boss")]
	[SerializeField] private float m_fDelay;

	void FixedUpdate() {

		if (!m_boss.activeSelf) {
			Invoke("ActivateWinState", m_fDelay);
		}
	}

	void ActivateWinState() {

		SceneManager.Instance.SceneState = eSceneState.COMPLETE;
	}
}
