using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSpellManager))]
public class StaffEmissionChanger : MonoBehaviour {

	[Header("Shader Emission")]

	[Tooltip("Material that emission will be added to.")]
	[SerializeField] private Material m_material;

	[Tooltip("List of Emision Colours.")]
	[SerializeField] private Texture[] m_emissions;

	[Header("Particle System")]

	[Tooltip("Staff's Particle System.")]
	[SerializeField] private ParticleSystem m_particleSystemStaff;

	[Tooltip("Spell Circle Particle System.")]
	[SerializeField] private ParticleSystem m_particleSystemCircleMinorGlow;

	[Tooltip("Spell Circle Particle System.")]
	[SerializeField] private ParticleSystem m_particleSystemCircle;

	[Tooltip("List of Particle Colours.")]
	[SerializeField] private Color[] m_colours;

	private ParticleSystem.MainModule m_particleMainStaff;

	private ParticleSystem.MainModule m_particleMainCircleMinorGlow;

	private ParticleSystem.MainModule m_particleMainCircle;

	private PlayerSpellManager m_spellManager;

	private eSpellType m_previousSpellType;

	void Start() {

		// get reference to spell manager
		m_spellManager = GetComponent<PlayerSpellManager>();

		// initialise particle main for staff
		m_particleMainStaff = m_particleSystemStaff.main;

		// initialise particle main for spell circle
		m_particleMainCircleMinorGlow = m_particleSystemCircleMinorGlow.main;
		m_particleMainCircle = m_particleSystemCircle.main;

		// initialise emission colour and particle system colour
		switch (m_spellManager.m_eSpellType) {
			case eSpellType.FIRE:
				// initialise previous spell type
				m_previousSpellType = eSpellType.FIRE;

				// change emission map to fire emission
				m_material.SetTexture("_EmissionMap", m_emissions[0]);

				// change staff colour
				m_particleMainStaff.startColor = new ParticleSystem.MinMaxGradient(m_colours[1], m_colours[0]);

				// change magic circle colour
				m_particleMainCircleMinorGlow.startColor = m_colours[0];
				m_particleMainCircle.startColor = m_colours[0];

				// restart particle system staff
				m_particleSystemStaff.Stop();
				m_particleSystemStaff.Play();

				// reset particle system minor glow
				m_particleSystemCircleMinorGlow.Stop();
				m_particleSystemCircleMinorGlow.Play();

				// reset particle system circle
				m_particleSystemCircle.Stop();
				m_particleSystemCircle.Play();
				break;

			case eSpellType.ICE:
				// initialise previous spell type
				m_previousSpellType = eSpellType.ICE;

				// change emission map to ice emission
				m_material.SetTexture("_EmissionMap", m_emissions[1]);

				// change staff colour
				m_particleMainStaff.startColor = new ParticleSystem.MinMaxGradient(m_colours[3], m_colours[2]);

				// change magic circle colour
				m_particleMainCircleMinorGlow.startColor = m_colours[2];
				m_particleMainCircle.startColor = m_colours[2];

				// restart particle system staff
				m_particleSystemStaff.Stop();
				m_particleSystemStaff.Play();

				// reset particle system minor glow
				m_particleSystemCircleMinorGlow.Stop();
				m_particleSystemCircleMinorGlow.Play();

				// reset particle system circle
				m_particleSystemCircle.Stop();
				m_particleSystemCircle.Play();
				break;
			
			case eSpellType.LIGHTNING:
				// initialise previous spell type
				m_previousSpellType = eSpellType.LIGHTNING;

				// change emission map to lightning emission
				m_material.SetTexture("_EmissionMap", m_emissions[2]);

				// change staff colour
				m_particleMainStaff.startColor = new ParticleSystem.MinMaxGradient(m_colours[5], m_colours[4]);

				// change magic circle colour
				m_particleMainCircleMinorGlow.startColor = m_colours[4];
				m_particleMainCircle.startColor = m_colours[4];

				// restart particle system staff
				m_particleSystemStaff.Stop();
				m_particleSystemStaff.Play();

				// reset particle system minor glow
				m_particleSystemCircleMinorGlow.Stop();
				m_particleSystemCircleMinorGlow.Play();

				// reset particle system circle
				m_particleSystemCircle.Stop();
				m_particleSystemCircle.Play();
				break;
			
			default:
				Debug.LogError("Couldn't find spell type.", gameObject);
				break;
		}
	}

	void FixedUpdate() {

		// if player has switched spell types
		if (m_spellManager.m_eSpellType != m_previousSpellType) {

			// update previous spell type
			m_previousSpellType = m_spellManager.m_eSpellType;

			// change colour of emission based on current spell type
			switch (m_spellManager.m_eSpellType) {
				case eSpellType.FIRE:
					// change emission map to fire emission
					m_material.SetTexture("_EmissionMap", m_emissions[0]);

					// change start colour to fire colours
					m_particleMainStaff.startColor = new ParticleSystem.MinMaxGradient(m_colours[1], m_colours[0]);

					// change magic circle colour
					m_particleMainCircleMinorGlow.startColor = m_colours[0];

					m_particleMainCircle.startColor = m_colours[0];

					// restart particle system staff
					m_particleSystemStaff.Stop();
					m_particleSystemStaff.Play();

					// reset particle system minor glow
					m_particleSystemCircleMinorGlow.Stop();
					m_particleSystemCircleMinorGlow.Play();

					// reset particle system circle
					m_particleSystemCircle.Stop();
					m_particleSystemCircle.Play();
					break;

				case eSpellType.ICE:
					// change emission map to ice emission
					m_material.SetTexture("_EmissionMap", m_emissions[1]);

					// change start colour to ice colours
					m_particleMainStaff.startColor = new ParticleSystem.MinMaxGradient(m_colours[3], m_colours[2]);

					// change magic circle colour
					m_particleMainCircleMinorGlow.startColor = m_colours[2];
					
					m_particleMainCircle.startColor = m_colours[2];

					// restart particle system staff
					m_particleSystemStaff.Stop();
					m_particleSystemStaff.Play();

					// reset particle system minor glow
					m_particleSystemCircleMinorGlow.Stop();
					m_particleSystemCircleMinorGlow.Play();

					// reset particle system circle
					m_particleSystemCircle.Stop();
					m_particleSystemCircle.Play();
					break;
				
				case eSpellType.LIGHTNING:
					// change emission map to lightning emission
					m_material.SetTexture("_EmissionMap", m_emissions[2]);

					// change start colour to lightning colours
					m_particleMainStaff.startColor = new ParticleSystem.MinMaxGradient(m_colours[5], m_colours[4]);

					// change magic circle colour
					m_particleMainCircleMinorGlow.startColor = m_colours[4];

					m_particleMainCircle.startColor = m_colours[4];

					// restart particle system staff
					m_particleSystemStaff.Stop();
					m_particleSystemStaff.Play();

					// reset particle system minor glow
					m_particleSystemCircleMinorGlow.Stop();
					m_particleSystemCircleMinorGlow.Play();

					// reset particle system circle
					m_particleSystemCircle.Stop();
					m_particleSystemCircle.Play();
					break;
				
				default:
					Debug.LogError("Couldn't find spell type.", gameObject);
					break;
			}
		}
	}
}
