using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseAI : MonoBehaviour
{
	bool m_IsMovingRight = true;
	bool m_CanTurn = true;

	protected Collision2D m_CurrentCollision;
	protected Collider2D m_CurrentCollider;

	public float m_Health = 10;
	public float m_MovementSpeed = 0.1f;
	public float m_JumpingSpeed = 0.3f;
    public List<Sprite> m_BigBlobs = new List<Sprite>();
    public GameObject m_CorpsePiece;

    private BoxCollider2D[] m_Colliders;
    private BoxCollider2D m_ChildCollider;
    
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

        m_Colliders = GetComponentsInChildren<BoxCollider2D>();
        for (int i = 0; i < m_Colliders.Length; i++)
        {
            if (m_Colliders[i].isTrigger == false)
            {
                m_ChildCollider = m_Colliders[i];
            }
        }

		VirtualStart ();
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
        m_ChildCollider.enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponentInChildren<SpriteRenderer>().enabled = false;

        GameObject particle = (GameObject)Instantiate(m_CorpsePiece, transform.position, transform.rotation);
        m_CorpsePiece.GetComponent<SpriteRenderer>().sprite = m_BigBlobs[Random.Range(0, m_BigBlobs.Count)];
        //m_GorePiece.rigidbody2D.isKinematic = true;

		VirtualDied ();
		m_CurrentState = ActionState.e_Idle;
	}

	void SwitchDirection ()
	{
		//m_MovementSpeed *= -1;
		transform.Rotate (Vector2.up, 180);

		VirtualSwitchDirection ();
	}

	protected virtual void VirtualStart()
	{

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

	protected virtual void VirtualOnCollisionEnter2D()
	{

	}

	protected virtual void VirtualOnTriggerEnter2D()
	{
		
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

		VirtualOnCollisionEnter2D ();
		m_CurrentCollision = null;
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
		VirtualOnTriggerEnter2D ();

		m_CurrentCollider = null;
    }

	void OnTriggerStay2D(Collider2D collider)
	{
		if(m_CanTurn)
		{
			if (collider.gameObject.tag == "Edge")
			{
				if(Vector2.Distance(transform.position, collider.transform.position) <= 1)
				{
					SwitchDirection();
					m_CanTurn = false;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "Edge")
		{
			m_CanTurn = true;
		}
	}
}
