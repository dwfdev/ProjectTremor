using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeamSpell : EnemySpellProjectile
{

	[Tooltip("How long until this beam should become active in seconds")]
	public float m_fBeamActiveTimer;

	[Tooltip("How long this should stay active in seconds, if it is a beam")]
	public float m_fBeamStayTimer;

	[Header("Beam Game Objects")]

	[Tooltip("Beam Base")]
	public GameObject m_beamBase;

	[Tooltip("Warning Beam")]
	public GameObject m_warningBeam;

	[Tooltip("Energy ball centre")]
	public ParticleSystem m_energyBallCentre;

	[Tooltip("Damage beam")]
	public ParticleSystem m_damageBeam;

	[Tooltip("Second circle")]
	public ParticleSystem m_secondCircle;

	//Current time in seconds
	private float m_fTimer;

	// Use this for initialization
	override protected void Awake()
	{
		//start out inactive
		m_bActive = false;

		transform.Rotate(Vector3.right, -90.0f);

	}
	
	public void ScaleParticle()
	{
		#region Scale Beam Particle Systems
		///<summary> Code by Denver Lacey ///</summary>

		// create list of particle systems
		List<ParticleSystem> beamChildren = new List<ParticleSystem>();

		// add beam base and children
		beamChildren.AddRange(m_beamBase.GetComponentsInChildren<ParticleSystem>());

		// find all of warning beam's children without including warning beam itself
		List<ParticleSystem> warningBeamChildren = new List<ParticleSystem>(m_warningBeam.GetComponentsInChildren<ParticleSystem>()).FindAll(part => part != m_warningBeam);

		// add warning beam children
		beamChildren.AddRange(warningBeamChildren);

		// modify start lifetime of base beam and warning beam
		foreach (ParticleSystem system in beamChildren) {
			// get main module
			ParticleSystem.MainModule main = system.main;

			// change start lifetime
			main.startLifetime = m_fBeamActiveTimer;
		}

		if (m_fBeamStayTimer != 0.0f) {
			// change energy ball entre
			ParticleSystem.MainModule energyBallCentreMain = m_energyBallCentre.main;
			energyBallCentreMain.startLifetime = m_fBeamStayTimer;

			// change damage beam
			ParticleSystem.MainModule damageBeamMain = m_damageBeam.main;
			damageBeamMain.startLifetime = m_fBeamStayTimer;

			// change second circle
			ParticleSystem.MainModule secondCircleMain = m_secondCircle.main;
			secondCircleMain.startDelay = m_fBeamActiveTimer - .2f;
			secondCircleMain.startLifetime = m_fBeamActiveTimer + .2f;
		}
		else {
			// deactivate energy ball, damage beam and second circle
			m_energyBallCentre.gameObject.SetActive(false);
			m_damageBeam.gameObject.SetActive(false);
			m_secondCircle.gameObject.SetActive(false);
		}
		#endregion
	}
	// Update is called once per frame
	override protected void Update()
	{
		//increment timer
		m_fTimer += Time.deltaTime;

		//if timer reaches active timer and isn't already active, start activating
		if (m_fTimer >= m_fBeamActiveTimer && !m_bActive)
		{
			//reset timer
			m_fTimer = 0.0f;

			//set active to true
			if (m_fBeamStayTimer != 0.0f)
			{
				m_bActive = true;

				//patched together fix that lets beams damage you if you were in the collider when it turns active
				//this really shouldn't work but it does
				gameObject.GetComponent<CapsuleCollider>().enabled = false;
				gameObject.GetComponent<CapsuleCollider>().enabled = true;
			}
			else
				Destroy(gameObject);
		}

		//if timer reaches end of stay timer and is already active, destroy self
		else if (m_fTimer >= m_fBeamStayTimer && m_bActive)
		{
			Destroy(gameObject);
		}
	}
}
