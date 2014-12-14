using UnityEngine;
using System.Collections;

public class BootPickUp : MonoBehaviour 
{
	public int m_PickUpDamageUpgrade;

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
		PlayerAttack player = collider.GetComponent<PlayerAttack>();

		if(player && player.m_DamageAmount < m_PickUpDamageUpgrade)
		{
			player.m_DamageAmount = m_PickUpDamageUpgrade;

			Destroy (gameObject);
		}
	}
}
