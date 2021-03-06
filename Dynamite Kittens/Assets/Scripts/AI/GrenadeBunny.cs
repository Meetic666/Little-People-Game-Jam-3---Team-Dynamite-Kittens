﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BodyExplosion))]
public class GrenadeBunny : BaseAI 
{
	public float m_HopHeight;
	public float m_HopIntervalTime;
	float m_HopIntervalTimer = 0;

	public Vector2 m_KnockBackForce;
	public float m_ExplosionRangeMultiplier = 2;

	protected override void VirtualStart()
	{
		m_MovementSpeed *= -1;
		m_FuseTimer = m_FuseTime;
	}

	protected override void VirtualUpdate()
	{
		transform.up = Vector2.up;

		if(m_FuseHasBeenLit)
		{
			m_FuseTimer -= Time.deltaTime;
			
			if(m_FuseTimer <= 0)
			{
				m_CurrentState = ActionState.e_Dead;
				m_FuseHasBeenLit = false;
			}
		}
	}

	protected override void VirtualMove()
	{
		if(m_HopIntervalTimer <= 0)
		{
			rigidbody2D.AddForce(Vector2.up * m_HopHeight, ForceMode2D.Impulse);
			m_HopIntervalTimer = m_HopIntervalTime;
		}
		else
		{
			m_HopIntervalTimer -= Time.deltaTime;
		}
	}

	protected override void VirtualAttack()
	{
        if (m_FuseHasBeenLit == false)
        {
            m_FuseHasBeenLit = true;
        }
	}
	
	protected override void VirtualDied()
	{
		if(m_AttackBox != null)
		{
			m_AttackBox.center = Vector3.zero;
			m_AttackBox.size *= m_ExplosionRangeMultiplier;
		}

		if(Vector2.Distance(transform.position, m_PlayerTarget.transform.position) < 1)
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
