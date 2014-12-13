using UnityEngine;
using System.Collections;

public class BodyExplosion : MonoBehaviour 
{
	public float m_ExplosionForce = 2;
	public float m_ExplosionRadius = 1;
	public float m_UpwardsModifier = 2;
	public int m_ParticleCount = 3;
	public GameObject m_GoreParticle;
	
	public void Explode()
	{
		for(int i = 0; i < m_ParticleCount; i++)
		{
			GameObject particle = (GameObject)Instantiate(m_GoreParticle, transform.position, transform.rotation);
			particle.rigidbody.AddExplosionForce(m_ExplosionForce, 
			                                     transform.position, 
			                                     m_ExplosionRadius, 
			                                     m_UpwardsModifier, 
			                                     ForceMode.Impulse);
		}
	}
}
