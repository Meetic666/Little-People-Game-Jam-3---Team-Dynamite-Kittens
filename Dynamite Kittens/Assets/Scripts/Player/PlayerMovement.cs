using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float m_Acceleration;
	public float m_MoveSpeed;
	public float m_JumpSpeed;

	PlayerInput m_Input;
	Animator m_Animator;

	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<PlayerInput>();
		m_Animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 newVelocity = rigidbody2D.velocity;

		newVelocity.x = Mathf.Lerp(newVelocity.x, m_MoveSpeed * m_Input.MoveHorizontal, m_Acceleration * Time.deltaTime);

		if((transform.lossyScale.x > 0.0f && rigidbody2D.velocity.x < 0.0f)
		   || (transform.lossyScale.x < 0.0f && rigidbody2D.velocity.x > 0.0f))
		{
			Vector3 newScale = transform.localScale;
			newScale.x *= -1;
			transform.localScale = newScale;
		}

		if(Mathf.Abs (newVelocity.y) < 0.1f)
		{			
			m_Animator.SetBool("Jump", false);

			if(m_Input.Jump)
			{
				newVelocity.y = m_JumpSpeed;
			}
		}
		else
		{
			m_Animator.SetBool("Jump", true);
		}

		if(Mathf.Abs (newVelocity.x) < 0.1f)
		{			
			m_Animator.SetBool("Walking", false);
        }
        else
        {
            m_Animator.SetBool("Walking", true);
        }

		rigidbody2D.velocity = newVelocity;
	}
}
