using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeekFunction : MonoBehaviour
{
    [SerializeField] private float travelingSpeed = 2;
    [SerializeField] private GameObject enemie;
    [SerializeField] private UnityEvent reachedTarget;

    private Rigidbody2D _rigidbody;
    private int position;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }   

    void Update()
    {
        var direction = enemie.transform.position - transform.position;
        direction.Normalize();

        _rigidbody.MovePosition(transform.position + direction * travelingSpeed * Time.deltaTime);
    }
}
