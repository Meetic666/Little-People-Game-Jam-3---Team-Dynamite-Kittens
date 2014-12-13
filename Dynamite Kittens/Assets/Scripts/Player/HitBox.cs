using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitBox : MonoBehaviour 
{
	public int m_DamageAmount;
	public float m_Radius;

	List<GameObject> m_ObjectsInteractedWith;

	void Start()
	{
		m_ObjectsInteractedWith = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, m_Radius);

		List<GameObject> objectInteractedWithThisFrame = new List<GameObject>();

		foreach(Collider otherCollider in colliders)
		{
			if(!m_ObjectsInteractedWith.Contains(otherCollider.gameObject) && !objectInteractedWithThisFrame.Contains(otherCollider.gameObject))
			{
				OnCollisionWithTrigger (otherCollider);

				objectInteractedWithThisFrame.Add (otherCollider.gameObject);
			}
		}

		m_ObjectsInteractedWith.Clear();

		foreach(Collider otherCollider in colliders)
		{
			if(!m_ObjectsInteractedWith.Contains(otherCollider.gameObject))
			{
				m_ObjectsInteractedWith.Add (otherCollider.gameObject);
			}
		}
	}

	void OnCollisionWithTrigger(Collider collider)
	{
		if(collider.tag != tag && collider.GetComponent<Health>())
		{
			collider.GetComponent<Health>().Damage(m_DamageAmount);
		}
	}
}
