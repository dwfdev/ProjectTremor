using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	[Tooltip("Witch prefab.")]
	[SerializeField] private GameObject m_witchPrefab;

	[Tooltip("Wizard prefab.")]
	[SerializeField] private GameObject m_wizardPrefab;

	[SerializeField] private bool isWitch;

	// Use this for initialization
	void Awake () {

		// create player object based on user's choice
		if (isWitch) {
			Instantiate(m_witchPrefab, transform.position, transform.rotation).GetComponent<PlayerActor>().transform.parent = transform.parent;
		}
		else if (!isWitch) {
			Instantiate(m_wizardPrefab, transform.position, transform.rotation).GetComponent<PlayerActor>().transform.parent = transform.parent;
		}
		else {
			Debug.LogError("Could not instantiate player object", gameObject);
		}
	}
}
