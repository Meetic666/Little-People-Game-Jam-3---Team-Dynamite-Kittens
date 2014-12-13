using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float m_Acceleration;
	public float m_MoveSpeed;
	public float m_JumpSpeed;
	public float m_GroundDetectionDistance;
	public LayerMask m_GroundLayers;

	Vector2 m_CurrentSpeed;

	float m_Gravity = -9.81f;

	bool m_IsGrounded = false;

	PlayerInput m_Input;
	Animator m_Animator;

	public Vector2 CurrentSpeed
	{
		get
		{
			return m_CurrentSpeed;
		}
	}

	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<PlayerInput>();
		m_Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_CurrentSpeed.x = Mathf.Lerp(m_CurrentSpeed.x, m_MoveSpeed * m_Input.MoveHorizontal, m_Acceleration * Time.deltaTime);

		if((transform.lossyScale.x > 0.0f && m_CurrentSpeed.x < 0.0f)
		   || (transform.lossyScale.x < 0.0f && m_CurrentSpeed.x > 0.0f))
		{
			Vector3 newScale = transform.localScale;
			newScale.x *= -1;
			transform.localScale = newScale;
		}

		if(m_IsGrounded)
		{
			if(m_Input.Jump)
			{
				m_CurrentSpeed.y = m_JumpSpeed;
			}
		}
		else
		{
			m_CurrentSpeed.y += m_Gravity * Time.deltaTime;
		}
		
		Vector2 position = new Vector2(transform.position.x, transform.position.y);

		RaycastHit2D hitInfo = Physics2D.Raycast(position, -Vector2.up, m_GroundDetectionDistance, m_GroundLayers.value);

		if(hitInfo.collider != null
		   && hitInfo.collider.tag == "Level")
		{
			m_IsGrounded = true;
		}
		else
		{
			m_IsGrounded = false;
		}

		rigidbody2D.velocity = m_CurrentSpeed;

		m_Animator.SetFloat("Horizontal Speed", rigidbody2D.velocity.x);
		m_Animator.SetFloat ("Vertical Speed", rigidbody2D.velocity.y);
	}
}
