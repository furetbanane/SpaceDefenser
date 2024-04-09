using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Components
{
    /// <summary>
    /// Permet d'insérer un commentaire au début d'un component dans l'inspecteur. Le commentaire est seulement à
    /// titre informatif et n'a aucune fonctionnalités.
    /// </summary>
    public class SXPMonobehaviour : MonoBehaviour
    {
        [FoldoutGroup("Info")]
        [LabelText("Commentaires"), TextArea, SerializeField] private string comments;
    }
}
