using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<sumarry>
///		Script Manager:	Denver
///		Description:	Handles the music of the level
///		Date Modified:	2/11/2018
///</summary>

public class MusicManager : MonoBehaviour {

	[Tooltip("Speed of transition between Audio clips")]
	[SerializeField] private float m_fTransitionSpeed = 0.01f;

	[Header("Audio Sources")]

	[Tooltip("Background music Audio Source.")]
	[SerializeField] private AudioSource m_backgroundMusic;

	[Tooltip("Boss music Audio Source.")]
	[SerializeField] private AudioSource m_bossMusic;

	[Tooltip("Victory music Audio Source.")]
	[SerializeField] private AudioSource m_victoryMusic;

	[Tooltip("Failure music Audio Source.")]
	[SerializeField] private AudioSource m_failedMusic;

	private AudioSource m_currentSource;

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

			m_currentSource = m_backgroundMusic;
		}
		else {
			Debug.LogWarning("Background music is null.", gameObject);
		}
	} 

	public void StartBossMusic () {
		
		// check that boss music exists
		if (m_bossMusic != null) {
			// transition to boss music
			StartCoroutine(Transition(m_bossMusic));
		}
	}

	public void StartVictoryMusic() {

		// check that victory music exists
		if (m_victoryMusic != null) {
			// transition to victory music
			StartCoroutine(Transition(m_victoryMusic));
		}
	}

	public void StartFailedMusic() {

		// check that failed music exists
		if (m_failedMusic != null) {
			// transition to failed music
			StartCoroutine(Transition(m_failedMusic));
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

	IEnumerator Transition(AudioSource targetSource) {

		// begin playing target source
		targetSource.volume = 0f;
		targetSource.Play();

		while(true) {
			// quiten current source
			m_currentSource.volume -= m_fTransitionSpeed;

			// louden target source
			targetSource.volume += m_fTransitionSpeed;

			// if transition is not complete
			if(m_currentSource.volume != 0f || targetSource.volume != 1f) {
				// continue during next frame
				yield return new WaitForEndOfFrame();
			}
			else {
				// stop current source
				m_currentSource.Stop();

				// set current source to targetSource
				m_currentSource = targetSource;

				// stop transitioning
				break;
			}
		}
	}
}
