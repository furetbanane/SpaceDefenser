using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [Serializable]
    public class GroupType : IRegistryType
    {
        [LabelText("Identifiant")] [SerializeField]
        private string name;

        public String Name => name;

        protected GroupType()
        {
        }
    }
}
