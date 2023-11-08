using System.Collections;
using UnityEngine;

namespace StudioXP.Scripts.Components.Movements.Characters
{
    public class FollowTargetAuto : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private string tagToDetect;
        [SerializeField] private float maxDistance = 15;

        private GameObject target;
        private bool targetDetected = false;
        private float distanceFromTarget;

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.tag == tagToDetect &&  !targetDetected)
            {
                targetDetected = true;
                target = other.gameObject;
                StartCoroutine("FollowTarget");
            }
        }

        private IEnumerator FollowTarget()
        {
            while(targetDetected)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                distanceFromTarget = Vector3.Distance(target.transform.position, transform.position);
            
                if(distanceFromTarget > maxDistance)
                    targetDetected = false;

                yield return null;
            }
        }
    }
}
