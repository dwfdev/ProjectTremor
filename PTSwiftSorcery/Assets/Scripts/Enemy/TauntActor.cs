using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///		Script Manager:	Denver
///		Description:	Handles the banter / Talking smack of an enemy
///		Date Modified:	8/11/2018
/// </summary>

public class TauntActor : MonoBehaviour {

	[Header("Timers")]

	[Tooltip("Minimum time between each piece of banter, in seconds.")]
	[SerializeField] private float m_fMinWaitTime;

	[Tooltip("Maximum time between each piece of banter, in seconds.")]
	[SerializeField] private float m_fMaxWaitTime;

	[Header("Audio Sources")]

	[Tooltip("Array of sounds bites of banter.")]
	[SerializeField] private AudioSource[] m_audioSources;

	// Use this for initialization
	void Start () {

		// generate random wait time
		float randTime = Random.Range(m_fMinWaitTime, m_fMaxWaitTime);

		// invoke playback fucntion
		Invoke("PlaySound", randTime);
	}

	void PlaySound() {

		// pick random sound bite
		int randIndex = Random.Range(0, m_audioSources.Length - 1);

		// play sound bite
		m_audioSources[randIndex].Play();

		// generate random wait time
		float randTime = Random.Range(m_fMinWaitTime, m_fMaxWaitTime);

		// invoke PlaySound function
		Invoke("PlaySound", m_audioSources[randIndex].clip.length + randTime);
	}
}
