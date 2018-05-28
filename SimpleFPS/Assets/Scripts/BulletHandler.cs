using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody))]
public class BulletHandler : MonoBehaviour, IBullet
{
    public float speed;

    Rigidbody rb;
    GameObject shooter;

    public void SetShooter(GameObject shooter)
    {
        this.shooter = shooter;
    }

    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    private void OnValidate()
    {
        speed = (speed <= 0) ? 1 : speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isShooterIsPlayer()) shooter.GetComponent<Shooting>().ConfirmHit();
        Destroy(gameObject);
    }

    private bool isShooterIsPlayer()
    {
        return shooter != null && shooter.name == "Player";
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
