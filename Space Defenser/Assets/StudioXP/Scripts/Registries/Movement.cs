using System;

namespace StudioXP.Scripts.Registries
{
    [Serializable]
    public class Movement : RegistryElement
    {
        public static readonly Movement None = new Movement{Identifier = -1};
    }
}
