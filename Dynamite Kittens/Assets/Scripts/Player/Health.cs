using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public enum DeathBehaviour
	{
		e_Respawn,
		e_DieForGood
	}

	public DeathBehaviour m_DeathBehaviour;

	public int m_MaxHealth;
	int m_Health;

	public Vector2 m_KnockBackForceMax;

	public int CurrentHealth
	{
		get
		{
			return m_Health;
		}

		set
		{
			m_Health = value;

			if(m_Health > m_MaxHealth)
			{
				m_Health = m_MaxHealth;
			}
		}
	}


	// Use this for initialization
	void Start () 
	{
		m_Health = m_MaxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Damage(int damageAmount)
	{
		m_Health -= damageAmount;

		if(m_Health <= 0)
		{
			DoDeathBehaviour();
		}

		gameObject.GetComponent<PlayerMovement> ().m_AdditionalForce = m_KnockBackForceMax * gameObject.GetComponent<PlayerMovement> ().m_Direction;
	}

	public void DoDeathBehaviour()
	{
		switch(m_DeathBehaviour)
		{
		case DeathBehaviour.e_DieForGood:
			// TODO: plug in death state for enemy
			break;

		case DeathBehaviour.e_Respawn:
			m_Health = m_MaxHealth;
			GetComponent<PlayerCheckpoint>().Respawn ();
			break;
		}
	}

	void OnGUI()
	{
		Rect rect = Camera.main.pixelRect;
		rect.width *= 0.1f;
		rect.height *= 0.1f;

		GUI.TextArea(rect, "Health: " + m_Health + " / " + m_MaxHealth);
	}
}
