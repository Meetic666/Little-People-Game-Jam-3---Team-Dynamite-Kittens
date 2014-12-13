using UnityEngine;
using System.Collections;

public enum PlayerAnimationState
{
	e_Idle,
	e_WalkingRight,
	e_WalkingLeft,
	e_Jumping,
	e_Attacking
}

public class PlayerAnimationManager : AnimationManager
{
	public void SetAnimationState(PlayerAnimationState state)
	{
		switch(state)
		{
		case PlayerAnimationState.e_Attacking:
			break;

		case PlayerAnimationState.e_Idle:
			break;

		case PlayerAnimationState.e_WalkingRight:
			break;

		case PlayerAnimationState.e_WalkingLeft:
			break;

		case PlayerAnimationState.e_Jumping:
			break;
		}
	}
}
