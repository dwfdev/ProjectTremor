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

	[Header("Spell Images.")]

	[Tooltip("Fire Spell Image.")]
	[SerializeField] private Image m_fireSpellImage;

	[Tooltip("Ice Spell Image.")]
	[SerializeField] private Image m_iceSpellImage;

	[Tooltip("Lightning Spell Image.")]
	[SerializeField] private Image m_lightningSpellImage;

	[Header("Spell Sprites.")]

	[Tooltip("Fire Spell Sprites. Unselected and selected.")]
	[SerializeField] private Sprite[] m_fireSpellSprites = new Sprite[2];

	[Tooltip("Ice Spell Sprites. Unselected and selected.")]
	[SerializeField] private Sprite[] m_iceSpellSprites = new Sprite[2];

	[Tooltip("Lightning Spell Sprites. Unselected and selected.")]
	[SerializeField] private Sprite[] m_lightningSpellSprites = new Sprite[2];

	private PlayerSpellManager m_spellManager;

	void Start() {

		// get instance of the player
		m_spellManager = FindObjectOfType<PlayerSpellManager>();
	}

	void FixedUpdate() {
		
		switch(m_spellManager.m_eSpellType) {
			case eSpellType.FIRE:
				m_fireSpellImage.sprite = m_fireSpellSprites[1];
				m_iceSpellImage.sprite = m_iceSpellSprites[0];
				m_lightningSpellImage.sprite = m_lightningSpellSprites[0];
				break;

			case eSpellType.ICE:
				m_fireSpellImage.sprite = m_fireSpellSprites[0];
				m_iceSpellImage.sprite = m_iceSpellSprites[1];
				m_lightningSpellImage.sprite = m_lightningSpellSprites[0];
				break;

			case eSpellType.LIGHTNING:
				m_fireSpellImage.sprite = m_fireSpellSprites[0];
				m_iceSpellImage.sprite = m_iceSpellSprites[0];
				m_lightningSpellImage.sprite = m_lightningSpellSprites[1];
				break;

			default:
				Debug.LogError("Could not find spell type.", gameObject);
				break;
		}
	}
}
