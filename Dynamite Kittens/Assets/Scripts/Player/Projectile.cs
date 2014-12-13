using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float m_LifeTime;
	public float m_Speed;
	public Vector3 m_Direction;
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += m_Speed * m_Direction * Time.deltaTime;

		m_LifeTime -= Time.deltaTime;

		if(m_LifeTime <= 0.0f)
		{
			Destroy (gameObject);
		}
	}
}
