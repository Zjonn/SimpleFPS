using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecivingDamage : MonoBehaviour {

    public HealthBar healthBar;

    public int maxHP;

    float hp;

	// Use this for initialization
	void Start () {
        hp = maxHP;
        healthBar.Init(maxHP);
	}
	
	// Update is called once per frame
	void Update () {
		
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
            Destroy(healthBar.gameObject);
            Destroy(gameObject);
        }
        else
            healthBar.UpdateHP(hp);
    }
}
