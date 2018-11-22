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

	[Tooltip("Difficulty dropdown component.")]
	[SerializeField] private Dropdown m_difficultyDropdown;

	void Start() {

		List<Dropdown.OptionData> newOptions = new List<Dropdown.OptionData>();
		
		// fill newOptions with SceneManager difficulties
		foreach(sGameDifficulty difficulty in SceneManager.Instance.Difficulties) {
			Dropdown.OptionData newData = new Dropdown.OptionData(difficulty.name);
			newOptions.Add(newData);
		}

		// Add these options to the drop down
		m_difficultyDropdown.AddOptions(newOptions);

		// initialise current difficulty to lowest
		SceneManager.Instance.CurrentDifficulty = SceneManager.Instance.Difficulties[0];
	}
	
	public void OnCharacterChanged(Toggle toggle) {

		// check if toggle is set to false
		if (!toggle.isOn) {

			// if no toggles are true
			if (!m_characterToggles.AnyTogglesOn()) {
				toggle.isOn = true;
			}
		}
	}

	public void OnDifficultyChanged(int difficultyIndex) {

		// set current difficulty
		SceneManager.Instance.CurrentDifficulty = SceneManager.Instance.Difficulties[difficultyIndex];
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
