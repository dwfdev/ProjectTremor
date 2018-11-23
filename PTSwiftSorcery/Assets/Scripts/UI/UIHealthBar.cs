using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///		Script Manager:	Denver
///		Description:	Handles instantiating a health bar for enemy,
///						moving that health bar with enemy and moving
///						it up in the hierarchy
///		Date Modified:	25/10/2018
///</summary>


public class UIHealthBar : MonoBehaviour {

	[Tooltip("Health Bar prefab.")]
	[SerializeField] private GameObject m_healthBarPrefab;

	[Tooltip("Y axis offset of health bar.")]
	[SerializeField] private float m_fOffset;

	private EnemyActor m_enemyActor;

	private Image m_healthBar;
	private Image m_healthBarFilled;

	private bool m_bIsActive;

	void Awake() {
		this.enabled = false;
	}

	void Start() {

		// get EnemyActor
		m_enemyActor = GetComponent<EnemyActor>();
	}

	// Use this for initialization
	void StartHealthBar () {
		
		// instantiate health bar
		m_healthBar = Instantiate(m_healthBarPrefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
		m_healthBarFilled = new List<Image>(m_healthBar.GetComponentsInChildren<Image>()).Find(img => img != m_healthBar);

		// make sure health bar is higher than other ui elements in hierarchy
		m_healthBar.transform.SetSiblingIndex(0);

		m_bIsActive = true;
	}
	
	void FixedUpdate () {

		if (m_enemyActor.m_bIsShooting && !m_bIsActive) {
			StartHealthBar();
		}
		
		if (m_bIsActive) {
			// move the health bar to be slightly above gameObject on canvas
			m_healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.forward * m_fOffset);

			// fill bar according to enemies health
			m_healthBarFilled.fillAmount = (float)m_enemyActor.m_nCurrentHealth / (float)m_enemyActor.m_nHealth;
		}
	}

	public void DestroyHealthBar() {

		// destroy background
		Destroy(m_healthBar.gameObject);
			
		// destroy fill bar	
		Destroy(m_healthBarFilled.gameObject);
	}
}
