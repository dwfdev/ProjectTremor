using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelect : MonoBehaviour {

	[Tooltip("Main Menu.")]
	[SerializeField] private GameObject m_mainMenu;

	[Header("Toggles")]

	[Tooltip("Toggle group.")]
	[SerializeField] private ToggleGroup m_characterToggles;

	[Tooltip("Witch toggle.")]
	[SerializeField] private Toggle m_witchToggle;
	
	public void OnBooleanChanged(Toggle toggle) {

		// check if toggle is set to false
		if (!toggle.isOn) {

			// if no toggles are true
			if (!m_characterToggles.AnyTogglesOn()) {
				toggle.isOn = true;
			}
		}
	}

	public void OnBeginClick() {

		// set SceneManager's IsWitch to equal witch toggle
		SceneManager.Instance.IsWitch = m_witchToggle.isOn; 

		// load level
		SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneByType(eSceneType.LEVEL_1), UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	public void OnBackClick() {

		// activate main menu
		m_mainMenu.SetActive(true);

		// deactivate character selct menu
		gameObject.SetActive(false);
	}
}
