using System;
using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [Serializable]
    public class RegistryElement
    {
        [SerializeField, HideInInspector] private int identifier = -1;
        
        public int Identifier
        {
            get => identifier;
            set => identifier = value;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || GetType() != obj.GetType())
                return false;

            var registryElement = (RegistryElement) obj;
            return (Identifier == registryElement.Identifier);
        }

        protected bool Equals(RegistryElement other)
        {
            return Identifier == other.Identifier;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Identifier.GetHashCode();
        }
    }
}
