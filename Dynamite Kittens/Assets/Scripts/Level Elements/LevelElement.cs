using UnityEngine;
using System.Collections;

public class LevelElement : MonoBehaviour 
{
    public bool EnemyCanInteractWith = true;
    public bool PlayerCanInteractWith = true;

    private void InteractWith(PlayerMovement player)
    {
        //VirtualInteractWith(player);
    }

    //private void InteractWith(Enemy enemy)
    //{
    //    VirtualInteractWith(enemy);
    //}

    //protected void VirtualInteractWith(PlayerMovement player);
    //protected void VirtualInteractWith(Enemy enemy);

    protected void SwitchTexture()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerCanInteractWith == true)
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                InteractWith(player);
            }
        }

        if (collision.gameObject.tag == "Enemy" && EnemyCanInteractWith == true)
        {

        }
    }
}
