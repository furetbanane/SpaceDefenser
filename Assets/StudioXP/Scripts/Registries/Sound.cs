using System;

namespace StudioXP.Scripts.Registries
{
    [Serializable]
    public class Sound : RegistryElement
    {
        public static readonly Sound None = new Sound{Identifier = -1};

        public void Play()
        {
            SoundRegistry.Instance.Play(this);
        }
    }
}
