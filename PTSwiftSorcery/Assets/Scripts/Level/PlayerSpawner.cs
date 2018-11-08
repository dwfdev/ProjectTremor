using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpawner : MonoBehaviour {

	[Tooltip("Witch prefab.")]
	[SerializeField] private GameObject m_witchPrefab;

	[Tooltip("Wizard prefab.")]
	[SerializeField] private GameObject m_wizardPrefab;

	// Use this for initialization
	void Awake () {

		// create player object based on user's choice
		if (SceneManager.Instance.IsWitch) {
			Instantiate(m_witchPrefab, transform.position, transform.rotation).GetComponent<PlayerActor>().transform.parent = transform.parent;
		}
		else if (!SceneManager.Instance.IsWitch) {
			Instantiate(m_wizardPrefab, transform.position, transform.rotation).GetComponent<PlayerActor>().transform.parent = transform.parent;
		}
		else {
			Debug.LogError("Could not instantiate player object", gameObject);
		}
	}
}
