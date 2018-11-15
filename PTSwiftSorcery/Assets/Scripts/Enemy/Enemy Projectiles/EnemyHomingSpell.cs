using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingSpell : EnemySpellProjectile
{
	//the target of this gameobject, should be player
	private GameObject m_target;

	[Tooltip("How fast the projectile can accelerate")]
	public float m_fMaxVelocity;

	[Tooltip("How strong the homing is")]
	public float m_fRotationSpeed;

	[Tooltip("How much of a burst of speed this projectile should get on spawning")]
	public float m_fSpeedBurst;

	[Tooltip("How long this shot will home for")]
	public float m_fHomeTimer;

	//internal timer
	private float m_fTimer;

	[HideInInspector]
	//how long this shot will exist for
	public float m_fLifespan;

	//whether or not this shot is currently homing
	private bool m_bIsHoming;

	// Use this for initialization
	void Start ()
	{
		
	}

	override protected void Awake()
	{
		//call base projectile awake
		base.Awake();
		//set homing to true and timer to 0
		m_bIsHoming = true;
		m_fTimer = 0.0f;

		//set target to player
		m_target = GameObject.FindWithTag("Player");

		//if no player tag, there are much worse problems than a lack of a target
		if (m_target == null)
		{
			Debug.LogError("No object with the player tag was found, did you forget to tag the player?");
		}

		//initial push of speed
		gameObject.GetComponent<Rigidbody>().AddForce(m_fMoveSpeed * m_fSpeedBurst * transform.forward);
	}

	// Update is called once per frame
	override protected void Update ()
	{
		//increment timer
		m_fTimer += Time.deltaTime;

		//if timer exceeds homing time, stop homing
		if (m_fTimer >= m_fHomeTimer)
			m_bIsHoming = false;

		//if homing exceeds lifespan, destroy self
		if (m_fLifespan != 0.0f && m_fTimer >= m_fLifespan)
			Destroy(gameObject);

		//if currently homing, smoothly rotate towards player
		if(m_bIsHoming)
		{
			Quaternion targetRotation = Quaternion.LookRotation(m_target.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_fRotationSpeed * Time.deltaTime);
		}

		//push bullet towards forward direction
		gameObject.GetComponent<Rigidbody>().AddForce(m_fMaxVelocity * transform.forward);
	}

	private void FixedUpdate()
	{
		//if bullet exceeds max movement speed, reset to max move speed
		if(gameObject.GetComponent<Rigidbody>().velocity.magnitude > m_fMoveSpeed)
		{
			gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity.normalized * m_fMoveSpeed;
		}
	}

	protected override void OnTriggerEnter(Collider other)
	{
		//call basic collision code
		base.OnTriggerEnter(other);
	}

	protected override void OnTriggerExit(Collider other)
	{
		//call basic exit code
		base.OnTriggerExit(other);
	}
}
