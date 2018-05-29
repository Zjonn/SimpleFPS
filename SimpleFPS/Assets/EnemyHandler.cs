using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class EnemyHandler : MonoBehaviour {

    public IMessage message;
    public int maxHP;

    float hp;
	// Use this for initialization
	void Start () {
        hp = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            message.DeadMessage(gameObject);
            GameObject.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ammo ammo;
        if (ammo = collision.gameObject.GetComponent<Ammo>())
        {
            TakeDamage(ammo.damage);
        }
    }
}
