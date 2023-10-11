using System;
using System.Collections;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Events;
using UnityEngine;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// SendFunction fonctionne en collaboration avec ReceiveFunction.
    /// SendFunction envoie un message à un objet contenant un ReceiveFunction et ce dernier envoie un évènement lorsque
    /// le message est reçu si l'identifiant du message se trouve dans acceptIdentifiers.
    ///
    /// Il faut y définir le type de message à envoyer ainsi que son identifiant. La valeur à envoyé peut être défini
    /// grâce à SetValue.
    /// </summary>
    public class SendFunction : SXPMonobehaviour
    {
        public enum SendFunctionType
        {
            Void,
            Float,
            Int,
            Bool,
            Vector2,
            Vector3,
            GameObject
        }

        [LabelText("Identifieur")]
        [SerializeField] private string identifier;
        
        /// <summary>
        /// Délai avant de lancer l'évènement
        /// </summary>
        [LabelText("Délai de collision")]
        [SerializeField] private float collisionDelay = 0;

        [PropertyTooltip("Type de valeur à envoyer")]
        [SerializeField] private SendFunctionType type = SendFunctionType.Void;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.Float)] private float valueFloat;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.Int)] private int valueInt;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.Bool)] private bool valueBool;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.Vector2)] private Vector2 valueVector2;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.Vector3)] private Vector3 valueVector3;
        [LabelText("Valeur")]
        [SerializeField, ShowIf("type", SendFunctionType.GameObject)] private GameObject valueGameObject;

        private CollisionInfo _lastCollision;

        /// <summary>
        /// Définie la value de type float.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(float value)
        {
            valueFloat = value;
        }
        
        /// <summary>
        /// Définie la value de type int.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(int value)
        {
            valueInt = value;
        }
        
        /// <summary>
        /// Définie la value de type bool.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(bool value)
        {
            valueBool = value;
        }
        
        /// <summary>
        /// Définie la value de type Vector2.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(Vector2 value)
        {
            valueVector2 = value;
        }
        
        /// <summary>
        /// Définie la value de type Vector3.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(Vector3 value)
        {
            valueVector3 = value;
        }
        
        /// <summary>
        /// Définie la value de type GameObject.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(GameObject value)
        {
            valueGameObject = value;
        }

        /// <summary>
        /// Défini l'information de collision
        /// </summary>
        /// <param name="info"></param>
        public void SetLastCollision(CollisionInfo info)
        {
            _lastCollision = info;
        }

        /// <summary>
        /// Efface l'information de collision
        /// </summary>
        public void ClearLastCollision()
        {
            _lastCollision = null;
        }
        
        /// <summary>
        /// Envoie le message à la dernière collision effectué
        /// </summary>
        public void SendToLastCollision()
        {
            if(_lastCollision != null)
                SendToCollision(_lastCollision);
        }

        /// <summary>
        /// Envoie le message à la collision envoyé en paramètre
        /// </summary>
        /// <param name="info"></param>
        public void SendToCollision(CollisionInfo info)
        { 
            StartCoroutine(SendToCollisionDelay(info));
        }

        /// <summary>
        /// Envoie le message à la collision envoyé en paramètre après un certain délai (en secondes).
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private IEnumerator SendToCollisionDelay(CollisionInfo info)
        {
            if (info == null || !info.GameObject) yield break;
            var receiveGO = info.GameObject;
            var receiveFunctions = receiveGO.GetComponentsInChildren<ReceiveFunction>();

            if (receiveFunctions == null) yield break;
            if (collisionDelay > 0)
                yield return new WaitForSeconds(collisionDelay);

            ReceiveFunction receiveFunction = null;
            foreach (var receiveF in receiveFunctions)
            {
                if (receiveF.Accepts(identifier))
                    receiveFunction = receiveF;
            }
            
            if(!receiveFunction)
                yield break;
            
            switch (type)
            {
                case SendFunctionType.Void:
                    receiveFunction.Receive();
                    break;
                case SendFunctionType.Float:
                    receiveFunction.Receive(valueFloat);
                    break;
                case SendFunctionType.Int:
                    receiveFunction.Receive(valueInt);
                    break;
                case SendFunctionType.Bool:
                    receiveFunction.Receive(valueBool);
                    break;
                case SendFunctionType.Vector2:
                    receiveFunction.Receive(valueVector2);
                    break;
                case SendFunctionType.Vector3:
                    receiveFunction.Receive(valueVector3);
                    break;
                case SendFunctionType.GameObject:
                    receiveFunction.Receive(valueGameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
