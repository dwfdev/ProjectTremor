using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelTimer : MonoBehaviour {

	[Tooltip("Text object that will display the timer.")]
	[SerializeField] private Text m_timerText;

	void FixedUpdate() {

		// calculate time elapsed by seconds since level load
		string timeElapsed = ((int)(Time.timeSinceLevelLoad / 60f)).ToString("00") + ':' + 
		((int)(Time.timeSinceLevelLoad % 60f)).ToString("00") + "." + 
		Mathf.Clamp(((Time.timeSinceLevelLoad % 60f) - ((int)(Time.timeSinceLevelLoad % 60f))) * 100, 0f, 99f).ToString("00");

		// set text to time elapsed
		m_timerText.text = timeElapsed;
	}
}
