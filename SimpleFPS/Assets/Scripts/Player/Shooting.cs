using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager;


public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPosition;
    public string ShootButtonName;
    public float breakBetweenShoots = 100;

    public int Accuracy
    {
        get { return hit / shootedBullets; }
    }

    private float breakTime;

    private int shootedBullets;
    private int hit;

    public void ConfirmHit()
    {
        ++hit;
    }
    // Use this for initialization
    void Start()
    {
        breakTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetButton(ShootButtonName) && breakTime <= 0)
        {
            Fire();
            breakTime = breakBetweenShoots;
        }
        breakTime = (breakTime <= 0) ? 0 : breakTime - Time.deltaTime;

    }

    private void Fire()
    {
        GameObject bullet = Instantiate<GameObject>(bulletPrefab, spawnPosition.position, spawnPosition.rotation);
        bullet.GetComponent<IBullet>().SetShooter(gameObject);
        shootedBullets++;
    }

    private void OnValidate()
    {
        if (breakBetweenShoots <= 0) breakBetweenShoots = 100;
    }
}
