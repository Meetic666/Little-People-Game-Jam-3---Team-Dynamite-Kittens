using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
    public float MoveHorizontal
    {
        get;
        protected set;
    }

    public float MoveVertical
    {
        get;
        protected set;
    }

    public bool Attack
    {
        get;
        protected set;
    }

    public bool Jump
    {
        get;
        protected set;
    }
	
	// Update is called once per frame
	void Update () 
    {
        MoveHorizontal = Input.GetAxis("Horizontal");
        MoveVertical = Input.GetAxis("Vertical");

        Attack = Input.GetButtonDown("Attack");

        Jump = Input.GetButtonDown("Jump");
	}
}
