using UnityEngine;
using System.Collections;

public class HealthPickUp : MonoBehaviour
{
	public int m_HealthAmount;

	public float m_WaveAmplitude;
	public float m_WaveSpeed;

	float m_WaveCenterY;

	float m_Time;

	void Start()
	{
		m_WaveCenterY = transform.position.y;
	}

	void Update()
	{
		m_Time += Time.deltaTime * m_WaveSpeed;

		Vector3 newPosition = transform.position;
		newPosition.y = m_WaveCenterY + m_WaveAmplitude * Mathf.Cos(m_Time);
		transform.position = newPosition;
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Health player = collider.GetComponent<Health>();
		
		if(player && player.CurrentHealth < player.m_MaxHealth)
		{
			player.CurrentHealth += m_HealthAmount;
			
			Destroy (gameObject);
		}
	}
}
