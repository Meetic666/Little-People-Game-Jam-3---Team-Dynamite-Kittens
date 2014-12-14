using UnityEngine;
using System.Collections;

public class PlayerCheckpoint : MonoBehaviour 
{
	Vector3 m_CheckpointPosition;

	GameObject[] m_Enemies;
	GameObject m_Boss;

	// Use this for initialization
	void Start () 
	{
		SetCheckpoint();

		m_Enemies = GameObject.FindGameObjectsWithTag("Enemy");
		m_Boss = GameObject.FindGameObjectWithTag ("Boss");
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
			enemy.GetComponent<BaseAI>().Respawn();
		}

		m_Boss.GetComponent<BabyBoss> ().Respawn ();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Checkpoint")
		{
			SetCheckpoint();
		}
	}
}
