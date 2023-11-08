using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Components.Handlers
{
    /// <summary>
    /// Fonctionnalités supplémentaires pour le component Animator
    ///
    /// Visualisation des paramètres disponible dans l'animator.
    /// Méthode permettant la modification des paramètres.
    /// </summary>
    public class AnimatorHandler : MonoBehaviour
    {
        [ChildGameObjectsOnly]
        [SerializeField] private Animator animator;
        
        [LabelText("Paramètres")]
        [ShowIf("animator")]
        [ListDrawerSettings(IsReadOnly = true, ShowFoldout = true)]
        [SerializeField] private List<AnimationParameter> parameters;

        private readonly HashSet<int> _validHash = new HashSet<int>();
        private bool _hasAnimator;

        private void OnValidate()
        {
            parameters.Clear();
            if (!animator || !animator.isActiveAndEnabled)
                return;

            foreach (var parameter in animator.parameters)
            {
                parameters.Add(new AnimationParameter(parameter.name, parameter.type));
            }
        }

        /// <summary>
        /// Change la valeur d'un paramètre booléen de l'animator à true
        /// </summary>
        /// <param name="parameter">Nom du paramètre</param>
        public void SetAnimatorBoolTrue(string parameter)
        {
            var hash = GetAnimatorHash(parameter);
            if (!_hasAnimator || !_validHash.Contains(hash)) return;
            animator.SetBool(hash, true);
        }
        
        /// <summary>
        /// Change la valeur d'un paramètre booléen de l'animator à false
        /// </summary>
        /// <param name="parameter">Nom du paramètre</param>
        public void SetAnimatorBoolFalse(string parameter)
        {
            var hash = GetAnimatorHash(parameter);
            if (!_hasAnimator || !_validHash.Contains(hash)) return;
            animator.SetBool(hash, false);
        }

        /// <summary>
        /// Change la valeur d'un paramètre booléen de l'animator
        /// </summary>
        /// <param name="hash">Hash du nom du paramètre</param>
        /// <param name="value">Valeur à définir</param>
        public void SetAnimatorBool(int hash, bool value)
        {
            if (!_hasAnimator || !_validHash.Contains(hash)) return;
            animator.SetBool(hash, value);
        }
    
        /// <summary>
        /// Change la valeur d'un paramètre float de l'animator
        /// </summary>
        /// <param name="hash">Hash du nom du paramètre</param>
        /// <param name="value">Valeur à définir</param>
        public void SetAnimatorFloat(int hash, float value)
        {
            if (!_hasAnimator || !_validHash.Contains(hash)) return;
            animator.SetFloat(hash, value);
        }
    
        /// <summary>
        /// Change la valeur d'un paramètre int de l'animator
        /// </summary>
        /// <param name="hash">Hash du nom du paramètre</param>
        /// <param name="value">Valeur à définir</param>
        public void SetAnimatorInteger(int hash, int value)
        {
            if (!_hasAnimator || !_validHash.Contains(hash)) return;
            animator.SetInteger(hash, value);
        }
    
        /// <summary>
        /// Active un trigger de l'animator
        /// </summary>
        /// <param name="hash">Hash du nom du trigger</param>
        public void SetAnimatorTrigger(int hash)
        {
            if (!_hasAnimator || !_validHash.Contains(hash)) return;
            animator.SetTrigger(hash);
        }
        
        /// <summary>
        /// Active un trigger de l'animator
        /// </summary>
        /// <param name="parameter">Nom du trigger</param>
        public void SetAnimatorTrigger(string parameter)
        {
            var hash = GetAnimatorHash(parameter);
            if (!_hasAnimator || !_validHash.Contains(hash)) return;
            animator.SetTrigger(hash);
        }

        /// <summary>
        /// Calcule le hash d'un nom de paramètre
        /// </summary>
        /// <param name="parameterName">Nom du paramètre</param>
        /// <returns>Hash du nom du paramètre</returns>
        public static int GetAnimatorHash(string parameterName)
        {
            return Animator.StringToHash(parameterName);
        }
    
        private void Awake()
        {
            _hasAnimator = animator != null;
            if (!_hasAnimator) return;
            
            foreach (var parameter in animator.parameters)
                _validHash.Add(parameter.nameHash);
        }
    }
}
