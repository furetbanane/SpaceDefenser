using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// ChangePositionFunction change la position de l'objet. La nouvelle position sera multiplié par le multiplicateur
    /// avant d'être définie.
    /// </summary>
    public class ChangePositionFunction : SXPMonobehaviour
    {
        [LabelText("Multiplicateur")]
        [SerializeField] private Vector2 multiplier;

        /// <summary>
        /// Change la position de l'objet à position * multiplier.
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(Vector2 position)
        {
            transform.position = position * multiplier;
        }
    }
}
