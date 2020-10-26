using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    //public string enemyTag = "Enemy";

    public void Damage(int dmg)
    {
        health -= dmg;

        if (health <=0)
        {
            switch(this.gameObject.tag)
            {
                case "Enemy":
                    GameController.currEnemies--;//track for wave
                    ResourcePool.AddResource(10);//add resource
                    break;
                case "Factory":
                    break;
                default:
                    break;
            }
            //Delete Gameobject
            Destroy(this.gameObject);
        }
    }
}
