using UnityEngine;
using System.Collections;

public class BabyBoss : MonoBehaviour 
{
	bool m_BossEngaged = false;

	GameObject m_Player;

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
		if(m_BossEngaged)
		{
			m_BabyAnimator.SetBool("Attack", m_SpawnIntervalTimer <= 0.0f);

			if(m_SpawnIntervalTimer <= 0)
			{
				for(int i = 0; i < m_NumberOfEnemiesPerWave; i++)
				{
					int rand = Random.Range(0, 3);
					GameObject enemy = (GameObject)Instantiate(m_Enemies[rand], m_SpawnPoint.transform.position, Quaternion.identity);
						
					if(i == 2)
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
	}

	public void Damage()
	{
		m_CurrentHealth--;

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
		if(other.gameObject.tag == "Attack")
		{
			Damage();
		}
	}
}
