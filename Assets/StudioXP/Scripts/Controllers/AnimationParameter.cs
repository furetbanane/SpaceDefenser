using System;
using System.Runtime.InteropServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Components.Handlers
{
    /// <summary>
    /// Représente un paramètre de l'animator.
    ///
    /// Utilisé dans la visualisation des paramètres de l'<see cref="AnimatorHandler"/>
    /// </summary>
    [Serializable]
    public class AnimationParameter
    {
        [Serializable]
        [StructLayout(LayoutKind.Explicit)]
        public struct AnimationParameterValue
        {
            [FieldOffset(0)] public int Int;
            [FieldOffset(0)] public float Float;
            [FieldOffset(4)] public bool Bool;
        }
        
        [HideLabel]
        [ReadOnly]
        [SerializeField] private string name;
        
        [HideLabel]
        [ReadOnly]
        [SerializeField] private AnimatorControllerParameterType type;
        
        [HideInInspector] [SerializeField] private AnimationParameterValue value;

        [ShowIf("type", AnimatorControllerParameterType.Int)]
        private int ValueInt
        {
            get => value.Int;
            set => this.value.Int = value;
        }
        
        [ShowIf("type", AnimatorControllerParameterType.Float)]
        private float ValueFloat
        {
            get => value.Float;
            set => this.value.Float = value;
        }
        
        [ShowIf("type", AnimatorControllerParameterType.Bool)]
        private bool ValueBool
        {
            get => value.Bool;
            set => this.value.Bool = value;
        }

        public AnimationParameter(string paramName, AnimatorControllerParameterType paramType)
        {
            name = paramName;
            type = paramType;
        }
    }
}
