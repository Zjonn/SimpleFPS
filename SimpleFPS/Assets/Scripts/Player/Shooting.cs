﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager;


public class Shooting : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform spawnPosition;
    public string ShootButtonName;
    public float breakBetweenShoots = 100;

    private float breakTime;
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
        Instantiate<GameObject>(bulletPrefab, spawnPosition.position, transform.rotation);
    }

    private void OnValidate()
    {
        if (breakBetweenShoots <= 0) breakBetweenShoots = 100;
    }
}
