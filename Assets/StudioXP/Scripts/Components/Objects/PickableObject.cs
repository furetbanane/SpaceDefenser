using UnityEngine;

namespace StudioXP.Scripts.Components.Objects
{
    public class PickableObject : MonoBehaviour
    {
        private Collider2D[] _colliders;
        private Rigidbody2D _rigidbody;

        private Transform _selfTransform;
        private Transform _originalParent;
        private RigidbodyConstraints2D _originalConstraints2D;
        private bool pickableIsEnabled = true;

        private void Awake()
        {
            _selfTransform = transform;
            _colliders = GetComponents<Collider2D>();
            if(_colliders.Length > 0)
                _rigidbody = _colliders[0].attachedRigidbody;
        }

        public void Pickup(GameObject parent)
        {
            if(pickableIsEnabled)
            {
            _originalParent = _selfTransform.parent;
            _selfTransform.parent = parent.transform;
            _selfTransform.localPosition = Vector2.zero;
            
            if (_rigidbody)
            {
                _originalConstraints2D = _rigidbody.constraints;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                _rigidbody.isKinematic = true;
            }

            foreach(var col in _colliders)
                col.enabled = false;
            }
        }

        public void Throw(Vector2 force)
        {
            Drop();
            _rigidbody.velocity = force;
        }

        public void Drop()
        {
            _selfTransform.parent = _originalParent;
            _originalParent = null;
            if (_rigidbody)
            {
                _rigidbody.constraints = _originalConstraints2D;
                _rigidbody.isKinematic = false;
            }

            foreach(var col in _colliders)
                col.enabled = true;
        }

        public void SetPickableIsEnabled(bool _trueOrFalse)
        {
            pickableIsEnabled = _trueOrFalse;
        }
    }
}
