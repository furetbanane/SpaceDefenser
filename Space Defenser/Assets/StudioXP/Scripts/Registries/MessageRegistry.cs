using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [ExecuteInEditMode]
    public class MessageRegistry : Registry<MessageType, Message>
    {
        public static MessageRegistry Instance => (MessageRegistry) InstanceHidden;

        protected override void OnAwake()
        {
        }
    }
}
