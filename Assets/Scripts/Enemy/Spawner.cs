using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Enemy and SpawnPoints
    public GameObject enemy;
    public GameObject enemyHard;
    public Transform[] spawnpoints;
    //Wave Control
    public int currentWave = 1;
    static public int currEnemies = 0;
    //Spawn Control
    public float spawnInterval;
    private float spawnTime;
    public int maxSpawn;
    private int currSpawn;

    // Called once
    void Start()
    {

        spawnTime = spawnInterval;
        Spawn(0, enemy);
    }

    // Update is called once per frame
    void Update()
    {
        //Spawn Loop
        if (spawnTime <= 0 && currSpawn < maxSpawn)
        {
            int rand = Random.Range(0, spawnpoints.Length);
            GameObject tempEnemy;
            if (currentWave > 3) //Harder
            {
                tempEnemy = Random.Range(0, 11) > 9 ? enemyHard : enemy;
            }
            else { tempEnemy = enemy; }
            Spawn(rand, tempEnemy);
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
        // Wave Defeated
        if (currEnemies == 0)
        {
            //Next Wave difficulty and rest spawns
            currentWave++;
            maxSpawn += currentWave * 2;
            currSpawn = 0;
        }
    }

    private void Spawn(int spawnpoint, GameObject enemy)
    {
        Instantiate(enemy, spawnpoints[spawnpoint]);
        spawnTime = spawnInterval;
        Debug.Log("EnemySpawned");
        currEnemies++;
        currSpawn++;
    }
}
