using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo : MonoBehaviour {

    public float damage;
    public float speed;
    [HideInInspector]
    public GameObject shooter;

}
