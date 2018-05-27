using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
public class BulletHandler : MonoBehaviour
{
    public float speed;
    public float distanceToDestroy;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        print(transform.forward);
    }

    private void OnValidate()
    {
        speed = (speed <= 0) ? 1 : speed;
        distanceToDestroy = (distanceToDestroy <= 0) ? 1 : distanceToDestroy;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
