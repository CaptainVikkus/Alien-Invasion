using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnpoints;
    public float spawnInterval;
    public int xRange;
    public int yRange;

    private float spawnTime;

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
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }
}
