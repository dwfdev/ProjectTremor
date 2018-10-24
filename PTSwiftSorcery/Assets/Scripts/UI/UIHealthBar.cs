using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour {

	[Tooltip("Health Bar prefab.")]
	[SerializeField] private GameObject m_healthBarPrefab;

	[Tooltip("Y axis offset of health bar.")]
	[SerializeField] private float m_fOffset;

	private Image m_healthBar;
	private Image m_healthBarFilled;

	// Use this for initialization
	void Start () {
		
		// instantiate health bar
		m_healthBar = Instantiate(m_healthBarPrefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
		m_healthBarFilled = new List<Image>(m_healthBar.GetComponentsInChildren<Image>()).Find(img => img != m_healthBar);
	}
	
	void FixedUpdate () {
		
		// move the health bar to be slightly above gameObject on canvas
		m_healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * m_fOffset);

		// fill bar according to enemies health
		m_healthBarFilled.fillAmount = (float)GetComponent<EnemyActor>().m_nCurrentHealth / (float)GetComponent<EnemyActor>().m_nHealth;
	}

	public void DestroyHealthBar() {

		// destroy background
		Destroy(m_healthBar);
			
		// destroy fill bar	
		Destroy(m_healthBarFilled);
	}
}
