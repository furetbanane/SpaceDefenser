using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [Serializable]
    public class MessageType : IRegistryType
    {
        [LabelText("Identifiant")]
        [SerializeField] private string name;

        public String Name => name;

        private MessageType()
        {
        }
    }
}
