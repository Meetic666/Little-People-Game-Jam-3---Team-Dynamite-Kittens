using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour 
{
	protected Animator m_Animator;

	// Use this for initialization
	void Start () 
	{
		m_Animator = GetComponent<Animator>();
	}
}
