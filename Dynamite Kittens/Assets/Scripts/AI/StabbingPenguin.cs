using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BodyExplosion))]
public class StabbingPenguin : BaseAI 
{
	bool m_FuseLit = false;
	bool m_IsSliding = false;
	Vector2 m_SightDirection = new Vector2 (-1, 0);

	public float m_SightRange = 1;
	public float m_AditionalSlidingSpeed = 0.05f;
	public Vector2 m_KnockBackForce;
	public float m_FuseTimer = 2.5f;
	public float m_ExplosionRangeMultiplier = 2;

	protected override void VirtualStart()
	{
		m_AttackBox.center = m_SightDirection;
		m_AttackBox.size = new Vector2 (2, 1);
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
		m_IsSliding = true;

		if(m_IsSliding)
		{
			transform.position += -transform.right * (m_MovementSpeed + m_AditionalSlidingSpeed);
		}
	}
	
	protected override void VirtualDied()
	{
		if(m_AttackBox != null)
		{
			m_AttackBox.center = Vector3.zero;
			m_AttackBox.size *= m_ExplosionRangeMultiplier;
		}
		
		gameObject.GetComponent<BodyExplosion> ().Explode ();
		m_CurrentState = ActionState.e_Idle;
	}
	
	protected override void VirtualDamage()
	{
		m_IsSliding = false;
		rigidbody2D.AddForce (m_KnockBackForce, ForceMode2D.Impulse);
		m_CurrentState = ActionState.e_Idle;
	}

	protected override void VirtualSwitchDirection()
	{
		m_SightDirection *= -1;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.GetComponent<PlayerMovement>() != null)
		{
			m_CurrentState = ActionState.e_Attacking;
		}
	}
}
