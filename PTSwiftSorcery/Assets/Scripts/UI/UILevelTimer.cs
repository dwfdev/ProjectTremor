using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelTimer : MonoBehaviour {

	[Tooltip("Text object that will display the timer.")]
	[SerializeField] private Text m_timerText;

	void FixedUpdate() {

		string text = ((int)(Time.timeSinceLevelLoad / 60f)).ToString("00") + ':' + 
		((int)(Time.timeSinceLevelLoad % 60f)).ToString("00") + "." + 
		(((Time.timeSinceLevelLoad % 60f) - ((int)(Time.timeSinceLevelLoad % 60f))) * 100).ToString("00");
		m_timerText.text = text;
	}
}
