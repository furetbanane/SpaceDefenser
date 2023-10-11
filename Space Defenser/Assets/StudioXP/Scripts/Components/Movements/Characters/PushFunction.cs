using StudioXP.Scripts.Components.Handlers;
using StudioXP.Scripts.Components.Objects;
using StudioXP.Scripts.Events;
using StudioXP.Scripts.Game;
using UnityEngine;

namespace StudioXP.Scripts.Components.Movements.Characters
{
    public class PushFunction : MonoBehaviour
    {
        private AnimatorHandler _animatorHandler;
        private int _animatorIsPushing;
        
        void Awake()
        {
            _animatorHandler = GetComponent<AnimatorHandler>();
            _animatorIsPushing = AnimatorHandler.GetAnimatorHash("IsPushing");
        }
        
        public void Push(CollisionInfo info)
        {
            Push(info.GameObject);
        }
        
        public void Push(GameObject go)
        {
            var pushable = go.GetComponent<PushableObject>();
            if (!pushable) return;
            if (!pushable.CanMove(go.transform.position.x < transform.position.x
                ? Direction.Left
                : Direction.Right)) return;
            
            if (_animatorHandler)
            {
                _animatorHandler.SetAnimatorBool(_animatorIsPushing, true);
            }
        }

        public void StopPush(CollisionInfo info)
        {
            StopPush(info.GameObject);
        }

        public void StopPush(GameObject go)
        {
            if (_animatorHandler)
            {
                _animatorHandler.SetAnimatorBool(_animatorIsPushing, false);
            }
        }
    }
}
