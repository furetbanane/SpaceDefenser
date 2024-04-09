using Sirenix.OdinInspector;
using StudioXP.Scripts.Events;
using StudioXP.Scripts.Game;
using UnityEditor;
using UnityEngine;

namespace StudioXP.Scripts.Components.Movements.Platforms
{
    public class MoveLimitAuto : MonoBehaviour
    {
        
        [FoldoutGroup("Évènements",false)]
        [LabelText("Vitesse changée")]
        [SerializeField] private Vector2Event velocityChanged;
        
        [HideInInspector] [SerializeField] private Rect bounds;

        public void SetBounds(Rect rect)
        {
            bounds = rect;
        }

/*
        private Vector2 GetDirection()
        {
            return startingDirection switch
            {
                Direction.Down => Vector2.up,
                Direction.Left => Vector2.right,
                Direction.Right => Vector2.right,
                Direction.Up => Vector2.up,
                _ => Vector2.zero
            };
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var direction = GetDirection();
            float extent = 0;
            if (direction == Vector2.left)
            {
                direction = Vector2.right;
            }
            else if (direction == Vector2.down)
            {
                direction = Vector2.up;
            }

            if (direction == Vector2.right)
                extent = bounds.size.x / 2;
            else
                extent = bounds.size.y / 2;

            Vector2 position;
            if (Application.isPlaying)
                position = _initialPosition;
            else
                position = transform.position;

            Vector3 start = position + (leftPosition - extent) * direction;
            Vector3 end = position + (rightPosition + extent) * direction;
            Color color = Color.blue;

            Vector3 perpendicular = Vector2.Perpendicular(direction);

            DrawThickLine(start, end, color);
            DrawThickLine(start + perpendicular * 0.5f, start + perpendicular * (-0.5f), color);
            DrawThickLine(end + perpendicular * 0.5f, end + perpendicular * (-0.5f), color);
        }

        private void DrawThickLine(Vector3 start, Vector3 end, Color color)
        {
            Handles.DrawBezier(start, end, start, end, color, null, 4);
        }
#endif

*/

    }
}
