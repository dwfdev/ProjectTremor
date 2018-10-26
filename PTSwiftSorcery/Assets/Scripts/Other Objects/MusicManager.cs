using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if (m_bossMusic.isPlaying) {
			m_bossMusic.Stop();
		}

		if (m_victoryMusic.isPlaying) {
			m_victoryMusic.Stop();
		}

		if (m_failedMusic.isPlaying) {
			m_failedMusic.Stop();
		}

		// check that background music is looping
		if (!m_backgroundMusic.playOnAwake) {
			Debug.LogError("Background music should play on awake.", gameObject);
		}

		if (!m_backgroundMusic.loop) {
			Debug.LogError("Background music should be looping.", gameObject);
		}
	}

	public void StartBossMusic () {
		
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

		// play boss music
		m_bossMusic.Play();
	}

	public void StartVictoryMusic() {

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

		// play victory music
		m_victoryMusic.Play();
	}

	public void StartFailedMusic() {

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

		// play failed music
		m_failedMusic.Play();
	}
}
