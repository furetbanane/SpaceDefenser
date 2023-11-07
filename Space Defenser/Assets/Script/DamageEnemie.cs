using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemie : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Health health = col.gameObject.GetComponent<Health>();

        if(health != null)
        {
            health.Decrease(health.Value);
            Destroy(gameObject);
        }
    }
}
