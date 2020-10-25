using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnpoints;
    public float spawnInterval;
    private float spawnTime;
    private int maxSpawn;
    private int currSpawn;

    // Called once
    void Start()
    {
        spawnTime = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime <= 0)
        {
            int rand = Random.Range(0, spawnpoints.Length);
            Instantiate(enemy, spawnpoints[rand]);
            spawnTime = spawnInterval;
            Debug.Log("EnemySpawned");
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }

    public void SetMaxSpawn(int max)
    {
        maxSpawn = max;
    }

    public void ResetSpawnCount()
    {
        currSpawn = 0;
    }
}
