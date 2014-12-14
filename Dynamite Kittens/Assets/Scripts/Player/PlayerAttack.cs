using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour 
{
	public GameObject m_ProjectilePrefab;

	public Vector3 m_HitBoxCreationOffset;
	public int m_DamageAmount;

	public float m_AttackDelay;
	float m_AttackTimer;

	PlayerInput m_Input;
	Animator m_Animator;

	void Start()
	{
		m_Input = GetComponent<PlayerInput>();
		m_Animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Animator.SetBool("Attack", m_Input.Attack);

		if(m_AttackTimer > 0.0f)
		{
			m_AttackTimer -= Time.deltaTime;

			if(m_AttackTimer <= 0.0f)
			{
				Attack ();
			}
		}
		else
		{
			if(m_Input.Attack)
			{
				m_AttackTimer = m_AttackDelay;
            }
		}
	}

	void Attack()
	{
		Vector3 offset = m_HitBoxCreationOffset;
		Vector3 direction = transform.right;

		if(transform.lossyScale.x < 0.0f)
		{
			offset.x *= -1;
			direction.x *= -1;
		}

		GameObject newProjectile = (GameObject) Instantiate(m_ProjectilePrefab, transform.position + offset, Quaternion.identity);

		newProjectile.GetComponent<Projectile>().m_Direction = direction;
		newProjectile.GetComponent<HitBox>().m_DamageAmount = m_DamageAmount;
	}
}
