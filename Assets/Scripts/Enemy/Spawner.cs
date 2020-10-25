using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnpoints;
    public float spawnInterval;
    private float spawnTime;
    public int maxSpawn;
    private int currSpawn;

    // Called once
    void Start()
    {
        spawnTime = spawnInterval;
        Spawn(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime <= 0 && currSpawn < maxSpawn)
        {
            int rand = Random.Range(0, spawnpoints.Length);
            Spawn(rand);
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }

    public void ResetSpawnCount()
    {
        currSpawn = 0;
    }

    private void Spawn(int spawnpoint)
    {
        Instantiate(enemy, spawnpoints[spawnpoint]);
        spawnTime = spawnInterval;
        Debug.Log("EnemySpawned");
        GameController.currEnemies++;
        currSpawn++;
    }
}
