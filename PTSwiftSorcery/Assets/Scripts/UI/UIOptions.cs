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

	[Tooltip("Resolutions Dropdown")]
	[SerializeField] private Dropdown m_resolutionsDropdown;

	[Tooltip("Game Audio Mixer")]
	[SerializeField] private AudioMixer m_audioMixer;

	void Start() {

		// initialise mouse sensitivity slider's value
		m_mouseSensSlider.value = SceneManager.Instance.MouseSensitivity;

		// initialise resolutions dropdown
		Resolution[] resolutions = Screen.resolutions;
		List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

		foreach(Resolution resolution in resolutions) {
			Dropdown.OptionData newData = new Dropdown.OptionData(resolution.width.ToString() + "x" + resolution.height.ToString());
			options.Add(newData);
		}

		m_resolutionsDropdown.AddOptions(options);

		// initialise value to current resolution
		m_resolutionsDropdown.value = GetCurrentResolutionIndexFrom(m_resolutionsDropdown.options);
	}

	int GetCurrentResolutionIndexFrom(List<Dropdown.OptionData> list) {

		// re-format Screen.currentResolution.ToString()
		string currentResolution = Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString();

		// find matching resolution
		for (int i = 0; i < list.Count; ++i) {
			// if match is found
			if (list[i].text.Equals(currentResolution)) {
				// return the index of that resolution
				return i;
			}
		}

		// if no match is found, return dummy value
		return -1;
	}

	public void OnMouseSensitivitySliderChanged(float mouseSensitivity) {

		// change mouse sensitivity
		SceneManager.Instance.MouseSensitivity = mouseSensitivity;
	}

	public void OnVolumeChanged(float volume) {

		// change master volume
		m_audioMixer.SetFloat("MasterVolume", Mathf.Log(volume) * 20f);
	}

	public void OnResolutionChanged(int resolutionIndex) {

		Resolution newRes = Screen.resolutions[resolutionIndex];

		// change resolution
		Screen.SetResolution(newRes.width, newRes.height, Screen.fullScreen);
	}
	
	public void OnBackButtonClicked() {

		// activate main menu
		m_mainMenu.SetActive(true);

		// deactivate options menu
		gameObject.SetActive(false);
	}
}
