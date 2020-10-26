using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Basic = 0,
    Rocket = 1
}
public class BulletController : MonoBehaviour
{
    public BulletType type = BulletType.Basic;
    public int damage = 1;
    public float bounds = 15;
    [SerializeField]
    private float speed = 1;
    public BulletFactory manager;

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        //Return or Delete when out of bounds
        if (transform.position.y > bounds || transform.position.x > bounds)
        {
            manager.returnBullet(this.gameObject, type);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().Damage(damage);
            manager.returnBullet(this.gameObject, type);
        }
    }
}
