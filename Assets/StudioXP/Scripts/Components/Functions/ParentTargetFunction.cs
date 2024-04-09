using System.Collections;
using System.Collections.Generic;
using StudioXP.Scripts.Events;
using StudioXP.Scripts.Utils;
using UnityEngine;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// Permet à un objet de transformer un objet avec lequel il entre en collision en enfant. Peux ensuite détacher
    /// l'objet enfant de soit même en le remettant à la racine de la scène.
    /// </summary>
    public class ParentTargetFunction : SXPMonobehaviour
    {
        private readonly Dictionary<GameObject, Transform> _previousParent = new ();
        private readonly Dictionary<GameObject, Quaternion> _previousRotation = new ();

        /// <summary>
        /// Prend l'objet info.GameObject et en fait un enfant
        /// </summary>
        /// <param name="info"></param>
        public void ParentTarget(CollisionInfo info)
        {
            if (gameObject.activeSelf == false) return;
            
            ParentTarget(info.GameObject);
        }
        
        /// <summary>
        /// Remet l'objet info.gameObject à la racine de la scène.
        /// </summary>
        /// <param name="info"></param>
        public void UnparentTarget(CollisionInfo info)
        {
            if (gameObject.activeSelf == false) return;
            
            UnparentTarget(info.GameObject);
        }
        
        /// <summary>
        /// Prend l'objet go et en fait un enfant
        /// </summary>
        /// <param name="go"></param>
        public void ParentTarget(GameObject go)
        {
            var otherTransform = go.transform;
            if (otherTransform.parent == transform)
                return;

            if (_previousParent.ContainsKey(go))
                return;
            
            _previousParent.Add(go, otherTransform.parent);
            _previousRotation.Add(go, go.transform.rotation);
            otherTransform.parent = transform;
        }

        /// <summary>
        /// Remet l'objet go à la racine de la scène.
        /// </summary>
        /// <param name="info"></param>
        public void UnparentTarget(GameObject go)
        {
            if (_previousParent.ContainsKey(go))
            {
                if(go.transform.parent == transform)
                    go.transform.parent = _previousParent[go];
                
                _previousParent.Remove(go);
                _previousRotation.Remove(go);
            }
            else
            {
                go.transform.parent = null;
            }
        }

        /// <summary>
        /// Détache tous les enfants
        /// </summary>
        private void UnparentAll()
        {
            foreach (var go in _previousParent.Keys)
                go.transform.parent = _previousParent[go];
            
            _previousParent.Clear();
            _previousRotation.Clear();
        }

        private IEnumerator UnparentAllCoroutine()
        {
            yield return new WaitForEndOfFrame();
            UnparentAll();
        }

        private void Update()
        {
            foreach (var go in _previousParent.Keys)
                go.transform.rotation = _previousRotation[go];
        }

        private void OnDisable()
        {
            if(CoroutineInvoker.Instance != null)
                CoroutineInvoker.Instance.StartCoroutine(UnparentAllCoroutine());
        }

        private void OnDestroy()
        {
            UnparentAll();
        }

        private void OnDrawGizmos()
        {
            if (transform.hasChanged)
                transform.localScale = Vector3.one;
        }
    }
}
