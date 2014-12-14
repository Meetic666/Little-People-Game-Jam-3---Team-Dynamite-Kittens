using UnityEngine;
using System.Collections;

public class GoreBehaviour : MonoBehaviour 
{
    private bool m_Locked = false;
    private float m_Counter;
    public float MaxLifeTime = 2.0f;
    

	void Start () 
    {
        m_Counter = MaxLifeTime;
	}
	
	void Update () 
    {
        if (m_Locked != true)
        {
            if (m_Counter <= 0)
            {
                Destroy(this.gameObject);
            }
            m_Counter -= Time.deltaTime;
        }    
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Level")
        {
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            body.isKinematic = true;
            body.velocity = Vector2.zero;
            m_Locked = true;
        }
    }
}
