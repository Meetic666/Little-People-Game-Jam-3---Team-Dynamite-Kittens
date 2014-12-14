using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BodyExplosion))]
public class StabbingPenguin : BaseAI 
{
	bool m_FuseLit = false;
	Vector2 m_SightDirection = new Vector2 (1, 0);
	Animator m_Animator;

	public float m_SightRange = 1;
	public float m_AditionalSlidingSpeed = 0.05f;
	public Vector2 m_KnockBackForce;
	public float m_ExplosionRangeMultiplier = 2;

	protected override void VirtualStart()
	{
		m_FuseTimer = m_FuseTime;
		m_AttackBox.center = m_SightDirection;
		m_AttackBox.size = new Vector2 (1.5f, 0.5f);
		m_MovementSpeed *= -1;
		m_Animator = transform.GetComponentInChildren<Animator> ();
		m_FuseTimer = 1.5f;
	}

	protected override void VirtualUpdate()
	{
		Debug.Log (m_CurrentState.ToString ());

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
		transform.position += transform.right * (m_MovementSpeed + m_AditionalSlidingSpeed);
		m_ChangeState = false;
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

		m_CurrentState = ActionState.e_Idle;
		m_ChangeState = true;
	}
	
	protected override void VirtualDamage()
	{
		m_Animator.SetBool("Walking", true);
		m_Animator.SetBool("Sliding", false);

		rigidbody2D.AddForce (m_KnockBackForce, ForceMode2D.Impulse);
		m_FuseLit = true;
	}

	protected override void VirtualOnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.GetComponent<PlayerMovement>() != null)
		{
			m_Animator.SetBool("Walking", false);
			m_Animator.SetBool("Sliding", true);
			m_CurrentState = ActionState.e_Attacking;
			m_ChangeState = false;
		}
	}

	protected override void VirtualOnCollisionEnter2D(Collision2D other)
	{
		if(m_CurrentState == ActionState.e_Attacking)
		{
			if(other.gameObject.GetComponent<PlayerMovement>() != null)
			{
				m_PlayerTarget.Damage(1);
				
				gameObject.GetComponent<BodyExplosion> ().Explode ();
				m_CurrentState = ActionState.e_Dead;
				m_FuseLit = false;

				//gameObject.SetActive(false);
			}
		}
	}
}
