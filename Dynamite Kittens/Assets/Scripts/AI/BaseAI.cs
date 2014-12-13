using UnityEngine;
using System.Collections;

public class BaseAI : MonoBehaviour
{
	float m_Health = 10;
	float m_MovementSpeed = 0.1f;
	float m_JumpingSpeed = 0.3f;

	protected BoxCollider m_AttackBox;

	protected ActionState m_CurrentState = ActionState.e_Idle;
	protected enum ActionState
	{
		e_Idle = 0,
		e_Moving,
		e_Attacking,
		e_Dead
	}

	protected float MovementSpeed
	{
		get{ return m_MovementSpeed; }
	}

	void Start()
	{
		if(gameObject.GetComponent<BoxCollider>() == null)
		{
			m_AttackBox = gameObject.AddComponent<BoxCollider>();
			m_AttackBox.center = transform.right * m_MovementSpeed;
		}
	}

	void Update()
	{
		switch(m_CurrentState)
		{
			case ActionState.e_Moving:
			{
				Move ();
				break;
			}
			case ActionState.e_Attacking:
			{
				Attack();
				break;
			}
			case ActionState.e_Dead:
			{
				Died();
				break;
			}
			default:
				break;
		}

		VirtualUpdate ();
	}

	void Move()
	{
		transform.position += transform.right * m_MovementSpeed;

		VirtualMove();
	}

	void Attack()
	{
		VirtualAttack ();
	}

	void Died()
	{
		VirtualDied ();
		m_CurrentState = ActionState.e_Idle;
	}

	protected virtual void VirtualUpdate()
	{

	}

	protected virtual void VirtualMove()
	{

	}

	protected virtual void VirtualAttack()
	{

	}

	protected virtual void VirtualDied()
	{

	}

	protected virtual void SwitchDirection()
	{

	}

	public void Damage(float dmg = 0)
	{
		if(dmg == 0)
		{
			m_Health = 0;
		}
		else
		{
			m_Health -= dmg;
		}

		if(m_Health <= 0)
		{
			m_CurrentState = ActionState.e_Dead;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.GetComponent<PlayerMovement>() != null)
		{
			m_CurrentState = ActionState.e_Attacking;
		}
		else if(other.gameObject.tag == "Edge")
		{
			m_MovementSpeed *= -1;
			SwitchDirection();
		}
	}
}
