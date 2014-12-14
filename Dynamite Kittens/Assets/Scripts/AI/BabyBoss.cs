using UnityEngine;
using System.Collections;

public class BabyBoss : MonoBehaviour 
{
	public float m_Health;
	
	public float m_SpawnIntervalTime;
	float m_SpawnIntervalTimer;
	
	public float m_JointRotationSpeed;
	float m_CurrentJointRotationSpeed;
	public float m_BobbingSpeed;
	
	//Limb Joints
	public GameObject m_HeadJoint;
	public GameObject m_LeftArm;
	public GameObject m_RightArm;
	public GameObject m_LeftLeg;
	public GameObject m_RightLeg;
	
	//Enemies to spawn
	public GameObject[] m_Enemies = new GameObject[3];
	
	public GameObject m_SpawnPoint;
	
	// Use this for initialization
	void Start () 
	{
		m_SpawnIntervalTimer = m_SpawnIntervalTime;
		m_CurrentJointRotationSpeed = m_JointRotationSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_CurrentJointRotationSpeed > 0)
		{
			
		}
	}
}
