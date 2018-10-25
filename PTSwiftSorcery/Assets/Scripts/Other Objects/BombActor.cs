using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager: Denver
///		Description:	Handles the functionality of the bomb.
///		Date Modified:	25/10/2018
///</summary>

public class BombActor : MonoBehaviour {

	[Tooltip("How much damage will be dealt to all active enemies.")]
	[SerializeField] private int m_nBombDamage;

	[Tooltip("How much score will be awarded for each bullet destroyed.")]
	[SerializeField] private int m_nBulletScore;

	private PlayerActor m_player;

	private GameObject[] m_bullets;

	private CameraActor m_camera;

	public void Boom() {

		// get player
		m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActor>();

		// shake camera
		m_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraActor>();
		m_camera.ShakeCamera();

		// get all bullets
		m_bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

		foreach (GameObject bullet in m_bullets) {
			ScoreManager.Instance.AddScore(m_nBulletScore);
			Destroy(bullet);
		}

		// get all active enemies in current section
		foreach (EnemyActor enemy in m_player.m_currentSection.m_enemiesList) {
			if (enemy.m_bIsActive && enemy.m_bIsAlive) {
				enemy.TakeDamage(m_nBombDamage);
			}
		}
	}
}
