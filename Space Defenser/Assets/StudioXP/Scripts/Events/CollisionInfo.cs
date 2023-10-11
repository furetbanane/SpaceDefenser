using StudioXP.Scripts.Components.Common;
using StudioXP.Scripts.Game;
using UnityEngine;

namespace StudioXP.Scripts.Events
{
    /// <summary>
    /// Information de collision. Contient le GameObject avec lequel on est entré en collision ainsi que des classes
    /// d'informations CollisionInfoFilter et CollisionInfoValue.
    /// </summary>
    public class CollisionInfo
    {
        public GameObject GameObject { get; }
        public CollisionInfoFilter CollisionInfoFilter { get; }
        
        public CollisionInfoValue CollisionInfoValue { get; }
        
        public CollisionInfo(GameObject go, CollisionInfoFilter infoFilter)
        {
            GameObject = go;
            CollisionInfoFilter = infoFilter;
            CollisionInfoValue = go.GetComponent<CollisionInfoValue>();
        }
    }
}
