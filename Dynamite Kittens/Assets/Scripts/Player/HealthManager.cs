using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour 
{
	SpriteRenderer[] m_Hearts;

	Health m_PlayerHealth;

	// Use this for initialization
	void Start () 
	{
		m_PlayerHealth = GameObject.Find("Player").GetComponent<Health>();

		m_Hearts = transform.GetComponentsInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		int index = 1;

		foreach(SpriteRenderer sprite in m_Hearts)
		{
			if(index <= m_PlayerHealth.CurrentHealth)
			{
				sprite.enabled = true;
			}
			else
			{
				sprite.enabled = false;
			}

			index++;
		}
	}
}
