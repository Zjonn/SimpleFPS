using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class EnemyHandler : MonoBehaviour {

    public IMessage message;
    public int maxHP;

    public float HP
    {
        get;
        private set;
    }
	// Use this for initialization
	void Start () {
        HP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TakeDamage(float damage)
    {
        HP -= damage;
        if(HP <= 0)
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
