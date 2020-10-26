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
    }
}
