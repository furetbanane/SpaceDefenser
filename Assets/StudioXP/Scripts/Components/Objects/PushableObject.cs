using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Handlers;
using StudioXP.Scripts.Game;
using StudioXP.Scripts.Utils;
using UnityEngine;

namespace StudioXP.Scripts.Components.Objects
{
    [RequireComponent(typeof(ColliderHandler))]
    public class PushableObject : MonoBehaviour
    {
        [LabelText("Layer d'interaction")]
        [SerializeField] private LayerMask interactionLayer;
            
        private ColliderHandler _colliderHandler;
        
        private void Awake()
        {
            _colliderHandler = GetComponent<ColliderHandler>();
        }

        public bool CanMove(Direction direction)
        {
            var go = _colliderHandler.GetTouching(direction, interactionLayer);
            while (go != null && go == gameObject)
                go = Physics2DUtil.GetNextResult()?.gameObject;

            if (go == null) return true;
            
            var pushableCapabilities = go.GetComponent<PushableObject>();
            if (pushableCapabilities)
                return pushableCapabilities.CanMove(direction);

            return go == null;
        }
    }
}
