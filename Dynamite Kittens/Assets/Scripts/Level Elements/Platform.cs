using UnityEngine;
using System.Collections;

public class Platform : LevelElement 
{
	void Start () 
    {
        PlayerCanInteractWith = true;
        EnemyCanInteractWith = true;
        GoreCanInteractWith = true;
	}
}
