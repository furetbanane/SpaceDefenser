using Sirenix.OdinInspector;
using StudioXP.Scripts.Events;
using UnityEngine;
// ReSharper disable NotAccessedField.Local

namespace StudioXP.Scripts.Components.Common
{
    /// <summary>
    /// Permet de transférer des informations suite à la collision. Les types suivants sont possibles mais pas nécessairement
    /// défini pour toutes les collisions. Cela est contrôlé par l'évènement unity qui lance l'information.
    ///
    /// Types disponibles : void, float, int, bool, Vector2, Vector3, GameObject.
    /// </summary>
    public class CollisionInfoValue : SXPMonobehaviour
    {
        public enum SendFunctionType
        {
            Void,
            Float,
            Int,
            Bool,
            Vector2,
            Vector3,
            GameObject
        }
        
        [PropertyTooltip("Type de valeur à envoyer")]
        [SerializeField] private SendFunctionType type = SendFunctionType.Void;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.Float)] private float valueFloat;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.Int)] private int valueInt;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.Bool)] private bool valueBool;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.Vector2)] private Vector2 valueVector2;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.Vector3)] private Vector3 valueVector3;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.GameObject)] private GameObject valueGameObject;

        public float ValueFloat => valueFloat;
        public int ValueInt => valueInt;
        public bool ValueBool => valueBool;
        public Vector2 ValueVector2 => valueVector2;
        public Vector3 ValueVector3 => valueVector3;
        public GameObject ValueGameObject => valueGameObject;
    }
}
