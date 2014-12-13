using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BaseAI))]
public class DynamiteKitten : BaseAI 
{
	bool m_FuseLit = false;
	float m_FuseTimer = 2.5f;
	float m_ExplosionRangeMultiplier = 2;
	float m_DismembermentForce = 1;

	List<GameObject> m_Children = new List<GameObject>();

	protected override void VirtualUpdate()
	{
		if(m_FuseLit)
		{
			m_FuseTimer -= Time.deltaTime;

			if(m_FuseTimer <= 0)
			{
				m_CurrentState = ActionState.e_Dead;
			}
		}
	}

	protected override void VirtualMove()
	{

	}

	protected override void VirtualAttack()
	{
		m_FuseLit = true;
	}

	protected override void VirtualDied()
	{
		if(m_AttackBox != null)
		{
			m_AttackBox.center = transform.position;
			m_AttackBox.size *= m_ExplosionRangeMultiplier;
		}
		
		Destroy (gameObject.GetComponent<Rigidbody> ());
		for(int i = 0; i < transform.childCount; i++)
		{
			m_Children.Add(transform.GetChild(i).gameObject);
			transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
			transform.GetChild(i).gameObject.rigidbody.AddForce(transform.up * m_DismembermentForce, ForceMode.Impulse);
		}
		transform.DetachChildren ();
	}

	protected override void SwitchDirection()
	{

	}
}
