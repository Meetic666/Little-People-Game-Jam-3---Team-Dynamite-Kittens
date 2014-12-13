using UnityEngine;
using System.Collections;

public class AnimatedPlatform : MonoBehaviour 
{
	public bool m_IsEye;
	public bool m_IsMouth;

	// Use this for initialization
	void Start () 
	{
		GetComponent<Animator>().SetBool("IsEye", m_IsEye);
		GetComponent<Animator>().SetBool("IsMouth", m_IsMouth);
	}
}
