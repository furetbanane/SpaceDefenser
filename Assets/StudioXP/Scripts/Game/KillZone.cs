using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Functions;
using UnityEngine;

namespace StudioXP.Scripts.Game
{
    public class KillZone : MonoBehaviour
    {
        [LabelText("Bornes de la zone sécuritaire")]
        [SerializeField] private Rect safeBounds;
        
        [LabelText("Bornes locales")]
        [SerializeField] private bool relativeBounds;
        
        [LabelText("Cibles (Tag)")]
        [ValueDropdown("GetTagsView")]
        [SerializeField] private string targetTag;

        [LabelText("Couleur du Gizmo")] [SerializeField]
        private Color gizmoColor = new Color(250, 92, 71);

        private readonly List<CounterFunction> _killList = new List<CounterFunction>();

        private void Awake()
        {
            foreach (var go in GameObject.FindGameObjectsWithTag(targetTag))
            {
                var counter = go.GetComponent<CounterFunction>();
                if(counter)
                    _killList.Add(counter);
            }
        }

        private void FixedUpdate()
        {
            if (relativeBounds)
                safeBounds.center = transform.position;
                
            foreach (var target in _killList)
            {
                var position = target.transform.position;

                if (position.x < safeBounds.xMin || position.x > safeBounds.xMax ||
                    position.y < safeBounds.yMin || position.y > safeBounds.yMax)
                {
                    target.SetToMinimum();
                }
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = gizmoColor;
            var bounds = safeBounds;
            if (relativeBounds)
                bounds.center = transform.position;

            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
        
#if UNITY_EDITOR
        private IEnumerable GetTagsView()
        {
            return UnityEditorInternal.InternalEditorUtility.tags;
        }
#endif
    }
}
