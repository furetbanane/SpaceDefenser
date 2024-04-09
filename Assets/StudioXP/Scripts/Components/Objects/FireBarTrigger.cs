using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Sprites;
using UnityEngine;

namespace StudioXP.Scripts.Components.Objects
{
    public class FireBarTrigger : MonoBehaviour
    {
        [ChildGameObjectsOnly]
        [Required("Ajoute le ExtendableSprite qui va être géré par ce script")]
        [SerializeField] private ExtendableSprite fireBarSprite;

        private BoxCollider2D _boxCollider;
        
        private void Start()
        {
            _boxCollider = gameObject.GetComponent<BoxCollider2D>();
            
            if(!_boxCollider)
                _boxCollider = gameObject.AddComponent<BoxCollider2D>();

            _boxCollider.isTrigger = true;
            
            SetBounds(fireBarSprite.ColliderBounds);
        }

        public void SetBounds(Rect bounds)
        {
            if (!_boxCollider) return;
            _boxCollider.size = bounds.size;
        }
    }
}
