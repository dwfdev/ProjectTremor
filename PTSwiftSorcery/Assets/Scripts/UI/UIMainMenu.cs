using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles Main Menu functionality
///		Date Modified:	18/10/2018

public class UIMainMenu : MonoBehaviour {

	[Tooltip("Main Menu parent game object.")]
	[SerializeField] private GameObject m_mainMenu;

	[Tooltip("Options Menu parent game object.")]
	[SerializeField] private GameObject m_optionsMenu;

	[Tooltip("Character selection toggle.")]
	[SerializeField] private UnityEngine.UI.Toggle m_toggle;

	void Start() {

		// initialise menu screens
		m_mainMenu.SetActive(true);
		m_optionsMenu.SetActive(false);

		// initialise toggle
		m_toggle.isOn = SceneManager.Instance.IsWitch;
	}

	public void OnStartPressed() {
		SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneWithName("Level1"), UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	public void OnOptionsPressed() {
		m_mainMenu.SetActive(false);
		m_optionsMenu.SetActive(true);
	}

	public void OnBackPressed() {
		m_optionsMenu.SetActive(false);
		m_mainMenu.SetActive(true);
	}

	public void OnSelectCharacterToggleChange(UnityEngine.UI.Toggle toggle) {

		// change character
		SceneManager.Instance.IsWitch = toggle.isOn;

		Debug.Log(SceneManager.Instance.IsWitch);
	}

	public void OnQuitPressed() {
		Application.Quit();
	}
}
