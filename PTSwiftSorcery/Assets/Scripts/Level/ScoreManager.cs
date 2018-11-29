
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	private void OnEnable()
	{
		//add level load code
		UnityEngine.SceneManagement.SceneManager.sceneLoaded += LevelLoaded;
	}

	private void OnDisable()
	{
		//remove level load code
		UnityEngine.SceneManagement.SceneManager.sceneLoaded -= LevelLoaded;
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

	//Prefab for score pickups
	[Header("Score pickup variables")]
	[Tooltip("Prefab for score pickups")]
	[SerializeField] private GameObject m_pickupPrefab;

	[Tooltip("How fast score pickups should move")]
	[SerializeField] private float m_fPickupMoveSpeed;

	[Header("How much each score pickup can be worth")]
	//list of score values
	[Tooltip("The lowest value for score pickups that is non-zero")]
	[SerializeField] private long m_lMinScorePickupValue = 5;
	[Tooltip("A low value for score pickups")]
	[SerializeField] private long m_lLowScorePickupValue = 10;
	[Tooltip("An average value for score pickups")]
	[SerializeField] private long m_lMidScorePickupValue = 25;
	[Tooltip("A high value for score pickups")]
	[SerializeField] private long m_lHighScorePickupValue = 100;
	[Tooltip("The max value for score pickups")]
	[SerializeField] private long m_lMaxScorePickupValue = 500;

	//playfield
	private GameObject m_playfield;

	//code to be called on level load
	void LevelLoaded(Scene scene, LoadSceneMode mode)
	{
		//reset multiplier
		m_fMultiplier = 1.0f;

		//get playfield
		m_playfield = GameObject.FindGameObjectWithTag("Playfield");
	}

	public void AddScore(long scoreValue)
	{
		//add score multiplied by multiplier
		m_lScore += (long)(scoreValue * m_fMultiplier);
	}

	public void AddMultiplier(float multiValue)
	{
		//increase multiplier
		m_fMultiplier += multiValue;
	}

	public void ResetScore()
	{
		m_fMultiplier = 1.0f;
		m_lScore = 0;
	}

	public void DropScorePickup(long scoreValue, Transform transform)
	{
		//get score to drop
		long scoreToDrop = scoreValue;
		
		//while loop for still having score to drop
		while(scoreToDrop > 0)
		{
			//check if score left is greater than or equal to max value of score pickup
			if(scoreToDrop >= m_lMaxScorePickupValue)
			{
				GameObject newPickup = Instantiate(m_pickupPrefab, transform.position, Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up));

				newPickup.GetComponent<ScorePickup>().m_lScoreValue = m_lMaxScorePickupValue;
				newPickup.GetComponent<ScorePickup>().m_fMoveSpeed = m_fPickupMoveSpeed;
				newPickup.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				newPickup.transform.parent = m_playfield.transform;
				newPickup.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Random.Range(20.0f, 100.0f));

				//decrease score left value
				scoreToDrop -= m_lMaxScorePickupValue;
			}
			//check if score left is greater than or equal to high value of score pickup
			if(scoreToDrop < m_lMaxScorePickupValue && scoreToDrop >= m_lHighScorePickupValue)
			{
				GameObject newPickup = Instantiate(m_pickupPrefab, transform.position, Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up));

				newPickup.GetComponent<ScorePickup>().m_lScoreValue = m_lHighScorePickupValue;
				newPickup.GetComponent<ScorePickup>().m_fMoveSpeed = m_fPickupMoveSpeed;
				newPickup.transform.localScale = new Vector3(0.83f, 0.83f, 0.83f);
				newPickup.transform.parent = m_playfield.transform;
				newPickup.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Random.Range(20.0f, 100.0f));

				//decrease score left value
				scoreToDrop -= m_lHighScorePickupValue;
			}
			//check if score left is greater than or equal to mid value of score pickup
			if(scoreToDrop < m_lHighScorePickupValue && scoreToDrop >= m_lMidScorePickupValue)
			{
				GameObject newPickup = Instantiate(m_pickupPrefab, transform.position, Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up));

				newPickup.GetComponent<ScorePickup>().m_lScoreValue = m_lMidScorePickupValue;
				newPickup.GetComponent<ScorePickup>().m_fMoveSpeed = m_fPickupMoveSpeed;
				newPickup.transform.localScale = new Vector3(0.66f, 0.66f, 0.66f);
				newPickup.transform.parent = m_playfield.transform;
				newPickup.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Random.Range(20.0f, 100.0f));

				//decrease score left value
				scoreToDrop -= m_lMidScorePickupValue;
			}
			//check if score left is greater than or equal to low value of score pickup
			if(scoreToDrop < m_lMidScorePickupValue && scoreToDrop >= m_lLowScorePickupValue)
			{
				GameObject newPickup = Instantiate(m_pickupPrefab, transform.position, Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up));

				newPickup.GetComponent<ScorePickup>().m_lScoreValue = m_lLowScorePickupValue;
				newPickup.GetComponent<ScorePickup>().m_fMoveSpeed = m_fPickupMoveSpeed;
				newPickup.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				newPickup.transform.parent = m_playfield.transform;
				newPickup.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Random.Range(20.0f, 100.0f));

				//decrease score left value
				scoreToDrop -= m_lLowScorePickupValue;
			}
			//check if score left is greater than or equal to min value of score pickup
			if(scoreToDrop < m_lLowScorePickupValue && scoreToDrop >= m_lMinScorePickupValue)
			{
				GameObject newPickup = Instantiate(m_pickupPrefab, transform.position, Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up));

				newPickup.GetComponent<ScorePickup>().m_lScoreValue = m_lMinScorePickupValue;
				newPickup.GetComponent<ScorePickup>().m_fMoveSpeed = m_fPickupMoveSpeed;
				newPickup.transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
				newPickup.transform.parent = m_playfield.transform;
				newPickup.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Random.Range(20.0f, 100.0f));

				//decrease score left value
				scoreToDrop -= m_lMinScorePickupValue;
			}
			//check if score left is greater than or equal to 1
			if(scoreToDrop < m_lMinScorePickupValue && scoreToDrop >= 1)
			{
				GameObject newPickup = Instantiate(m_pickupPrefab, transform.position, Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up));

				newPickup.GetComponent<ScorePickup>().m_lScoreValue = 1;
				newPickup.GetComponent<ScorePickup>().m_fMoveSpeed = m_fPickupMoveSpeed;
				newPickup.transform.localScale = new Vector3(0.16f, 0.16f, 0.16f);
				newPickup.transform.parent = m_playfield.transform;
				newPickup.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Random.Range(20.0f, 100.0f));

				//decrease score left value
				scoreToDrop -= 1;
			}
		}
	}
}
