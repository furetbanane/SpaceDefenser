using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemie : MonoBehaviour
{
    [SerializeField] private int attackForce = 1;

    gameObject.AddComponent<BoxCollider2D>();

    void OnCollisionEnter2D(Collision2D col)
    {
        health.Decrease(attackForce);
    }
}
