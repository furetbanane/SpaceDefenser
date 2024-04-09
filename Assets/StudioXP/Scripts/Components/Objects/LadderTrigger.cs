using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Sprites;
using UnityEngine;

namespace StudioXP.Scripts.Components.Objects
{
    public class LadderTrigger : MonoBehaviour
    {
        [ChildGameObjectsOnly]
        [Required("Ajoute le ExtendableSprite qui va être géré par ce script")]
        [SerializeField] private ExtendableSprite ladderSprite;

        private BoxCollider2D _boxCollider;
        
        private void Start()
        {
            _boxCollider = gameObject.GetComponent<BoxCollider2D>();
            
            if(!_boxCollider)
                _boxCollider = gameObject.AddComponent<BoxCollider2D>();

            _boxCollider.isTrigger = true;
            
            SetBounds(ladderSprite.ColliderBounds);
        }

        public void SetBounds(Rect bounds)
        {
            if (!_boxCollider) return;
            _boxCollider.size = bounds.size;
        }
    }
}
