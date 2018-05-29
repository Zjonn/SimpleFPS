using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager;


public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPosition;
    public string shootButtonName;
    public float breakBetweenShots = 0.5f;

    public float Accuracy
    {
        get
        {
            return (shootedBullets == 0) ? 0 : Mathf.Round(hit / (float)shootedBullets * 100);
        }
    }

    float breakTime;

    int shootedBullets;
    int hit;

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
        if (InputManager.GetButton(shootButtonName) && breakTime <= 0)
        {
            Fire();
            breakTime = breakBetweenShots;
        }
        breakTime = (breakTime <= 0) ? 0 : breakTime - Time.deltaTime;
    }

    private void Fire()
    {
        GameObject bullet = Instantiate<GameObject>(bulletPrefab, spawnPosition.position, spawnPosition.rotation);
        bullet.GetComponent<Ammo>().shooter = gameObject;
        shootedBullets++;
    }

    private void OnValidate()
    {
        if (breakBetweenShots <= 0) breakBetweenShots = 100;
    }
}
