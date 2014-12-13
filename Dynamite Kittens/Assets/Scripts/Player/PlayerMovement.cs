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
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_CurrentSpeed.x = Mathf.Lerp(m_CurrentSpeed.x, m_MoveSpeed * m_Input.MoveHorizontal, m_Acceleration * Time.deltaTime);

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
