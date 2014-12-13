using UnityEngine;
using System.Collections;

public class PlayerCheckpoint : MonoBehaviour 
{
	Vector3 m_CheckpointPosition;

	// Use this for initialization
	void Start () 
	{
		SetCheckpoint();
	}

	public void SetCheckpoint()
	{
		m_CheckpointPosition = transform.position;
	}

	public void Respawn()
	{
		transform.position = m_CheckpointPosition;
	}
}
