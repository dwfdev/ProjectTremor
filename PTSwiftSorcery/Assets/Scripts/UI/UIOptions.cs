using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles functionality for options menu
///		Date Modified:	22//11/2018
///</summary>

public class UIOptions : MonoBehaviour {

	[Tooltip("Reference to Main Menu")]
	[SerializeField] private GameObject m_mainMenu;

	[Header("Options")]

	[Tooltip("Mouse Sensitivity slider")]
	[SerializeField] private Slider m_mouseSensSlider;

	[Tooltip("Game Audio Mixer")]
	[SerializeField] private AudioMixer m_audioMixer;

	void Start() {

		// initialise mouse sensitivity slider's value
		m_mouseSensSlider.value = SceneManager.Instance.MouseSensitivity;
	}

	public void OnMouseSensitivitySliderChanged(float mouseSensitivity) {

		// change mouse sensitivity
		SceneManager.Instance.MouseSensitivity = mouseSensitivity;
	}

	public void OnVolumeChanged(float volume) {

		m_audioMixer.SetFloat("MasterVolume", Mathf.Log(volume) * 20f);
	}
	
	public void OnBackButtonClicked() {

		// activate main menu
		m_mainMenu.SetActive(true);

		// deactivate options menu
		gameObject.SetActive(false);
	}
}
