using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles displaying player's lives graphically
///		Date Modified:	11/10/2018
///</summary>

public class UILives : MonoBehaviour {

	[Tooltip("The sprite for lives the player still has.")]
	[SerializeField] private Sprite m_lifeSprite;

	[Tooltip("The sprite for lives the player has lost.")]
	[SerializeField] private Sprite m_lostLifeSprite;

	[Tooltip("List of Images.")]
	[SerializeField] private List<Image> m_imageHolders;

	private PlayerActor m_player;

	void Start() {

		// get reference to the player
		m_player = FindObjectOfType<PlayerActor>();

		// check image holders set up properly
		if (m_imageHolders.Count != SceneManager.Instance.Difficulties[0].numOfLives) {
			Debug.LogError("Incorrect number of image holders.", gameObject);
		}
		else {
			for (int i = m_imageHolders.Count; i != 0; --i) {
				// if heart is redudent
				if (i > SceneManager.Instance.CurrentDifficulty.numOfLives) {
					// get referene to heart
					Image image = m_imageHolders[i - 1];

					// remove it from list
					m_imageHolders.Remove(image);

					// destroy heart
					Destroy(image);
				}
			}
		}
	}

	void OnGUI() {

		// change each image holders sprites such that it matches player's lives
		for (int i = 0; i < m_imageHolders.Count; ++i) {
			if (i <= m_player.m_nLives - 1) {
				m_imageHolders[i].sprite = m_lifeSprite;
			}
			else {
				m_imageHolders[i].sprite = m_lostLifeSprite;
			}
		}

	}
	
}
