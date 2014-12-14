using UnityEngine;
using System.Collections;

public class PlayerCheckpoint : MonoBehaviour 
{
	Vector3 m_CheckpointPosition;

	GameObject[] m_Enemies;

	// Use this for initialization
	void Start () 
	{
		SetCheckpoint();

		m_Enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}

	public void SetCheckpoint()
	{
		m_CheckpointPosition = transform.position;
	}

	public void Respawn()
	{
		transform.position = m_CheckpointPosition;

		foreach(GameObject enemy in m_Enemies)
		{
			enemy.SetActive(true);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Checkpoint")
		{
			SetCheckpoint();
		}
	}
}
