using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseAI : MonoBehaviour
{
	bool m_CanTurn = true;

	protected bool m_ChangeState = true;

	protected float m_FuseTime = 0.8f;
	protected float m_FuseTimer;

	protected Health m_PlayerTarget;

	public float m_Health = 10;
	public float m_MovementSpeed = 0.1f;
	public float m_JumpingSpeed = 0.3f;
    public List<Sprite> m_BigBlobs = new List<Sprite>();
    public GameObject m_CorpsePiece;

	public List<GameObject> m_PickUpPrefabs = new List<GameObject>();

    private BoxCollider2D[] m_Colliders;
    private BoxCollider2D m_ChildCollider;
    
	protected BoxCollider2D m_AttackBox;

	Vector3 m_StartPosition;
	Quaternion m_StartRotation;

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

		m_StartPosition = transform.position;
		m_StartRotation = transform.rotation;

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

		if(m_ChangeState)
		{
			m_CurrentState = ActionState.e_Moving;
		}

	}

	void Died()
	{
        /*m_ChildCollider.enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponentInChildren<SpriteRenderer>().enabled = false;*/

        GameObject particle = (GameObject)Instantiate(m_CorpsePiece, transform.position, transform.rotation);
        particle.GetComponent<SpriteRenderer>().sprite = m_BigBlobs[Random.Range(0, m_BigBlobs.Count)];
        particle.AddComponent<BoxCollider2D>().isTrigger = true;

		Instantiate(m_PickUpPrefabs[Random.Range (0, m_PickUpPrefabs.Count)], transform.position + Vector3.up * 0.3f, Quaternion.identity);

		VirtualDied ();
		m_CurrentState = ActionState.e_Idle;

		gameObject.SetActive (false);
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

	protected virtual void VirtualOnCollisionEnter2D(Collision2D other)
	{

	}

	protected virtual void VirtualOnTriggerEnter2D(Collider2D collider)
	{
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.GetComponent<Health>() != null)
		{
			m_PlayerTarget = other.gameObject.GetComponent<Health>();
		}

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

		VirtualOnCollisionEnter2D (other);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
		if(collider.gameObject.GetComponent<Health>() != null)
		{
			m_PlayerTarget = collider.gameObject.GetComponent<Health>();
		}

		VirtualOnTriggerEnter2D (collider);
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

	public void Respawn()
	{
		transform.position = m_StartPosition;
		transform.rotation = m_StartRotation;

		m_CurrentState = ActionState.e_Moving;

		gameObject.SetActive(true);
	}
}
