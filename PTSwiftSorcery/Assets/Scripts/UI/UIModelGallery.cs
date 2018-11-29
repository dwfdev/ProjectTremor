using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager:	Denver
///		Description:	Will handle showcasing character models in the menus
///		Date Modified:	29/11/2018
///</summary>

public class UIModelGallery : MonoBehaviour {

	[Tooltip("Time in seconds until model changes")]
	[SerializeField] private float m_fTimeLimit;

	[Header("Models")]

	[Tooltip("Array of models")]
	[SerializeField] private GameObject[] m_models;

	private float m_fTimer;

	private int m_nCurrentModelIndex;

	// Use this for initialization
	void Start () {
		
		// deactivate
		foreach (GameObject model in m_models) {
			model.SetActive(false);
		}

		// initialise timer
		m_fTimer = m_fTimeLimit;
	}
	
	void FixedUpdate () {

		// add time to timer
		m_fTimer += Time.deltaTime;

		if (m_fTimer >= m_fTimeLimit) {
			ChangeModel();
		}
	}

	void ChangeModel() {

		// initialise a new index variable
		int newIndex = m_nCurrentModelIndex;

		// generate random indexes until a new index is selected
		while (newIndex == m_nCurrentModelIndex && m_models.Length > 1) {
			newIndex = Random.Range(0, m_models.Length);
		}

		// deactivate current model
		m_models[m_nCurrentModelIndex].SetActive(false);

		// activate new model
		m_models[newIndex].SetActive(true);

		// update current model index
		m_nCurrentModelIndex = newIndex;

		m_fTimer = 0f;
	}
}
