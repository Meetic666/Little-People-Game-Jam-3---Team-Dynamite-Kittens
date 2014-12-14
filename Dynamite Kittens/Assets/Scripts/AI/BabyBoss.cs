using UnityEngine;
using System.Collections;

public class BabyBoss : MonoBehaviour 
{
	bool m_LeftArmUp = true;
	bool m_BobbingUp = true;
	bool m_BossEngaged = false;

	public int m_Health;
	
	public float m_SpawnIntervalTime;
	float m_SpawnIntervalTimer;
	
	//Enemies to spawn
	public GameObject[] m_Enemies = new GameObject[3];
	
	public GameObject m_SpawnPoint;
	public int m_NumberOfEnemiesPerWave;

	// Use this for initialization
	void Start () 
	{
		m_SpawnIntervalTimer = m_SpawnIntervalTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
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

	public void Damage()
	{
		m_Health--;
		//TODO: Update animator
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.GetComponent<PlayerMovement>() != null)
		{
			m_BossEngaged = true;
			gameObject.GetComponent<BoxCollider>().isTrigger = false;
		}
	}
}
