using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BodyExplosion))]
public class DynamiteKitten : BaseAI 
{
	bool m_FuseLit = false;
	public Vector2 m_KnockBackForce;

	public float m_ExplosionRangeMultiplier = 2;

	protected override void VirtualStart()
	{
		m_FuseTimer = m_FuseTime;
	}

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

	protected override void VirtualAttack()
	{
		m_FuseLit = true;
        Sources[1].Play();
	}

	protected override void VirtualDied()
	{
		if(m_AttackBox != null)
		{
			m_AttackBox.center = Vector3.zero;
			m_AttackBox.size *= m_ExplosionRangeMultiplier;
		}

		if(m_PlayerTarget != null && Vector2.Distance(transform.position, m_PlayerTarget.transform.position) < 1.5f)
		{
			m_PlayerTarget.Damage(1);
		}

		gameObject.GetComponent<BodyExplosion> ().Explode ();
		m_CurrentState = ActionState.e_Idle;
	}

	protected override void VirtualDamage()
	{
		rigidbody2D.AddForce (m_KnockBackForce, ForceMode2D.Impulse);
		m_CurrentState = ActionState.e_Attacking;
	}
}
