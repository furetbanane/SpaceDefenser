using UnityEngine;

namespace StudioXP.Scripts.Components.Movements.Simple
{
    public class MoveLinear : MonoBehaviour
    {
        [SerializeField] private Vector2 speed;

        private Vector3 _initialPosition;

        public void Reset()
        {
            transform.position = _initialPosition;
        }

        private void Start()
        {
            _initialPosition = transform.position;
        }

        void Update()
        {
            transform.position += (Vector3)(speed * Time.deltaTime);
        }
    }
}
