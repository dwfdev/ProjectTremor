using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager: Denver
///		Description:	Handles the behaviour of the pick-up
///		Date Modified:	1/11/2018
///</summary>

enum ePickUpType {
	SPELL_UPGRADE,
	SHIELD,
	BOMB
}

public class PickUpActor : MonoBehaviour {

	[Tooltip("What type of pick up this is.")]
	[SerializeField] private ePickUpType m_type;

	void OnTriggerEnter(Collider other) {

		// check that other is the player
		if (other.tag == "Player") {
			switch(m_type) {
				case ePickUpType.SPELL_UPGRADE:
					++other.gameObject.GetComponent<PlayerSpellManager>().m_nSpellLevel;
					break;
				case ePickUpType.SHIELD:
					other.gameObject.GetComponent<PlayerActor>().m_lifeState = eLifeState.SHIELDED;
					break;

				case ePickUpType.BOMB:
					other.gameObject.GetComponent<PlayerActor>().AddToPlayerBombCount(1);
					break;

				default:
					Debug.LogError("Could not find pick up type", gameObject);
					break;
			}

			Destroy(gameObject);
		}
	}
}