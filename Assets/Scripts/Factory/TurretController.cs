using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TurretController : MonoBehaviour
{
    private BulletFactory bulletFactory;
    private PlayerController player;
    [SerializeField]
    private float fireRate = 1;
    private float cooldown = 0;

    [SerializeField]
    private AudioClip fireSound;
    [SerializeField]
    private AudioClip dmgSound;

    public bool autoFire = false;
    public float range = 10;
    public BulletType ammoType = BulletType.Basic;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        Assert.IsNotNull(player, "Player not found");
        //Add to list of player contolled turrets
        player.turretList.Add(this);

        bulletFactory = FindObjectOfType<BulletFactory>();
        Assert.IsNotNull(bulletFactory, "Bullets not found");
    }

    // Update is called once per frame
    void Update()
    {
        //Cooldown
        if (cooldown < fireRate)
        {
            cooldown += Time.deltaTime;
        }
        //Fire if Cooldown complete and autofire on
        else if (autoFire)
        {
            //Find Target
            var target = FindTarget();
            if (target != Vector2.zero) 
            { 
                Fire(target);
            }
        }
        if (GetComponent<Health>().health <= 0)
        {
            AudioSource.PlayClipAtPoint(dmgSound, transform.position);
            player.turretList.Remove(this);
            Destroy(this.gameObject);
        }
    }

    private Vector2 FindTarget()
    {
        Vector2 target = Vector2.zero;
        //Get list of Enemies
        foreach (EnemyController enemy in FindObjectsOfType<EnemyController>())
        {
            //Check if in Range
            if (Vector2.Distance(enemy.transform.position, transform.position) < range)
            {
                //Set Target Vector and Rotate
                target = enemy.transform.position;
            }
        }
        return target;
    }

    public void Fire(Vector2 target)
    {
        //Check if firing is available
        if (cooldown >= fireRate)
        {
            //Rotate turret to face
            transform.up = (Vector3)target - transform.position;
            //SpawnBullet
            bulletFactory.getBullet(transform, ammoType);
            cooldown = 0;
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
        }
    }
}
