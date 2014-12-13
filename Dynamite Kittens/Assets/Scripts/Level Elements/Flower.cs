using UnityEngine;
using System.Collections;

public class Flower : LevelElement 
{
	void Start () 
    {
        PlayerCanInteractWith = false;
        EnemyCanInteractWith = false;
        GoreCanInteractWith = true;
	}
}
