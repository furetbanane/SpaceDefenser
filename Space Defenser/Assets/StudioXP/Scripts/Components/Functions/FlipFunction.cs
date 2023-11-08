using Sirenix.OdinInspector;
using StudioXP.Scripts.Events;
using StudioXP.Scripts.Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// Gère le flip vertical et horizontal d'un objet local à un autre. Ne fonctionne pas sur un objet placé à la racine.
    /// Le flip a deux modes indépendants.
    /// Le flip de l'offset: Si le flip est horizontale et que l'objet est à la
    /// position locale 1.5, sa position sera -1.5 après le flip.
    /// Le flip du scale. Le flip inversera le scale entre positif et négatif en X ou en Y selon l'orientation du flip.
    /// </summary>
    public class FlipFunction : SXPMonobehaviour
    {
        /// <summary>
        /// Si vrai, le mode sera FlipScale, sinon FlipOffset.
        /// </summary>
        [FormerlySerializedAs("_flipScale")] [SerializeField] private bool flipScale = false;
        [FoldoutGroup("Évènements",false)]
        [LabelText("Retournement Horizontal")]
        [SerializeField] private BoolEvent onHorizontalFlip;
        [FoldoutGroup("Évènements")]
        [LabelText("Retournement Vertical")]
        [SerializeField] private BoolEvent onVerticalFlip;
        
        private DirectionHorizontal _defaultHorizontalFacing = DirectionHorizontal.Left;
        private DirectionVertical _defaultVerticalFacing = DirectionVertical.Up;
        
        private DirectionHorizontal _currentHorizontalFacing = DirectionHorizontal.Left;
        private DirectionVertical _currentVerticalFacing = DirectionVertical.Up;

        private Vector2 _defaultOffset;
        
        /// <summary>
        /// Défini l'orientation par défaut. Important à exécuter au début du jeu pour définir l'orientation par défaut
        /// de l'objet. Doit être l'orientation visuel de l'objet avant que le jeu démarre.
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="vertical"></param>
        public void SetDefaultFacing(DirectionHorizontal horizontal, DirectionVertical vertical)
        {
            _defaultHorizontalFacing = horizontal;
            _defaultVerticalFacing = vertical;
            _currentHorizontalFacing = horizontal;
            _currentVerticalFacing = vertical;
        }

        /// <summary>
        /// Change l'orientation horizontale
        /// </summary>
        /// <param name="horizontal"></param>
        public void SetHorizontalFacing(DirectionHorizontal horizontal)
        {
            if (_currentHorizontalFacing == horizontal) return;
            
            _currentHorizontalFacing = horizontal;
            var offset = _defaultOffset;
            if (horizontal != _defaultHorizontalFacing)
            {
                if (flipScale)
                {
                    var localScale = transform.localScale;
                    localScale.x *= -1;
                    transform.localScale = localScale;
                }
                else
                    offset.x = -_defaultOffset.x;
                onHorizontalFlip.Invoke(true);
            }
            else
                onHorizontalFlip.Invoke(false);

            if (flipScale)
                return;

            var selfTransform = transform;
            Vector2 position = selfTransform.position;
            position.x -= offset.x * 2;
            selfTransform.position = position;
            selfTransform.localPosition = offset;
        }

        /// <summary>
        /// Change l'orientation verticale
        /// </summary>
        /// <param name="vertical"></param>
        public void SetVerticalFacing(DirectionVertical vertical)
        {
            if (_currentVerticalFacing == vertical) return;
            
            _currentVerticalFacing = vertical;
            var offset = _defaultOffset;
            if (vertical != _defaultVerticalFacing)
            {
                if (flipScale)
                {
                    var localScale = transform.localScale;
                    localScale.y *= -1;
                    transform.localScale = localScale;
                }
                else
                    offset.y = -_defaultOffset.y;
                onVerticalFlip.Invoke(true);
            }
            else
                onVerticalFlip.Invoke(false);
            
            if (flipScale)
                return;
            
            var selfTransform = transform;
            Vector2 position = selfTransform.position;
            position.y -= offset.y * 2;
            selfTransform.position = position;
            selfTransform.localPosition = offset;
        }

        private void Awake()
        {
            _defaultOffset = transform.localPosition;
        }
    }
}
