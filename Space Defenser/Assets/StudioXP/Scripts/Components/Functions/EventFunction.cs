using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// EventFunction permet d'exécuter une action de type UnityEvent définie dans l'inspecteur
    ///
    /// L'évènement s'exécute lorsqu'on appel la méthode Execute.
    /// </summary>
    public class EventFunction : SXPMonobehaviour
    {
        [SerializeField] private UnityEvent action;
        
        /// <summary>
        /// Exécute l'évènement action.
        /// </summary>
        public void Execute()
        {
            action.Invoke();
        }
    }
}
