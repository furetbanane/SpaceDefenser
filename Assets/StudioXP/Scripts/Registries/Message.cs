using System;

namespace StudioXP.Scripts.Registries
{
    /// <summary>
    /// Message envoyé par <see cref="MessageHandler"/>
    /// </summary>
    [Serializable]
    public class Message : RegistryElement
    {
        public static readonly Message None = new Message{Identifier = -1};
    }
}
