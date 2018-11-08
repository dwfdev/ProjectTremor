using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///		Script Manager:	Denver
///		Description:	Handles the banter / Talking smack of an enemy
///		Date Modified:	8/11/2018
/// </summary>

[RequireComponent(typeof(EnemyActor))]
public class TauntActor : MonoBehaviour {

	[Header("Timers")]

	[Tooltip("Minimum time between each piece of banter, in seconds.")]
	[SerializeField] private float m_fMinWaitTime;

	[Tooltip("Maximum time between each piece of banter, in seconds.")]
	[SerializeField] private float m_fMaxWaitTime;

	[Header("Audio Sources")]

	[Tooltip("Array of sounds bites of banter.")]
	[SerializeField] private AudioSource[] m_audioSources;

	private EnemyActor m_enemyActor;

	private bool m_bIsValid;

	private float m_fWaitTime;

	private int m_nIndex;

	// Use this for initialization
	void Start () {

		// get EnemyActor component
		m_enemyActor = GetComponent<EnemyActor>();

		// check audioSources
		m_bIsValid = AudioSourcesIsValid();

		// initialise m_fWaitTime
		m_fWaitTime = Random.Range(m_fMinWaitTime, m_fMaxWaitTime);

		// initialise m_nIndex
		m_nIndex = Random.Range(0, m_audioSources.Length - 1);
	}

	void FixedUpdate() {

		if(m_bIsValid && m_enemyActor.m_bIsActive) {
			// run timer
			m_fWaitTime -= Time.deltaTime;

			// if it is time to play a new sound bite and the current sound bite has stopped playing
			if (m_fWaitTime <= 0f && !m_audioSources[m_nIndex].isPlaying) {
				// generate new random index
				m_nIndex = Random.Range(0, m_audioSources.Length);

				// play sound bite
				m_audioSources[m_nIndex].Play();

				// generate random wait time
				m_fWaitTime = m_audioSources[m_nIndex].clip.length + Random.Range(m_fMinWaitTime, m_fMaxWaitTime);
				Debug.Log("Audio Source: " + m_nIndex + " is playing. Waiting for " + m_fWaitTime + " seconds");
			}
		}
	}

	bool AudioSourcesIsValid() {

		// check that timers are valid
		if(m_fMinWaitTime > m_fMaxWaitTime) {
			Debug.LogError("Min Wait Time should be smaller than Max Wait Time.", gameObject);
			return false;
		}

		// check audio sources
		foreach(AudioSource audio in m_audioSources) {
			if (audio == null || audio.clip == null) {
				Debug.LogError("Audio Sources is not valid.", gameObject);
				return false;
			}
		}

		return true;
	}
}
