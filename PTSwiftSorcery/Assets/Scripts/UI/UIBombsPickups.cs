using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles displaying both how many bombs the player has
///						and what pick up they have.
///		Date Modified:	11/10/2018
///</summary>

public class UIBombsPickups : MonoBehaviour {

	[Header("UI Elements.")]
	[Tooltip("The text box that you want the bomb count to be displayed by.")]
	[SerializeField] private Text m_BombCountText;

	[Tooltip("The Image box that you want the player's current pick up to be displayed by.")]
	[SerializeField] private Image m_currentPickupImage;

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

	// Use this for initialization
	void Start () {
		
		m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActor>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// change bomb count text to match the number of bombs the player has
		m_BombCountText.text = m_player.m_nCurrentBombCount.ToString();

		#region Display Current Pickup
		if (m_player.m_bHasPickUp) {

			switch(m_player.m_currentPickUp.type) {
				case ePickUpType.INCREASE_FIRE_RATE:
					m_currentPickupImage.sprite = m_IFRSprite;
					break;

				case ePickUpType.IMMUNITY:
					m_currentPickupImage.sprite = m_immunitySprite;
					break;
				
				case ePickUpType.SLOW_DOWN_TIME:
					m_currentPickupImage.sprite = m_SDTSprite;
					break;

				case ePickUpType.HOMING_SPELLS:
					m_currentPickupImage.sprite = m_homingSpellsSprite;
					break;

				case ePickUpType.SCATTER_SPELLS:
					m_currentPickupImage.sprite = m_scatterSpellsSprite;
					break;

				default:
					Debug.LogError("Sprite could not be changed.", gameObject);
					break;
			}
		}
		else {
			m_currentPickupImage.sprite = m_emptySprite;
		}
		#endregion
		
	}
}
