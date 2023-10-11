using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// La seule méthode disponible est ToggleAction. Lorsque ToggleAction est appelé l'évènement onAction sera toujours lancé.
    /// Cependant, l'état du toggle est défini par Actif ou Inactif. Lorqu'on appel toggle, on change entre Actif et Inactif.
    /// Lorsque le Toggle devient Actif, l'évènement onActivation est lancés. S'il devient inactif, c'est onDeactivation
    /// qui est lancé.
    /// </summary>
    public class ToggleFunction : MonoBehaviour
    {
        [SerializeField] private UnityEvent onActivation;
        [SerializeField] private UnityEvent onDeactivation;
        [SerializeField] private UnityEvent onAction;
        [SerializeField] private bool isActiveOnStart;
        
        private bool _isActive = false;
        
        private void Start()
        {
            _isActive = isActiveOnStart;
            InvokeEvents();
        }

        /// <summary>
        /// Effectue l'action de Toggle
        /// </summary>
        public void ToggleAction()
        {
            _isActive = !_isActive;
            InvokeEvents();
        }

        private void InvokeEvents()
        {
            if(_isActive)
            {
                onActivation.Invoke();
            }
            else
            {
                onDeactivation.Invoke();
            }

            onAction.Invoke();
        }
    }
}
