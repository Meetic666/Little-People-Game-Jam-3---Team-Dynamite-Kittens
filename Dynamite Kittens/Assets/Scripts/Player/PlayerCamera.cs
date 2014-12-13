using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour 
{
	public PlayerMovement m_TargetPlayer;

	public float m_CameraSpeed;
	public Vector3 m_Offset;
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 newPosition = transform.position;
		Vector3 targetPosition = m_TargetPlayer.transform.position + m_Offset;

		newPosition = Vector3.Lerp (newPosition, targetPosition, m_CameraSpeed * Time.deltaTime);

		transform.position = newPosition;
	}
}
