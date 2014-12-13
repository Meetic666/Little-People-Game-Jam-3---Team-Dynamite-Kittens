using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseAI), typeof(BodyExplosion))]
public class DynamiteKitten : BaseAI 
{
	bool m_FuseLit = false;

	public float m_FuseTimer = 2.5f;
	public float m_ExplosionRangeMultiplier = 2;
	public float m_DismembermentForce = 1;

	protected override void VirtualUpdate()
	{
		if(m_FuseLit)
		{
			m_FuseTimer -= Time.deltaTime;

			if(m_FuseTimer <= 0)
			{
				m_CurrentState = ActionState.e_Dead;
				m_FuseLit = false;
			}
		}
	}

	protected override void VirtualMove()
	{

	}

	protected override void VirtualAttack()
	{
		m_FuseLit = true;	}

	protected override void VirtualDied()
	{
		if(m_AttackBox != null)
		{
			m_AttackBox.center = Vector3.zero;
			m_AttackBox.size *= m_ExplosionRangeMultiplier;
		}

		gameObject.GetComponent<BodyExplosion> ().Explode ();
	}

	protected override void VirtualSwitchDirection()
	{

	}
}
