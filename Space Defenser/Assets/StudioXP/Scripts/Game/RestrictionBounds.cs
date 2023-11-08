using UnityEngine;

namespace StudioXP.Scripts.Game
{
    public class RestrictionBounds : MonoBehaviour
    {
        public virtual Vector3 GetBoundedPosition(Vector3 position)
        {
            return position;
        }
    }
}
