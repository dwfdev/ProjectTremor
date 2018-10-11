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
	[SerializeField] private Image[] m_imageHolders;

	private PlayerActor m_player;

	void Start() {

		m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActor>();

	}

	void OnGUI() {

		// change each image holders sprites such that it matches player's lives
		for (int i = 0; i < m_imageHolders.Length; ++i) {
			if (i <= m_player.m_nLives - 1) {
				m_imageHolders[i].sprite = m_lifeSprite;
			}
			else {
				m_imageHolders[i].sprite = m_lostLifeSprite;
			}
		}

	}
	
}
