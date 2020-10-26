using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyController : MonoBehaviour
{
    public float speed = 0.5f;
    public LayerMask collisionLayer;
    public Vector3 targetPos = new Vector3(0, 0, 0);

    [SerializeField]
    private int resourceDrop = 1;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float dmgTick = 2.0f;
    private float dmgTickCounter = 0.0f;
    [SerializeField]
    private AudioClip dmgSound;
    [SerializeField]
    private AudioClip atkSound;

    private Rigidbody2D rb2D;

    // start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb2D, "RigidBody not found");
    }

    // Update is called once per frame
    void Update()
    {
        //LookToward Center
        transform.right = targetPos - transform.position;
        //MoveToward Center
        Vector2 forward = transform.TransformDirection(Vector2.right);
        Vector2 destination = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        var hit = Physics2D.OverlapCircle(destination + forward, 0.1f, collisionLayer);
        Debug.DrawLine(transform.position, destination + forward, Color.red);
        if (hit != null) //collision
        {
            Debug.Log(hit.gameObject.name);
            if (dmgTickCounter >= dmgTick) //check if dmgtick interval
            {
                try
                {
                    AudioSource.PlayClipAtPoint(atkSound, transform.position);
                    hit.gameObject.GetComponent<Health>().Damage(damage);
                    Debug.Log(name + " damaged " + hit.gameObject.name + " for " + damage);
                }
                catch (Exception e) { Debug.Log("No health component on object: " + e); }
                dmgTickCounter = 0.0f;
            }
            dmgTickCounter += Time.deltaTime; //add time to counter
        }
        else { transform.position = destination; } //move forward

        //Health
        if (GetComponent<Health>().health <= 0)
        {
            AudioSource.PlayClipAtPoint(dmgSound, transform.position);
            Spawner.currEnemies--;   //track wave progress
            ResourcePool.AddResource(GetLoot()); //Add Loot
            Destroy(this.gameObject);
        }
    }

    public int GetLoot() { return resourceDrop; }
}
