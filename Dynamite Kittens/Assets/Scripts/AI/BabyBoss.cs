﻿using UnityEngine;
using System.Collections;

public class BabyBoss : MonoBehaviour 
{
	bool m_BossEngaged = false;

	Animator m_Animator;

	public int m_Health;
	
	public float m_SpawnIntervalTime;
	float m_SpawnIntervalTimer;

	public float m_DelayTime;
	float m_DelayTimer;
	
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
		m_DelayTimer = m_DelayTime;
		m_Animator = transform.GetComponentInChildren<Animator> ();
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
					if(m_DelayTimer <= 0)
					{
						int rand = Random.Range(0, 3);
						GameObject enemy = (GameObject)Instantiate(m_Enemies[rand], m_SpawnPoint.transform.position, Quaternion.identity);
						
						if(i == 2)
						{
							enemy.transform.Rotate(enemy.transform.up, 180);
						}
						
						m_DelayTimer = m_DelayTime;
					}
					else
					{
						m_DelayTimer -= Time.deltaTime;
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
		m_Health--;

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

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.GetComponent<PlayerMovement>() != null)
		{
			m_BossEngaged = true;
			gameObject.GetComponent<BoxCollider>().isTrigger = false;
		}
	}
}
