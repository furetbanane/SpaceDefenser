using System.Collections.Generic;
using StudioXP.Scripts.Registries;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Components.Handlers
{
    /// <summary>
    /// Permet d'envoyer un <see cref="Message"/> à un autre objet possédant aussi un <see cref="MessageHandler"/>
    ///
    /// Les messages sont définis via le rgistre de message. Lorsqu'un message est reçu, un objet peut effectuer un
    /// action selon le message reçu.
    /// </summary>
    public class MessageHandler : MonoBehaviour
    {
        private static readonly Dictionary<GameObject, MessageHandler> Controllers =
            new Dictionary<GameObject, MessageHandler>();

        [LabelText("Évènement")]
        [SerializeField] private UnityEvent<Message> test;
        
        private void Awake()
        {
            Controllers.Add(gameObject, this);
        }

        /// <summary>
        /// Envoie un <see cref="Message"/> à tous les objets possédant un <see cref="MessageHandler"/>
        /// </summary>
        /// <param name="message">Le message à envoyé</param>
        public void SendGlobal(Message message)
        {
            foreach (var controller in Controllers.Values)
            {
                controller.Receive(gameObject, message);
            }
        }
        
        /// <summary>
        /// Envoie un <see cref="Message"/> à un objet spécifique possédant un <see cref="MessageHandler"/>
        /// </summary>
        /// <param name="destination">L'objet auquel on envoie le message</param>
        /// <param name="message">Le message à envoyer</param>
        public void Send(GameObject destination, string message)
        {
            var controller = Controllers[destination];
            if (!controller) return;
            
            //controller.Receive(gameObject, message);
        }

        private void Receive(GameObject origin, Message message)
        {
            
        }
    }
}
