using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class EnemyHandler : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody player;
    public GameObject healthBarPrefab;

    public GameObject bulletPrefab;
    public Transform spawnPosition;
    public Transform gun;
    public float breakBetweenShots = 0.5f;

    public IMessage message;
    public int maxHP;

    float hp;
    float breakTime;
    float bulletSpeed;

    FollowingHealthBar healthBar;

    // Use this for initialization
    void Start()
    {
        breakTime = 0;
        hp = maxHP;
        InitHealthBar();
        bulletSpeed = bulletPrefab.GetComponent<Ammo>().speed;
    }

    void InitHealthBar()
    {
        GameObject bar = Instantiate<GameObject>(healthBarPrefab);
        healthBar = bar.GetComponent<FollowingHealthBar>();
        healthBar.Init(transform, player.transform, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        AimAtPlayer();

        if (breakTime <= 0)
        {
            Fire();
            breakTime = breakBetweenShots;
        }
        breakTime = (breakTime <= 0) ? 0 : breakTime - Time.deltaTime;


        //transform.LookAt(player.transform);
        //gun.LookAt(player.transform);
    }

    void AimAtPlayer()
    {
        Vector3 v = PredictPlayerPosition();
        Quaternion rotation = Quaternion.LookRotation(v - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 60);
        //transform.LookAt(v);
        v.y -= 0.5f;
        gun.LookAt(v);
    }


    Vector3 PredictPlayerPosition()
    {
        return player.position + player.velocity * bulletSpeed * Time.deltaTime;
    }

    void Fire()
    {
        GameObject bullet = Instantiate<GameObject>(bulletPrefab, spawnPosition.position, spawnPosition.rotation);
        bullet.GetComponent<Ammo>().shooter = gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ammo ammo;
        if (ammo = collision.gameObject.GetComponent<Ammo>())
        {
            TakeDamage(ammo.damage);
        }
    }

    void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            message.ReciveGameObject(gameObject);
            Destroy(gameObject);
        }
        else            
            healthBar.UpdateHP(hp);

    }
}
