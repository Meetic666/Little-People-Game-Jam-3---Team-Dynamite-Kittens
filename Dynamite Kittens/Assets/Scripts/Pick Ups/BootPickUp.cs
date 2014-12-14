using UnityEngine;
using System.Collections;

public class BootPickUp : MonoBehaviour 
{
	public int m_PickUpDamageUpgrade;

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
