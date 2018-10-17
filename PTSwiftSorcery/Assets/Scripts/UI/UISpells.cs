using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles displaying which spell the player has selected
///						graphically.
///		Date Modified:	17/10/2018
///</summary>

public class UISpells : MonoBehaviour {

	[Tooltip("Image of spells.")]
	[SerializeField] private Image m_image;

	[Tooltip("Border for selected spell.")]
	[SerializeField] private Image m_selectedBorder;

	private PlayerActor m_player;

	void Start() {

		// get instance of the player
		m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActor>();
	}

	void FixedUpdate() {
		
		// change position of border based on selected spell type
		switch(m_player.m_spellManager.m_eSpellType) {
			case eSpellType.FIRE:
				// set border position to top third
				m_selectedBorder.rectTransform.position = new Vector3(m_selectedBorder.rectTransform.position.x, m_image.rectTransform.position.y + m_image.rectTransform.rect.height / 3, 0);
				break;

			case eSpellType.ICE:
				// set border position to middle third
				m_selectedBorder.rectTransform.position = m_image.rectTransform.position;
				break;

			case eSpellType.LIGHTNING:
				// set border position to bottom third
				m_selectedBorder.rectTransform.position = new Vector3(m_selectedBorder.rectTransform.position.x, m_image.rectTransform.position.y - m_image.rectTransform.rect.height / 3, 0);
				break;

			default:
				Debug.LogError("Could not find player's selected spell type.", gameObject);
				break;
		}
	}
}
