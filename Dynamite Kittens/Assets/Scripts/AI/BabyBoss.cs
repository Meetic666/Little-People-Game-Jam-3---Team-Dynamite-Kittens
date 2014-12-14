using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BabyBoss : MonoBehaviour 
{
	public float m_RestartDelay;

	bool m_BossEngaged = false;
	bool m_BossAlive = true;

	GameObject m_Player;

	public List<Sprite> m_BigBlobs = new List<Sprite>();
	public GameObject m_CorpsePiece;

	public GameObject m_BossEngagement;
	BoxCollider2D m_BossEyes;

	public int m_Health;
	int m_CurrentHealth;
	
	public float m_SpawnIntervalTime;
	float m_SpawnIntervalTimer;
	
	//Enemies to spawn
	public GameObject[] m_Enemies = new GameObject[3];
	
	public GameObject m_SpawnPoint;
	public int m_NumberOfEnemiesPerWave;

	public Animator m_BabyAnimator;
	public Animator m_EyesAnimator;

	int m_NumberOfHits = 0;

	// Use this for initialization
	void Start () 
	{
		m_CurrentHealth = m_Health;

		m_BossEyes = gameObject.AddComponent<BoxCollider2D> ();

		m_BossEyes.center = new Vector2 (0.18f, 1.51f);
		m_BossEyes.size = new Vector2 (2.05f, 2);

		m_SpawnIntervalTimer = m_SpawnIntervalTime;

		m_EyesAnimator.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_BossEngaged && m_BossAlive)
		{
			m_BabyAnimator.SetBool("Attack", m_SpawnIntervalTimer <= 0.0f);

			if(m_SpawnIntervalTimer <= 0)
			{
				for(int i = 0; i < m_NumberOfEnemiesPerWave; i++)
				{
					int rand = Random.Range(0, 3);
					GameObject enemy = (GameObject)Instantiate(m_Enemies[rand], m_SpawnPoint.transform.position, Quaternion.identity);
						
					if(i == 2 || i == 1)
					{
						enemy.transform.Rotate(enemy.transform.up, 180);
					}
				}
				
				m_SpawnIntervalTimer = m_SpawnIntervalTime;
			}
			else
			{
				m_SpawnIntervalTimer -= Time.deltaTime;
			}
		}

		if(!m_BossAlive)
		{
			m_RestartDelay -= Time.deltaTime;
			if(m_RestartDelay <= 0)
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	public void Damage()
	{
		m_CurrentHealth--;

		float rand = Random.Range(-2, 4);
		GameObject particle = (GameObject)Instantiate(m_CorpsePiece, transform.position + (transform.right * rand), transform.rotation);
		particle.GetComponent<SpriteRenderer>().sprite = m_BigBlobs[Random.Range(0, m_BigBlobs.Count)];
		particle.AddComponent<BoxCollider2D>().isTrigger = true;

		if(m_CurrentHealth <= 0)
		{
			m_BossAlive = false;
			gameObject.GetComponent<BodyExplosion>().Explode();
			Die ();
		}

		m_NumberOfHits++;

		if(m_NumberOfHits == 1)
		{
			m_EyesAnimator.renderer.enabled = true;
		}
		else
		{
			m_EyesAnimator.SetInteger("Hit", m_NumberOfHits - 1);
		}
	}

	void Die()
	{
		for(int i = 0; i < 25; i++)
		{
			float rand = Random.Range(-2, 4);
			GameObject particle = (GameObject)Instantiate(m_CorpsePiece, transform.position + (transform.right * rand), transform.rotation);
			particle.GetComponent<SpriteRenderer>().sprite = m_BigBlobs[Random.Range(0, m_BigBlobs.Count)];
			particle.AddComponent<BoxCollider2D>().isTrigger = true;
		}

		m_BossAlive = false;

		for(int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	public void Respawn()
	{
		m_CurrentHealth = m_Health;
		m_BossEngaged = false;
		m_BossEngagement.GetComponent<BoxCollider2D> ().isTrigger = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.GetComponent<PlayerMovement>() != null)
		{
			m_BossEngaged = true;
			m_BossEngagement.GetComponent<BoxCollider2D>().isTrigger = false;
			m_BossEngagement.tag = "Edge";
			m_Player = other.gameObject;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if(m_BossAlive)
		{
			if(other.gameObject.tag == "Attack")
			{
				Damage();
			}
		}
	}
}
