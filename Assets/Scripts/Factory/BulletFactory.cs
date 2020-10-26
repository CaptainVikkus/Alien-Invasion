using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletFactory : MonoBehaviour
{
    public GameObject bullet;
    public GameObject rocket;

    private Queue<GameObject> basicPool;
    private Queue<GameObject> rocketPool;

    public 
    // Start is called before the first frame update
    void Start()
    {
        basicPool = new Queue<GameObject>();
        rocketPool = new Queue<GameObject>();

        for (int count = 0; count < 20; count++)
        {
            var tempBullet = createBullet(BulletType.Basic);
            basicPool.Enqueue(tempBullet);
        }

        for (int count = 0; count < 20; count++)
        {
            var tempBullet = createBullet(BulletType.Rocket);
            rocketPool.Enqueue(tempBullet);
        }
    }

    private GameObject createBullet(BulletType bulletType)
    {
        var tempBullet = Instantiate(bullet);
        tempBullet.SetActive(false);
        tempBullet.GetComponent<BulletController>().manager = this;
        return tempBullet;
    }

    public GameObject getBullet(Transform transform, BulletType bulletType)
    {
        Queue<GameObject> pool;
        switch(bulletType)
        {
            case BulletType.Basic:
                pool = basicPool;
                break;
            case BulletType.Rocket:
                pool = rocketPool;
                break;
            default:
                pool = basicPool;
                break;
        }
        //Make sure a bullet is available
        if (pool.Count == 0)
        {
            var temp = createBullet(bulletType);
            pool.Enqueue(temp);
        }
        //Set Active and remove from pool
        var bullet = pool.Dequeue();
        bullet.SetActive(true);
        //set to location of request;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        return bullet;
    }

    public void returnBullet(GameObject bullet, BulletType bulletType)
    {
        Queue<GameObject> pool;
        switch (bulletType)
        {
            case BulletType.Basic:
                pool = basicPool;
                break;
            case BulletType.Rocket:
                pool = rocketPool;
                break;
            default:
                pool = basicPool;
                break;
        }
        //Set Inactive and return to pool
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
}
