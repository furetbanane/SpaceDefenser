using Sirenix.OdinInspector;
using StudioXP.Scripts.Events;
using StudioXP.Scripts.Game;
using UnityEngine;

namespace StudioXP.Scripts.Components.Handlers
{
    /// <summary>
    /// Ajoute des fonctionnalités au component SpriteRenderer.
    /// 
    /// Gère un système d'évènement pour detecter le changement de direction de l'objet.
    /// Lorsque le sprite change de direction horizontalement ou verticalement, un évènement est envoyé.
    /// </summary>
    public class SpriteRendererHandler : MonoBehaviour
    {
        [ChildGameObjectsOnly]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [LabelText("Direction de départ horizontal")]
        [SerializeField] private DirectionHorizontal startingHorizontalFacing;
        [LabelText("Direction de départ Vertical")]
        [SerializeField] private DirectionVertical startingVerticalFacing;

        [FoldoutGroup("Évènements", false)] [SerializeField]
        [LabelText("Directions par défaut changées")]
        private DirectionHVEvent defaultFacingChanged;

        [FoldoutGroup("Évènements")] 
        [LabelText("Direction horizontale changée")]
        [SerializeField] private DirectionHEvent horizontalFacingChanged;
        
        [FoldoutGroup("Évènements")] 
        [LabelText("Direction verticale changée")]
        [SerializeField] private DirectionVEvent verticalFacingChanged;
        
        private DirectionHorizontal _horizontalFacing;
        private DirectionVertical _verticalFacing;
        private bool _hasSpriteRenderer;
        private bool _defaultSpriteFlipX;
        private bool _defaultSpriteFlipY;
        
        public DirectionHorizontal HorizontalFacing
        {
            get => _horizontalFacing;
            set
            {
                if (!_hasSpriteRenderer) return;
                _horizontalFacing = value;
            
                spriteRenderer.flipX = (_horizontalFacing == startingHorizontalFacing)
                    ? _defaultSpriteFlipX
                    : !_defaultSpriteFlipX;
                
                horizontalFacingChanged.Invoke(_horizontalFacing);
            }
        }
    
        public DirectionVertical VerticalFacing
        {
            get => _verticalFacing;
            set
            {
                if (!_hasSpriteRenderer) return;
                _verticalFacing = value;
            
                spriteRenderer.flipY = (_verticalFacing == startingVerticalFacing)
                    ? _defaultSpriteFlipY
                    : !_defaultSpriteFlipY;
                
                verticalFacingChanged.Invoke(_verticalFacing);
            }
        }
        
        /// <summary>
        /// Met le sprite transparent
        /// </summary>
        public void MakeTransparent()
        {
            if (!_hasSpriteRenderer)
                return;
        
            var color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;
        }

        /// <summary>
        /// Met le sprite opaque
        /// </summary>
        public void MakeOpaque()
        {
            if (!_hasSpriteRenderer)
                return;
        
            var color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
        
        void Awake()
        {
            _hasSpriteRenderer = spriteRenderer != null;
            _horizontalFacing = startingHorizontalFacing;
            _verticalFacing = startingVerticalFacing;
            if (!_hasSpriteRenderer) return;
            _defaultSpriteFlipX = spriteRenderer.flipX;
            _defaultSpriteFlipY = spriteRenderer.flipY;
        }

        private void Start()
        {
            defaultFacingChanged.Invoke(_horizontalFacing, _verticalFacing);
        }
    }
}
