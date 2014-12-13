using UnityEngine;
using System.Collections;

public class BaseAI : MonoBehaviour
{
	bool m_IsMovingRight = true;

	public float m_Health = 10;
	public float m_MovementSpeed = 0.1f;
	public float m_JumpingSpeed = 0.3f;

	protected BoxCollider2D m_AttackBox;

	protected ActionState m_CurrentState = ActionState.e_Moving;
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
		//gameObject.AddComponent<BoxCollider2D>();
		m_AttackBox = gameObject.GetComponent<BoxCollider2D> ();
		m_AttackBox.isTrigger = true;
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
		transform.position += -transform.right * m_MovementSpeed;

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

	void SwitchDirection ()
	{
		//m_MovementSpeed *= -1;
		transform.Rotate (Vector2.up, 180);

		VirtualSwitchDirection ();
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

	protected virtual void VirtualSwitchDirection()
	{

	}

	protected virtual void VirtualDamage()
	{

	}

	public void Damage(float dmg = 0)
	{
		if(m_CurrentState != ActionState.e_Idle)
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
			
			VirtualDamage ();
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.GetComponent<PlayerMovement>() != null)
		{
			if(m_CurrentState != ActionState.e_Idle)
			{
				m_CurrentState = ActionState.e_Attacking;
			}
		}
        else if (other.gameObject.tag == "Edge")
        {
            SwitchDirection();
        }
	}

    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.tag == "Edge")
    //    {
    //        SwitchDirection();
    //    }
    //}
}
