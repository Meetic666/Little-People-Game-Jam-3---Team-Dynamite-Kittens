using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelElement : MonoBehaviour 
{
    protected enum EmotionalState
    {
        Happy = 0,
        Ecstatic,
        Insane,
        None
    };

    public bool EnemyCanInteractWith = true;
    public bool PlayerCanInteractWith = true;
    public bool GoreCanInteractWith = true;

    private Animator m_Animator;
    private EmotionalState m_EmotionalState = EmotionalState.Happy;
    private List<GameObject> m_InteractingObjects = new List<GameObject>();
    private Animator[] m_AnimatedSprites;

    void Awake()
    {
        m_AnimatedSprites = GetComponentsInChildren<Animator>();
    }

    private void InteractWith(GameObject player)
    {
        VirtualInteractWith(player);
    }

    private void StopInteractWith(GameObject player)
    {
        VirtualStopInteractWith(player);
    }

    //private void InteractWith(GameObject gore)
    //{
    //    VirtualInteractWith(gore);
    //}

    //private void StopInteractWith(GameObject gore)
    //{
    //    VirtualStopInteractWith(gore);
    //}

    //private void InteractWith(Enemy enemy)
    //{
    //    VirtualInteractWith(enemy);
    //}

    //private void StopInteractWith(Enemy enemy)
    //{
    //    VirtualStopInteractWith(enemy);
    //}

    protected virtual void VirtualInteractWith(GameObject player)
    {

    }

    protected virtual void VirtualStopInteractWith(GameObject player)
    {

    }

    //protected virtual void VirtualInteractWith(GameObject gore)
    //{

    //}

    //protected virtual void VirtualStopInteractWith(GameObject gore)
    //{

    //}

    //protected virtual void VirtualInteractWith(Enemy enemy)
    //{

    //}

    //protected virtual void VirtualStopInteractWith(Enemy enemy)
    //{

    //}

    protected virtual void PlayAnimation(EmotionalState state)
    {
        switch (state)
        {
            case EmotionalState.Happy:
                for (int i = 0; i < m_AnimatedSprites.Length; i++)
                {
                    m_AnimatedSprites[i].SetBool("IsEcstatic", false);
                    m_AnimatedSprites[i].SetBool("IsInsane", false);
                }
                break;
            case EmotionalState.Ecstatic:
                for (int i = 0; i < m_AnimatedSprites.Length; i++)
                {
                    m_AnimatedSprites[i].SetBool("IsEcstatic", true);
                    m_AnimatedSprites[i].SetBool("IsInsane", false);
                }
                break;
            case EmotionalState.Insane:
                for (int i = 0; i < m_AnimatedSprites.Length; i++)
                {
                    m_AnimatedSprites[i].SetBool("IsEcstatic", false);
                    m_AnimatedSprites[i].SetBool("IsInsane", true);
                }
                break;
            case EmotionalState.None:
                break;
        }
    }

    protected virtual void ChangeState(EmotionalState state)
    {
        if (m_EmotionalState == EmotionalState.Insane ||
            m_EmotionalState == state) { return; }

        m_EmotionalState = state;
        PlayAnimation(state);
    }

    private void AddObjectToList(GameObject obj)
    {
        m_InteractingObjects.Add(obj);
    }

    private void RemoveObjectFromList(GameObject obj)
    {
        m_InteractingObjects.Remove(obj);
    }

    private void CheckIfInteractingObjectIsGore(GameObject obj)
    {
        if (obj.tag == "Gore" && GoreCanInteractWith == true)
        {
            ChangeState(EmotionalState.Insane);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CheckIfInteractingObjectIsGore(collider.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerCanInteractWith == true)
        {
            //PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            GameObject player = collision.gameObject;

            if (player != null)
            {
                ChangeState(EmotionalState.Ecstatic);
                AddObjectToList(player.gameObject);
                InteractWith(player);
            }
        }

        if (collision.gameObject.tag == "Enemy" && EnemyCanInteractWith == true)
        {
            ChangeState(EmotionalState.Ecstatic);
        }

        CheckIfInteractingObjectIsGore(collision.gameObject);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerCanInteractWith == true)
        {
            //PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            GameObject player = collision.gameObject;

            if (player != null)
            {
                RemoveObjectFromList(player.gameObject);
                if (m_InteractingObjects.Count == 0) { ChangeState(EmotionalState.Happy); }
                StopInteractWith(player);
            }
        }

        if (collision.gameObject.tag == "Enemy" && EnemyCanInteractWith == true)
        {

        }
    }
}
