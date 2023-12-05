using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeekFunction : MonoBehaviour
{
    [SerializeField] private float travelingSpeed = 2;
    private GameObject enemie;
    [SerializeField] private UnityEvent reachedTarget;
    [SerializeField] private float radius = 1.5f;

    private Rigidbody2D _rigidbody;
    private int position;

    private void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("enemie");
        float furthest = 0;
        Vector3 position = transform.position;
        foreach (GameObject go in gameObjects)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.magnitude;
            if (curDistance < radius && curDistance > furthest)
            {
                enemie = go;
                furthest = curDistance;
            }
        }

    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }   

    void Update()
    {
        if (enemie == null)
        {
            Destroy(gameObject);
            return;
        }
        var direction = enemie.transform.position - transform.position;
        direction.Normalize();

        _rigidbody.MovePosition(transform.position + direction * travelingSpeed * Time.deltaTime);
    }
}
