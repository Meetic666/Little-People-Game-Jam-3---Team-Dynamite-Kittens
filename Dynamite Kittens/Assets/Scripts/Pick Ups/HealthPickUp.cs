using UnityEngine;
using System.Collections;

public class HealthPickUp : MonoBehaviour
{
	public int m_HealthAmount;
	
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
