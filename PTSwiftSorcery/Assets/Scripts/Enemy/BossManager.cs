using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {

	[Tooltip("Enemy that is the final boss.")]
	[SerializeField] private GameObject m_boss;

	void FixedUpdate() {

		if (!m_boss.activeSelf) {
			SceneManager.Instance.SceneState = eSceneState.COMPLETE;
		}
	}
}
