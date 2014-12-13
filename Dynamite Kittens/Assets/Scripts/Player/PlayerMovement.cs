using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float m_Acceleration;
	public float m_MoveSpeed;
	public float m_JumpSpeed;

	Vector2 m_CurrentSpeed;

	float m_Gravity = -9.81f;

	bool m_IsGrounded = false;

	PlayerInput m_Input;
	CharacterController m_Controller;
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
		m_Controller = GetComponent<CharacterController>();
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

		m_IsGrounded = (m_Controller.Move (m_CurrentSpeed * Time.deltaTime) & CollisionFlags.Below) != 0;

		m_Animator.SetFloat("Horizontal Speed", m_CurrentSpeed.x);
		m_Animator.SetFloat ("Vertical Speed", m_CurrentSpeed.y);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if(hit.collider.tag == "Level")
		{

		}
		else if(hit.collider.tag == "Enemy")
		{

		}
	}
}
