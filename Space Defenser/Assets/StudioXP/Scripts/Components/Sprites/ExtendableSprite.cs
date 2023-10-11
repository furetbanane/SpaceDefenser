using System;
using System.Collections;
using System.Numerics;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Events;
using StudioXP.Scripts.Game;
using StudioXP.Scripts.Utils;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace StudioXP.Scripts.Components.Sprites
{
    [ExecuteInEditMode]
    public class ExtendableSprite : MonoBehaviour
    {
        [LabelText("Grandeur")]
        [Range(1, 20)] [SerializeField] private int size = 1;

        [SerializeField] private Orientation orientation = Orientation.Horizontal;
        
        [FoldoutGroup("Sprites", false)]
        [LabelText("Négatif")]
        [Required("Ajoute le sprite qui sera placé du côté négatif de l'orientation")]
        [AssetSelector(Paths = "Assets/Etudiant/Sprites|Assets/StudioXP/Sprites", ExpandAllMenuItems = false)]
        [SerializeField] private SpriteRenderer negativeSprite;
        
        [FoldoutGroup("Sprites")]
        [LabelText("Centre")]
        [Required("Ajoute le sprite qui sera placé au centre")]
        [AssetSelector(Paths = "Assets/Etudiant/Sprites|Assets/StudioXP/Sprites", ExpandAllMenuItems = false)]
        [SerializeField] private SpriteRenderer centerSprite;
        
        [FoldoutGroup("Sprites")]
        [LabelText("Positif")]
        [Required("Ajoute le sprite qui sera placé du côté positif de l'orientation")]
        [AssetSelector(Paths = "Assets/Etudiant/Sprites|Assets/StudioXP/Sprites", ExpandAllMenuItems = false)]
        [SerializeField] private SpriteRenderer positiveSprite;
        
        public Rect ColliderBounds { get; private set; }

        private void Awake()
        {
            var isHorizontal = orientation == Orientation.Horizontal;
            var centerBounds = centerSprite.sprite.bounds;
            var xColliderBound = isHorizontal ? (size + 1) * centerBounds.size.x : centerBounds.size.x;
            var yColliderBound = isHorizontal ? centerBounds.size.y : (size + 1) * centerBounds.size.y;
            ColliderBounds = new Rect(Vector2.zero, new Vector2(xColliderBound, yColliderBound));
        }

        private void UpdateSprites()
        {
            if (this == null)
                return;
            
            if (Application.isPlaying) return;

            var centerBounds = centerSprite.sprite.bounds;

            var isHorizontal = orientation == Orientation.Horizontal;
            var centerExtents = isHorizontal ? centerBounds.extents.x : centerBounds.extents.y;
            var extremities = centerExtents * size;

            var negativeTransform = negativeSprite.transform;
            var centerTransform = centerSprite.transform;
            var positiveTransform = positiveSprite.transform;
            
            centerTransform.localPosition = Vector3.zero;
            negativeTransform.localPosition = (isHorizontal ? Vector3.left : Vector3.down) * extremities;
            positiveTransform.localPosition = (isHorizontal ? Vector3.right : Vector3.up) * extremities;
            
            centerSprite.gameObject.SetActive(size > 1);

            var centerScale = (Vector2)(isHorizontal ? Vector3.right : Vector3.up) * (size - 1);
            if (centerScale.x == 0)
                centerScale.x = 1;
            else if (centerScale.y == 0)
                centerScale.y = 1;
            
            centerSprite.size = centerScale;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (EditorApplication.isPlaying) return;
            if (EditorApplication.isUpdating) return;
            EditorApplication.delayCall += UpdateSprites; 
        }
#endif
    }
}
