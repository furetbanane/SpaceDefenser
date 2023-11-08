using System;
using StudioXP.Scripts.Game;
using StudioXP.Scripts.Registries;
using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Events
{
    [Serializable]
    public class FloatEvent : UnityEvent<float>
    {
    }

    [Serializable]
    public class IntEvent : UnityEvent<int>
    {
    }
    
    [Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
    }

    [Serializable]
    public class Vector2Event : UnityEvent<Vector2>
    {
    }
    
    [Serializable]
    public class Vector2IntEvent : UnityEvent<Vector2Int>
    {
    }
    
    [Serializable]
    public class Vector3Event : UnityEvent<Vector3>
    {
    }
    
    [Serializable]
    public class Vector3IntEvent : UnityEvent<Vector3Int>
    {
    }

    [Serializable]
    public class RectEvent : UnityEvent<Rect>
    {
    }

    [Serializable]
    public class GameObjectEvent : UnityEvent<GameObject>
    {
    }

    [Serializable]
    public class GameObjectDirectionEvent : UnityEvent<GameObject, Direction>
    {
    }

    [Serializable]
    public class ColliderEventInfoEvent : UnityEvent<CollisionInfo>
    {
        public float delay;
    }

    [Serializable]
    public class DirectionEvent : UnityEvent<Direction>
    {

    }

    [Serializable]
    public class DirectionHEvent : UnityEvent<DirectionHorizontal>
    {

    }

    [Serializable]
    public class DirectionVEvent : UnityEvent<DirectionVertical>
    {

    }

    [Serializable]
    public class DirectionHVEvent : UnityEvent<DirectionHorizontal, DirectionVertical>
    {

    }

    [Serializable]
    public class MessageEvent : UnityEvent<Message>
    {
        
    }

}
