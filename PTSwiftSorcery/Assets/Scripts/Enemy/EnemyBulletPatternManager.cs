using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Script Manager: Drake
///Description:
///This script manages each enemy's patterns, and controls when the enemy should shoot. 
///</summary>
public class EnemyBulletPatternManager : MonoBehaviour
{
	[Tooltip("How long between pattern firing in seconds. ")]
	[SerializeField] private float m_fTimer;

	[Tooltip("All spawn locations of bullets in this pattern")]
	[SerializeField] private GameObject[] m_children;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
