using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles Main Menu functionality
///		Date Modified:	18/10/2018

public class UIMainMenu : MonoBehaviour {

	[Tooltip("Options Menu parent game object.")]
	[SerializeField] private GameObject m_CharacterSelectMenu;

	[Tooltip("Character selection toggle.")]
	[SerializeField] private UnityEngine.UI.Toggle m_toggle;

	void Start() {

		// initialise menu screens
		gameObject.SetActive(true);
		m_CharacterSelectMenu.SetActive(false);

		// initialise toggle
		m_toggle.isOn = SceneManager.Instance.IsWitch;
	}

	public void OnStartPressed() {
		
		// activate character select screen
		m_CharacterSelectMenu.SetActive(true);

		// deactivate main menu
		gameObject.SetActive(false);
	}

	public void OnQuitPressed() {
		Application.Quit();
	}
}
