using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Script Manager: Drake
///Description:
///
///</summary>
public class ScoreManager : MonoBehaviour 
{

	#region Make a Singleton Class
	private static ScoreManager instance = null;
	public static ScoreManager Instance {
		get {
			// if ScoreManager doesn't already exist
			if (instance == null) {
				// check that a GameObject doesn't already have a ScoreManager component
				instance = GameObject.FindObjectOfType<ScoreManager>();

				// if there isn't such a GameObject
				if (instance == null) {
					// create a GameObject for itself that won't be able to be replaced
					GameObject go = new GameObject();
					go.name = "ScoreManager";
					instance = go.AddComponent<ScoreManager>();
				}
			}
			return instance;
		}
	}

	private void Start()
	{
		m_fMultiplier = 1.0f;
	}

	void Awake() {
		if (instance == null) {
			// set instance
			instance = this;

			// let ScoreManager transcend scenes
			DontDestroyOnLoad(instance);
		}
		else {
			Destroy(gameObject);
		}
	}

	private ScoreManager() {}
	#endregion

	//the current score
	[HideInInspector]
	public long m_lScore;

	//the current multiplier
	[HideInInspector]
	public float m_fMultiplier;
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void AddScore(long scoreValue)
	{
		m_lScore += (long)Mathf.Round(scoreValue * m_fMultiplier);
	}

	public void AddMultiplier(float multiValue)
	{
		m_fMultiplier += multiValue;
	}
}
