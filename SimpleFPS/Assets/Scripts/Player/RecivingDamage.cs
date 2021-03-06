﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecivingDamage : MonoBehaviour
{

    public HealthBar healthBar;

    public GameSceneManangment manager;

    public int maxHP;

    float hp;

    // Use this for initialization
    void Start()
    {
        hp = maxHP;
        healthBar.Init(maxHP);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        print(hp);
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
            manager.ReciveGameObject(gameObject);
        }
        else
            healthBar.UpdateHP(hp);

    }
}
