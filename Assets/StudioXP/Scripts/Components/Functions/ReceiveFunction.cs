using System.Collections.Generic;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Events;
using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// ReceiveFunction fonctionne en collaboration avec SendFunction.
    /// SendFunction envoie un message à un objet contenant un ReceiveFunction et ce dernier envoie un évènement lorsque
    /// le message est reçu si l'identifiant du message se trouve dans acceptIdentifiers.
    ///
    /// Seul l'évènement du type défini dans l'inspecteur s'affichera.
    /// </summary>
    public class ReceiveFunction : SXPMonobehaviour
    {
        [LabelText("Identifiants acceptés")]
        [SerializeField] private List<string> acceptIdentifiers = new List<string>();

        [PropertyTooltip("Type de fonction recevable")]
        [SerializeField] private SendFunction.SendFunctionType type = SendFunction.SendFunctionType.Void;
        [LabelText("Réception")]
        [SerializeField, ShowIf("type", SendFunction.SendFunctionType.Void)] private UnityEvent onReceiveVoid;
        [LabelText("Réception")]
        [SerializeField, ShowIf("type", SendFunction.SendFunctionType.Float)] private FloatEvent onReceiveFloat;
        [LabelText("Réception")]
        [SerializeField, ShowIf("type", SendFunction.SendFunctionType.Int)] private IntEvent onReceiveInt;
        [LabelText("Réception")]
        [SerializeField, ShowIf("type", SendFunction.SendFunctionType.Bool)] private BoolEvent onReceiveBool;
        [LabelText("Réception")]
        [SerializeField, ShowIf("type", SendFunction.SendFunctionType.Vector2)] private Vector2Event onReceiveVector2;
        [LabelText("Réception")]
        [SerializeField, ShowIf("type", SendFunction.SendFunctionType.Vector3)] private Vector3Event onReceiveVector3;
        [LabelText("Réception")]
        [SerializeField, ShowIf("type", SendFunction.SendFunctionType.GameObject)] private GameObjectEvent onReceiveGameObject;

        /// <summary>
        /// Vérifie si un identifiant est accepté
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public bool Accepts(string identifier)
        {
            return acceptIdentifiers.Contains(identifier); 
        }

        /// <summary>
        /// Applique la logique de réception si le type du message est Void.
        /// </summary>
        public void Receive()
        {
            if (type != SendFunction.SendFunctionType.Void) return;
            onReceiveVoid.Invoke();
        }
        
        /// <summary>
        /// Applique la logique de réception si le type du message est Float.
        /// </summary>
        public void Receive(float value)
        {
            if (type != SendFunction.SendFunctionType.Float) return;
            onReceiveFloat.Invoke(value);
        }
        
        /// <summary>
        /// Applique la logique de réception si le type du message est Int.
        /// </summary>
        public void Receive(int value)
        {
            if (type != SendFunction.SendFunctionType.Int) return;
            onReceiveInt.Invoke(value);
        }
        
        /// <summary>
        /// Applique la logique de réception si le type du message est Bool.
        /// </summary>
        public void Receive(bool value)
        {
            if (type != SendFunction.SendFunctionType.Bool) return;
            onReceiveBool.Invoke(value);
        }
        
        /// <summary>
        /// Applique la logique de réception si le type du message est Vector2.
        /// </summary>
        public void Receive(Vector2 value)
        {
            if (type != SendFunction.SendFunctionType.Vector2) return;
            onReceiveVector2.Invoke(value);
        }
        
        /// <summary>
        /// Applique la logique de réception si le type du message est Vector3.
        /// </summary>
        public void Receive(Vector3 value)
        {
            if (type != SendFunction.SendFunctionType.Vector3) return;
            onReceiveVector3.Invoke(value);
        }
        
        /// <summary>
        /// Applique la logique de réception si le type du message est GameObject.
        /// </summary>
        public void Receive(GameObject value)
        {
            if (type != SendFunction.SendFunctionType.GameObject) return;
            onReceiveGameObject.Invoke(value);
        }
    }
}
