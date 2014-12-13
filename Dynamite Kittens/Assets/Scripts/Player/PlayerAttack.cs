using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour 
{
	public GameObject m_ProjectilePrefab;

	public Vector3 m_HitBoxCreationOffset;

	PlayerInput m_Input;
	PlayerMovement m_Movement;

	void Start()
	{
		m_Input = GetComponent<PlayerInput>();
		m_Movement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Input.Attack)
		{
			Attack();
		}
	}

	void Attack()
	{
		Vector3 offset = m_HitBoxCreationOffset;
		Vector3 direction = transform.right;

		if(m_Movement.CurrentSpeed.x < 0.0f)
		{
			offset.x *= -1;
			direction.x *= -1;
		}

		GameObject newProjectile = (GameObject) Instantiate(m_ProjectilePrefab, transform.position + offset, Quaternion.identity);

		newProjectile.GetComponent<Projectile>().m_Direction = direction;
	}
}
