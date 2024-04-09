using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5 ;
    [SerializeField] private UnityEvent healthDecreased;
    [SerializeField] private UnityEvent healthReachedMin;

    private int _health;
    public int Value => _health;

    private void Awake()
    {
        _health = maxHealth;
    }

    public void Decrease(int damage)
    {
        _health -= damage;
        healthDecreased.Invoke();

        if(_health <= 0)
            //EnemySpawner.onEnemyDestroy.Invoke();
            healthReachedMin.Invoke();
    }
}
