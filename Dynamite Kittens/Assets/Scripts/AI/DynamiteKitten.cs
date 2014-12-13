using UnityEngine;
using System.Collections;

public class DynamiteKitten : BaseAI 
{
	bool m_FuseLit = false;
	float m_FuseTimer = 2.5f;
	float m_ExplosionRangeMultiplier = 2;

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
		
		//TODO: Detach all children, apply rigidBodies and have them explode
	}

	protected override void SwitchDirection()
	{

	}
}
