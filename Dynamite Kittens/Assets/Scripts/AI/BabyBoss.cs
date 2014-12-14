using UnityEngine;
using System.Collections;

public class BabyBoss : MonoBehaviour 
{
	bool m_LeftArmUp = true;
	bool m_BobbingUp = true;
	bool m_BossEngaged = false;

	public int m_Health;
	
	public float m_SpawnIntervalTime;
	float m_SpawnIntervalTimer;
	
	public float m_JointRotationSpeed;
	float m_CurrentJointRotationSpeed;

	public float m_BobbingSpeed;
	float m_CurrentBobbingSpeed;
	
	//Limb Joints
	public GameObject m_LeftArm;
	public GameObject m_RightArm;
	
	//Enemies to spawn
	public GameObject[] m_Enemies = new GameObject[3];
	
	public GameObject m_SpawnPoint;

	// Use this for initialization
	void Start () 
	{
		m_SpawnIntervalTimer = m_SpawnIntervalTime;
		m_CurrentJointRotationSpeed = m_JointRotationSpeed;
		m_CurrentBobbingSpeed = m_BobbingSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_CurrentJointRotationSpeed > 0)
		{
			if(m_LeftArmUp)
			{
				m_LeftArm.transform.Rotate(transform.forward, m_CurrentJointRotationSpeed);
				m_RightArm.transform.Rotate(transform.forward, -m_CurrentJointRotationSpeed);
			}
			else
			{
				m_LeftArm.transform.Rotate(transform.forward, -m_CurrentJointRotationSpeed);
				m_RightArm.transform.Rotate(transform.forward, m_CurrentJointRotationSpeed);
			}

			m_CurrentJointRotationSpeed -= Time.deltaTime;
		}
		else
		{
			m_CurrentJointRotationSpeed = m_JointRotationSpeed;
			m_LeftArmUp = !m_LeftArmUp;
		}

		if(m_CurrentBobbingSpeed > 0)
		{
			if(m_BobbingUp)
			{
				transform.position += transform.up * m_CurrentBobbingSpeed;
			}
			else
			{
				transform.position += -transform.up * m_CurrentBobbingSpeed;
			}
			m_CurrentBobbingSpeed -= Time.deltaTime;
		}
		else
		{
			m_BobbingUp = !m_BobbingUp;
			m_CurrentBobbingSpeed = m_BobbingSpeed;
		}
	}

	public void Damage()
	{
		m_Health--;
		//TODO: Detach limbs until dead
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.GetComponent<PlayerMovement>() != null)
		{
			m_BossEngaged = true;
			gameObject.GetComponent<BoxCollider>().isTrigger = false;
		}
	}
}
