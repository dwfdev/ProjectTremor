using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///		Script Manager:	Denver
///		Description:	Displays the player's score on the canvas
///		Date Modified:	25/10/2018
///</summary>

public class UIScore : MonoBehaviour {

	[Tooltip("Text box that shows score.")]
	[SerializeField] private Text m_scoreTextBox;

	[Tooltip("Text box that shows multiplier.")]
	[SerializeField] private Text m_multiplierTextBox;

	void FixedUpdate() {

		// change score text box text
		m_scoreTextBox.text = ScoreManager.Instance.m_lScore.ToString();

		// change multiplier text box text
		m_multiplierTextBox.text = "x" + ScoreManager.Instance.m_fMultiplier.ToString();
	}
}
