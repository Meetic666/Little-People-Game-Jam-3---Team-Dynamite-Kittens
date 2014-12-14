using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BodyExplosion : MonoBehaviour 
{
	public float m_ExplosionForce = 2;
	public float m_ExplosionRadius = 1;
	public float m_UpwardsModifier = 2;
	public int m_MediumParticleCount = 3;
    public int m_TinyParticleCount = 3;
	public GameObject m_GoreParticle;
    public List<Sprite> m_MediumGoreSprites = new List<Sprite>();
    public List<Sprite> m_TinyGoreSprites = new List<Sprite>();

	
	public void Explode()
	{
		for(int i = 0; i < m_MediumParticleCount; i++)
		{
            GameObject particle = (GameObject)Instantiate(m_GoreParticle, transform.position, transform.rotation);
            particle.GetComponent<SpriteRenderer>().sprite = m_MediumGoreSprites[Random.Range(0, m_MediumGoreSprites.Count)];
            particle.AddComponent<BoxCollider2D>().isTrigger = true;

            float x = Random.Range(-1.0f,1.0f);
            float y = Random.Range(-1.0f,1.0f);
            Vector2 direction = new Vector2(x, y);
            particle.rigidbody2D.velocity = direction * m_ExplosionForce;

		}

        for (int i = 0; i < m_TinyParticleCount; i++)
        {
            GameObject particle = (GameObject)Instantiate(m_GoreParticle, transform.position, transform.rotation);
            particle.GetComponent<SpriteRenderer>().sprite = m_TinyGoreSprites[Random.Range(0, m_MediumGoreSprites.Count)];
            particle.AddComponent<BoxCollider2D>().isTrigger = true;

            float x = Random.Range(-1.0f, 1.0f);
            float y = Random.Range(-1.0f, 1.0f);
            Vector2 direction = new Vector2(x, y);
            particle.rigidbody2D.velocity = direction * m_ExplosionForce;

        }
	}
}
