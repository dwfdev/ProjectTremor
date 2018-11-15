using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpellProjectile : MonoBehaviour
{
	[Tooltip("How much damage this shot does")]
	public int m_nDamage;

	[Tooltip("What type of spell this shot is")]
	public eSpellType m_spellType;

	[Tooltip("Lightning effect particle system")]
	public ParticleSystem m_lightningParticle;

	[Tooltip("Lightning perimiter particle system")]
	public ParticleSystem m_perimiterParticle;
}
