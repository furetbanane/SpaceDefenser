using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [Serializable]
    public class MovementType : IRegistryType
    {
        [LabelText("Identifiant")]
        [SerializeField] private string name;

        public string Name => name;
    }
}
