using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles displaying both how many bombs the player has
///						and what pick up they have.
///		Date Modified:	14/10/2018
///</summary>

public class UIBombsPickups : MonoBehaviour {

	[Header("UI Elements.")]
	[Tooltip("The text box that you want the bomb count to be displayed by.")]
	[SerializeField] private Text m_BombCountText;

	[Tooltip("The Image box that you want the player's current pick up to be displayed by.")]
	[SerializeField] private Image m_pickupImage;

	[Tooltip("Progress bar for activated pickup.")]
	[SerializeField] private Image m_progressBar;

	[Header("Pickup Sprites.")]
	[Tooltip("Empty / No Pickup Sprite.")]
	[SerializeField] private Sprite m_emptySprite;

	[Tooltip("Increased Fire Rate Pickup Sprite.")]
	[SerializeField] private Sprite m_IFRSprite;

	[Tooltip("Immunity Pickup Sprite.")]
	[SerializeField] private Sprite m_immunitySprite;

	[Tooltip("Slow Down Time Pickup Sprite.")]
	[SerializeField] private Sprite m_SDTSprite;

	[Tooltip("Homing Spells Pickup Sprite.")]
	[SerializeField] private Sprite m_homingSpellsSprite;

	[Tooltip("Scatter Spells Pickup Sprite.")]
	[SerializeField] private Sprite m_scatterSpellsSprite;

	private PlayerActor m_player;

	private bool m_bPickUpIsActive;

	private bool m_bFire3Down;

	private float m_fPickupTimeRemaining;

	// Use this for initialization
	void Start () {
		
		// get the PlayerActor
		m_player = GameObject.FindObjectOfType<PlayerActor>();

		// initialise is pick up active
		m_bPickUpIsActive = false;

	}

	void FixedUpdate() {
		if (m_bPickUpIsActive) {
			m_fPickupTimeRemaining -= Time.deltaTime;
		}
	}
	
	void OnGUI () {

		// change bomb count text to match the number of bombs the player has
		m_BombCountText.text = m_player.m_nCurrentBombCount.ToString();

	}
}
