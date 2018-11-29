using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHitEffect : MonoBehaviour {

	[Tooltip("default Material")]
	[SerializeField] private Material m_defaultMaterial;

	[Tooltip("Hit Material.")]
	[SerializeField] private Material m_hitMaterial; 

	[Tooltip("How long the texture is drawn for.")]
	[SerializeField] private float m_fEffectDuration;

	private Renderer[] m_renderers;

	void Start() {
		
		m_renderers = GetComponentsInChildren<Renderer>();
	}

	public void Show() {

		if (!IsInvoking("Hide")) {
			// change material
			foreach (Renderer rend in m_renderers) {
				rend.material = m_hitMaterial;
			}

			Invoke("Hide", m_fEffectDuration);
		}
	}

	void Hide() {

		// change it back
		foreach (Renderer rend in m_renderers) {
			rend.material = m_defaultMaterial;
		}
	}
}

