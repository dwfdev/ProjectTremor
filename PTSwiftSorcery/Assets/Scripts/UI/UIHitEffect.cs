using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///		Script Manager:	Denver
///		Description:	Handles hit marker effect
/// </summary>

public struct sMaterial
{
	public Renderer renderer;
	public Material[] defaultMaterials;
}

public class UIHitEffect : MonoBehaviour {

	[Tooltip("Hit Material.")]
	[SerializeField] private Material m_hitMaterial; 

	[Tooltip("How long the texture is drawn for.")]
	[SerializeField] private float m_fEffectDuration;

	private List<sMaterial> m_sMaterials = new List<sMaterial>();

	void Start() {

		// get all renderers in object
		Renderer[] renderers = GetComponentsInChildren<Renderer>();

		foreach (Renderer rend in renderers) {
			sMaterial newSmat = new sMaterial() {
				renderer = rend,
				defaultMaterials = rend.materials
			};

			m_sMaterials.Add(newSmat);
		}
	}

	public void Show() {

		if (!IsInvoking("Hide")) {
			// change material
			foreach (sMaterial smat in m_sMaterials) {
				smat.renderer.material = m_hitMaterial;
			}

			Invoke("Hide", m_fEffectDuration);
		}
	}

	void Hide() {

		// change it back
		foreach (sMaterial smat in m_sMaterials) {
			smat.renderer.materials = smat.defaultMaterials;
		}
	}
}

