using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Sprites;
using UnityEngine;
using UnityEngine.Serialization;

namespace StudioXP.Scripts.Components.Objects
{
    public class PlatformCollider : MonoBehaviour
    {
        [FormerlySerializedAs("platformSprite")]
        [ChildGameObjectsOnly]
        [Required("Ajoute le ExtendablePlatform qui va être géré par ce script")]
        [SerializeField] private ExtendableSprite extendableSprite;

        [LabelText("Collision à direction unique")]
        [SerializeField] private bool oneWayCollision;

        [LabelText("Arc (côté)")]
        [SerializeField] private float sideArc = 1;

        private BoxCollider2D _boxCollider;
        private PlatformEffector2D _platformEffector;
        
        private void Start()
        {
            _boxCollider = gameObject.GetComponent<BoxCollider2D>();
            
            if(!_boxCollider)
                _boxCollider = gameObject.AddComponent<BoxCollider2D>();

            if (oneWayCollision && !_platformEffector)
            {
                _platformEffector = gameObject.AddComponent<PlatformEffector2D>();
                _platformEffector.useOneWay = true;
                _platformEffector.sideArc = sideArc;
                _boxCollider.usedByEffector = true;
            }
            
            SetBounds(extendableSprite.ColliderBounds);
        }

        public void SetBounds(Rect bounds)
        {
            if(_boxCollider)
                _boxCollider.size = bounds.size;
        }
    }
}
