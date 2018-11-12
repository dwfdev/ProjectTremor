using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHitEffect : MonoBehaviour {

	[Tooltip("Hit Effect Texture.")]
	[SerializeField] private Texture m_hitEffectTexture; 

	[Tooltip("How long the texture is drawn for.")]
	[SerializeField] private float m_fEffectDuration;

	private bool m_bIsShowing;

	public void Show() {

		m_bIsShowing = true;

		if (!IsInvoking("Hide")) {
			Invoke("Hide", m_fEffectDuration);
		}
	}

	void Hide() {

		m_bIsShowing = false;
	}

	void OnGUI() {

		if (m_bIsShowing) {
			// calculate desired position for texture
			Vector2 desiredPosition = Camera.main.WorldToScreenPoint(transform.position);

			// calculate desired Texture Rectangle
			Rect desiredRect = new Rect(desiredPosition.x - (m_hitEffectTexture.width / 2), Screen.height - desiredPosition.y - (m_hitEffectTexture.height / 2), m_hitEffectTexture.width, m_hitEffectTexture.height);

			// draw texture
			GUI.DrawTexture(desiredRect, m_hitEffectTexture);
		}
	}
}
