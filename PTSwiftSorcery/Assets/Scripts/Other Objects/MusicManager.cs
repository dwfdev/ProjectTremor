using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<sumarry>
///		Script Manager:	Denver
///		Description:	Handles the music of the level
///		Date Modified:	2/11/2018
///</summary>

public class MusicManager : MonoBehaviour {

	[Tooltip("Background music Audio Source.")]
	[SerializeField] private AudioSource m_backgroundMusic;

	[Tooltip("Boss music Audio Source.")]
	[SerializeField] private AudioSource m_bossMusic;

	[Tooltip("Victory music Audio Source.")]
	[SerializeField] private AudioSource m_victoryMusic;

	[Tooltip("Failure music Audio Source.")]
	[SerializeField] private AudioSource m_failedMusic;

	void Start() {

		// stop boss and victory music if playing
		if (m_bossMusic != null) {
			if (m_bossMusic.isPlaying) {
				m_bossMusic.Stop();
			}
		}
		else {
			Debug.LogWarning("Boss Music is null.", gameObject);
		}

		if (m_victoryMusic != null) {
			if (m_victoryMusic.isPlaying) {
				m_victoryMusic.Stop();
			}
		}
		else {
			Debug.LogWarning("Victory Music is null.", gameObject);
		}

		if (m_failedMusic != null) {
			if (m_failedMusic.isPlaying) {
				m_failedMusic.Stop();
			}
		}
		else {
			Debug.LogWarning("Failed Music is null.", gameObject);
		}

		// check that background music is looping
		if (m_backgroundMusic != null) {
			if (!m_backgroundMusic.playOnAwake) {
				Debug.LogError("Background music should play on awake.", gameObject);
			}

			if (!m_backgroundMusic.loop) {
				Debug.LogError("Background music should be looping.", gameObject);
			}
		}
		else {
			Debug.LogWarning("Background music is null.", gameObject);
		}
	} 

	public void StartBossMusic () {
		
		// check that boss music exists
		if (m_bossMusic != null) {
			// play boss music
			m_bossMusic.Play();
			
			// stop background music
			if (m_backgroundMusic.isPlaying) {
				m_backgroundMusic.Stop();
			}

			// stop victory music
			if (m_victoryMusic.isPlaying) {
				m_victoryMusic.Stop();
			}

			// stop failed music
			if (m_failedMusic.isPlaying) {
				m_failedMusic.Stop();
			}
		}
	}

	public void StartVictoryMusic() {

		// check that victory music exists
		if (m_victoryMusic != null) {
			// play victory music
			m_victoryMusic.Play();

			// stop background music
			if (m_backgroundMusic.isPlaying) {
				m_backgroundMusic.Stop();
			}

			// stop boss music
			if (m_bossMusic.isPlaying) {
				m_bossMusic.Stop();
			}

			// stop failed music
			if (m_failedMusic.isPlaying) {
				m_failedMusic.Stop();
			}
		}
	}

	public void StartFailedMusic() {

		// check that failed music exists
		if (m_failedMusic != null) {
			// play failed music
			m_failedMusic.Play();

			// stop background music
			if (m_backgroundMusic.isPlaying) {
				m_backgroundMusic.Stop();
			}

			// stop boss music
			if (m_bossMusic.isPlaying) {
				m_bossMusic.Stop();
			}

			// stop victory music
			if (m_victoryMusic.isPlaying) {
				m_victoryMusic.Stop();
			}
		}
	}

	public bool IsPlaying() {

		// check background music
		if (m_backgroundMusic != null) {
			if (m_backgroundMusic.isPlaying) {
				return true;
			}
		}

		// check boss music
		if (m_bossMusic != null) {
			if (m_bossMusic.isPlaying) {
				return true;
			}
		}

		// check failed music
		if (m_failedMusic != null) {
			if (m_failedMusic.isPlaying) {
				return true;
			}
		}

		// check victory music
		if (m_victoryMusic != null) {
			if (m_victoryMusic.isPlaying) {
				return true;
			}
		}

		return false;
	}
}
