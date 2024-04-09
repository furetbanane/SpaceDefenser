using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Visuals
{
    public class LoopingBackground : MonoBehaviour
    {
        [ChildGameObjectsOnly]
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        [LabelText("Camera (Tag)")]
        [ValueDropdown("GetTagsView")]
        [SerializeField] private string cameraTag;
        
        private Bounds _bounds;
        private Transform _left;
        private Transform _right;
        private Transform _cameraTransform;

        private void Awake()
        {
            _cameraTransform = GameObject.FindWithTag(cameraTag).transform;
        }

        void Start()
        {
            _bounds = spriteRenderer.bounds;
            _left = spriteRenderer.transform;
            var sr = Instantiate(spriteRenderer, transform);
            _right = sr.transform;
            _left.transform.localPosition = new Vector2(-_bounds.extents.x, 0);
            _right.transform.localPosition = new Vector2(_bounds.extents.x - 0.1f, 0);
        }

        void Update()
        {
            var cameraPosition = _cameraTransform.position;
            var relativeRight = _right.position - cameraPosition;
            var relativeLeft = _left.position - cameraPosition;
            
            if (relativeRight.x < 0)
            {
                var newRight = _left;
                _left = _right;
                _right = newRight;

                _right.localPosition = new Vector2(_left.localPosition.x + _bounds.size.x - 0.1f, 0);
            }

            if (relativeLeft.x > 0)
            {
                var newLeft = _right;
                _right = _left;
                _left = newLeft;

                _left.localPosition = new Vector2(_right.localPosition.x - _bounds.size.x + 0.1f, 0);
            }
        }
#if UNITY_EDITOR
        private IEnumerable GetTagsView()
        {
            return UnityEditorInternal.InternalEditorUtility.tags;
        }
#endif
    }
}
