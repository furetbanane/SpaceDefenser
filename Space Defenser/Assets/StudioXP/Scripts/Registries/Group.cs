using System;

namespace StudioXP.Scripts.Registries
{
    [Serializable]
    public class Group : RegistryElement
    {
        public static readonly Group None = new Group{Identifier = -1};
    }
}
