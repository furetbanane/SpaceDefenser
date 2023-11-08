using System;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Registries;
using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Events
{
    [Serializable]
    public class MovementEvent
    {
        public enum MovementEventType
        {
            Void,
            Float,
            Int,
            Bool,
            Vector2,
            Vector2Int,
            Vector3,
            Vector3Int
        }
        
        [SerializeField, ValueDropdown("@MovementRegistry.Instance.GetView()")] private Registries.Movement movement = Registries.Movement.None;
        
        [SerializeField] private MovementEventType type = MovementEventType.Void;
        [SerializeField, ShowIf("type", MovementEventType.Void)] private UnityEvent onMovementVoid;
        [SerializeField, ShowIf("type", MovementEventType.Float)] private FloatEvent onMovementFloat;
        [SerializeField, ShowIf("type", MovementEventType.Int)] private IntEvent onMovementInt;
        [SerializeField, ShowIf("type", MovementEventType.Bool)] private BoolEvent onMovementBool;
        [SerializeField, ShowIf("type", MovementEventType.Vector2)] private Vector2Event onMovementVector2;
        [SerializeField, ShowIf("type", MovementEventType.Vector2Int)] private Vector2IntEvent onMovementVector2Int;
        [SerializeField, ShowIf("type", MovementEventType.Vector3)] private Vector3Event onMovementVector3;
        [SerializeField, ShowIf("type", MovementEventType.Vector3Int)] private Vector3IntEvent onMovementVector3Int;

        public Registries.Movement Movement => movement;

        public bool Execute(string name, object value = null)
        {
            if (MovementRegistry.Instance.GetElement(name).Identifier != movement.Identifier) return false;
            Execute(value);
            return true;
        }

        public void Execute(object value = null)
        {
            switch (type)
            {
                case MovementEventType.Void:
                    onMovementVoid.Invoke();
                    break;
                case MovementEventType.Float:
                    if (value != null) onMovementFloat.Invoke((float) value);
                    break;
                case MovementEventType.Int:
                    if (value != null) onMovementInt.Invoke((int) value);
                    break;
                case MovementEventType.Bool:
                    if (value != null) onMovementBool.Invoke((bool) value);
                    break;
                case MovementEventType.Vector2:
                    if (value != null) onMovementVector2.Invoke((Vector2) value);
                    break;
                case MovementEventType.Vector2Int:
                    if (value != null) onMovementVector2Int.Invoke((Vector2Int) value);
                    break;
                case MovementEventType.Vector3:
                    if (value != null) onMovementVector3.Invoke((Vector3) value);
                    break;
                case MovementEventType.Vector3Int:
                    if (value != null) onMovementVector3Int.Invoke((Vector3Int) value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Value type <" + value?.GetType() +
                                                          "> is not supported by MovementEvent.");
            }
        }
    }
}
