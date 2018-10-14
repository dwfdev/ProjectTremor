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
		m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActor>();

		// initialise is pick up active
		m_bPickUpIsActive = false;

		m_bFire3Down = false;

		m_progressBar.gameObject.SetActive(false);

	}

	void FixedUpdate() {
		if (m_bPickUpIsActive) {
			m_fPickupTimeRemaining -= Time.deltaTime;
		}
	}
	
	void OnGUI () {

		// change bomb count text to match the number of bombs the player has
		m_BombCountText.text = m_player.m_nCurrentBombCount.ToString();

		#region Display Current Pickup
		// if player has a pickup
		if (m_player.m_bHasPickUp) {
			// change image to be corresponding sprite
			switch(m_player.m_currentPickUp.type) {
				case ePickUpType.INCREASE_FIRE_RATE:
					m_pickupImage.sprite = m_IFRSprite;
					break;

				case ePickUpType.IMMUNITY:
					m_pickupImage.sprite = m_immunitySprite;
					break;
				
				case ePickUpType.SLOW_DOWN_TIME:
					m_pickupImage.sprite = m_SDTSprite;
					break;

				case ePickUpType.HOMING_SPELLS:
					m_pickupImage.sprite = m_homingSpellsSprite;
					break;

				case ePickUpType.SCATTER_SPELLS:
					m_pickupImage.sprite = m_scatterSpellsSprite;
					break;

				default:
					Debug.LogError("Sprite could not be changed.", gameObject);
					break;
			}
		}
		else {
			m_pickupImage.sprite = m_emptySprite;
		}
		#endregion
		
		#region Pick up progress bar
		// if player has activated their pickup
		if (Input.GetAxis("Fire3") > 0 && m_player.m_bHasPickUp && !m_bFire3Down) {
			m_bPickUpIsActive = true;
			m_bFire3Down = true;
			m_fPickupTimeRemaining = m_player.m_currentPickUp.duration;
			m_progressBar.gameObject.SetActive(true);
		}

		if (Input.GetAxis("Fire3") == 0) {
			m_bFire3Down = false;
		}

		// if player's pickup is active
		if (m_bPickUpIsActive) {
			// scale progress bar
			m_progressBar.transform.localScale = new Vector3(1f, m_fPickupTimeRemaining / m_player.m_currentPickUp.duration, 1f);

			// check if time has run out
			if (m_fPickupTimeRemaining <= 0f) {
				m_bPickUpIsActive = false;
				m_progressBar.gameObject.SetActive(false);
			}
		}
		#endregion

	}
}
