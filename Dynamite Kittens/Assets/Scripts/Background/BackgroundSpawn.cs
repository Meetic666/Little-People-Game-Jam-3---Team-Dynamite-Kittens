using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundSpawn : MonoBehaviour 
{
	public float m_StartX;
	public float m_EndX;
	public float m_StepX;

	public List<GameObject> m_BackgroundTiles;

	int m_NumberOfTiles;

	// Use this for initialization
	void Start () 
	{
		m_NumberOfTiles = (int)((m_EndX - m_StartX) / m_StepX) + 1;

		Vector3 position = transform.position;

		for(int i = 0; i < m_NumberOfTiles; i++)
		{
			int randomIndex = Random.Range (0, m_BackgroundTiles.Count);

			position.x = m_StartX + ((float)i / (float)m_NumberOfTiles) * (m_EndX - m_StartX);

			Instantiate(m_BackgroundTiles[randomIndex], position, Quaternion.identity);
		}
	}
}
