using System;
using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [Serializable]
    public struct RegistryElementContainer
    {
        [SerializeField, HideInInspector] private int identifier;
        public int Identifier => identifier;

        public RegistryElementContainer(int identifier)
        {
            this.identifier = identifier;
        }
    }
}
