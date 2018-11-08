using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIBossHealthBar : MonoBehaviour {

	[Tooltip("Boss Game Object")]
	[SerializeField] private EnemyActor m_enemyActor;

	[Tooltip("Rate at which fade in happens")]
	[SerializeField] [Range(0.0001f, 1f)] private float m_fFadeIn = 0.01f; 

	private Image m_healthBar;
	private Image m_healthBarFilled;

	// Use this for initialization
	void Start () {

		m_healthBar = gameObject.GetComponent<Image>();
		m_healthBarFilled = new List<Image>(GetComponentsInChildren<Image>()).Find(img => img != m_healthBar);

		// set alpha
		m_healthBar.canvasRenderer.SetAlpha(0f);
		m_healthBarFilled.canvasRenderer.SetAlpha(0f);

		// move to top of hierarchy
		m_healthBar.transform.SetSiblingIndex(0);

		Debug.Log(m_enemyActor.name);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		// fill bar according to boss' health
		m_healthBarFilled.fillAmount = (float)m_enemyActor.m_nCurrentHealth / (float)m_enemyActor.m_nHealth;

		if (m_enemyActor.m_bIsActive && m_healthBar.canvasRenderer.GetAlpha() < 255) {
			// increase alpha
			m_healthBar.canvasRenderer.SetAlpha(m_healthBar.canvasRenderer.GetAlpha() + m_fFadeIn);
			m_healthBarFilled.canvasRenderer.SetAlpha(m_healthBarFilled.canvasRenderer.GetAlpha() + m_fFadeIn);
		}
		
		if (!m_enemyActor.m_bIsAlive) {
			Destroy(gameObject);
		}
	}
}
