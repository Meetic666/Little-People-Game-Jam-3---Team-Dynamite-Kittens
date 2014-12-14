using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitBox : MonoBehaviour 
{
	public int m_DamageAmount;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.GetComponent<BaseAI>())
		{
			collider.GetComponent<BaseAI>().Damage(m_DamageAmount);
		}
	}
}
