using System.Collections;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Events;
using StudioXP.Scripts.Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace StudioXP.Scripts.Components.Movements.Camera
{
    public class FollowTargetSmoothAuto : MonoBehaviour
    {
        [LabelText("Cible (Tag)")]
        [ValueDropdown("GetTagsView")]
        [SerializeField] private string targetTag;
        
        [LabelText("Décalage de la cible")]
        [SerializeField] private Vector2 targetOffset;
        
        [FormerlySerializedAs("velocityFactor")]
        [LabelText("Multiplicateur de vitesse")]
        [SerializeField] private float velocityMultiplier = 1;
        
        [FoldoutGroup("Évènements", false)]
        [LabelText("La position a changée")]
        [SerializeField] private Vector2Event positionChanged;

        private Transform _targetTransform;
        private Vector2 _velocity = Vector2.zero;
        private Vector3 _lastPosition = Vector2.zero;

        private RestrictionBounds _bounds;

        private void Awake()
        {
            _bounds = GetComponent<RestrictionBounds>();
            _targetTransform = GameObject.FindWithTag(targetTag).transform;
        }

        void FixedUpdate()
        {
            var selfTransform = transform;
            var position = selfTransform.position;
            
            var posTarget = _targetTransform.position;
            var distanceX = (posTarget.x + targetOffset.x) - position.x;
            var distanceY = (posTarget.y + targetOffset.y) - position.y;

            _lastPosition = position;
            var distance = new Vector2(distanceX, distanceY);
            if (distance.magnitude < float.Epsilon * 2)
                return;
            
            _velocity = distance * velocityMultiplier;
            position += (Vector3)_velocity * Time.deltaTime;
            if (_bounds)
                position = _bounds.GetBoundedPosition(position);
            
            positionChanged.Invoke(position);
            selfTransform.position = position;
        }
        
#if UNITY_EDITOR
        private IEnumerable GetTagsView()
        {
            return UnityEditorInternal.InternalEditorUtility.tags;
        }
#endif
    }
}