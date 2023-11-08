using UnityEngine;

namespace StudioXP.Scripts.Visuals
{
    public class ParallaxOffset : MonoBehaviour
    {
        private Transform _parent;
        private Vector3 _parentInitialPos;
        
        private void Start()
        {
            _parent = transform.parent;
            _parentInitialPos = _parent.position;
        }

        private void Update()
        {
            var diff = _parent.position - _parentInitialPos;
            diff.y = 0;
            diff.z = 0;
            transform.localPosition = -diff;
        }
    }
}
