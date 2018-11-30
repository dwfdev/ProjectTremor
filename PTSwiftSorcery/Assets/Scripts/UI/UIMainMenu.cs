using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles Main Menu functionality
///		Date Modified:	18/10/2018

public class UIMainMenu : MonoBehaviour {

	[Tooltip("Options menu parent game object.")]
	[SerializeField] private GameObject m_optionsMenu;

	[Tooltip("Chracter Select Menu parent game object.")]
	[SerializeField] private GameObject m_characterSelectMenu;

	[Tooltip("Character selection toggle.")]
	[SerializeField] private UnityEngine.UI.Toggle m_toggle;

	void Start() {

		// initialise menu screens
		gameObject.SetActive(true);
		m_characterSelectMenu.SetActive(false);

		// initialise toggle
		m_toggle.isOn = SceneManager.Instance.IsWitch;

		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FastMobileBloom>().enabled = SceneManager.Instance.BloomOn;
	}

	public void OnStartPressed() {
		
		// activate character select screen
		m_characterSelectMenu.SetActive(true);

		// deactivate main menu
		gameObject.SetActive(false);
	}

	public void OnOptionsPressed() {

		// activate options menu
		m_optionsMenu.SetActive(true);

		// deactivate main menu
		gameObject.SetActive(false);
	}

    public void OnCreditsPressed()
    {
        SceneManager.Instance.LoadScene(SceneManager.Instance.GetGameSceneByType(eSceneType.CREDITS), UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

	public void OnQuitPressed() {
		Application.Quit();
	}
}
