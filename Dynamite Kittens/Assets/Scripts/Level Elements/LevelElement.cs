using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelElement : MonoBehaviour 
{
    protected enum EmotionalState
    {
        Happy,
        Ecstatic,
        Insane
    };

    public bool EnemyCanInteractWith = true;
    public bool PlayerCanInteractWith = true;
    public bool GoreCanInteractWith = true;

    private EmotionalState emotionalState = EmotionalState.Happy;

    private List<GameObject> InteractingObjects = new List<GameObject>();

    private void InteractWith(PlayerMovement player)
    {
        VirtualInteractWith(player);
    }

    private void StopInteractWith(PlayerMovement player)
    {
        VirtualStopInteractWith(player);
    }

    private void InteractWith(GameObject gore)
    {
        VirtualInteractWith(gore);
    }

    private void StopInteractWith(GameObject gore)
    {
        VirtualStopInteractWith(gore);
    }

    //private void InteractWith(Enemy enemy)
    //{
    //    VirtualInteractWith(enemy);
    //}

    //private void StopInteractWith(Enemy enemy)
    //{
    //    VirtualStopInteractWith(enemy);
    //}

    protected virtual void VirtualInteractWith(PlayerMovement player)
    {

    }

    protected virtual void VirtualStopInteractWith(PlayerMovement player)
    {

    }

    protected virtual void VirtualInteractWith(GameObject gore)
    {

    }

    protected virtual void VirtualStopInteractWith(GameObject gore)
    {

    }

    //protected virtual void VirtualInteractWith(Enemy enemy)
    //{

    //}

    //protected virtual void VirtualStopInteractWith(Enemy enemy)
    //{

    //}

    protected virtual void PlayAnimation(EmotionalState state)
    {

    }

    protected virtual void ChangeState(EmotionalState state)
    {
        if (emotionalState == EmotionalState.Insane ||
            emotionalState == state) { return; }

        emotionalState = state;
    }

    private void AddObjectToList(GameObject obj)
    {
        InteractingObjects.Add(obj);
    }

    private void RemoveObjectFromList(GameObject obj)
    {
        foreach (GameObject interactingObject in InteractingObjects)
        {
            if (interactingObject == obj)
            {
                InteractingObjects.Remove(obj);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerCanInteractWith == true)
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

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

        if (collision.gameObject.tag == "Gore" && GoreCanInteractWith == true)
        {
            ChangeState(EmotionalState.Insane);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerCanInteractWith == true)
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                RemoveObjectFromList(player.gameObject);
                if (InteractingObjects.Count == 0) { ChangeState(EmotionalState.Happy); }
                StopInteractWith(player);
            }
        }

        if (collision.gameObject.tag == "Enemy" && EnemyCanInteractWith == true)
        {

        }
    }
}
