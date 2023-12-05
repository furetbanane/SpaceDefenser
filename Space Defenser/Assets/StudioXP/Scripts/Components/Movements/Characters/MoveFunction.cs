using System;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Handlers;
using StudioXP.Scripts.Game;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace StudioXP.Scripts.Components.Movements.Characters
{
    [RequireComponent(typeof(SpriteRendererHandler))]
    [RequireComponent(typeof(AnimatorHandler))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveFunction : MonoBehaviour
    {
        [LabelText("Vitesse")]
        [SerializeField] private float speed = 5;
        [SerializeField] private List<Vector3> points;
        [SerializeField] private float distancePoint = 0.01f;
        [SerializeField] private UnityEvent reachedEnd;
        // N'oublie pas de programmer la méthode SetSpeed(float speed) pour pouvoir changer cette valeur.
        
        private Rigidbody2D _rigidbody;
        private int position;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
           var point = points[position];
           var distance = Vector3.Distance(transform.position, point);
           if (distance <= distancePoint)
           {
               position ++;
               if (position == points.Count)
               {
                   reachedEnd.Invoke();
               }
           }

           if (position >= points.Count)
           {
               return;
           }

           var direction = point - transform.position;
           direction.Normalize();

           _rigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
           
        }
    }
}
