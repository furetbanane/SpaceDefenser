using System;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Handlers;
using StudioXP.Scripts.Events;
using StudioXP.Scripts.Game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace StudioXP.Scripts.Components.Movements.Characters
{
    [RequireComponent(typeof(SpriteRendererHandler))]
    [RequireComponent(typeof(AnimatorHandler))]
    [RequireComponent(typeof(ColliderHandler))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class WalkFunction : MonoBehaviour
    {
        private float speed = 5;
        
        [LabelText("Accélération")]
        [MinValue(0)]
        [SerializeField] private float acceleration = 0.05f;
        
        [LabelText("Décélération passive")]
        [MinValue(0)]
        [SerializeField] private float passiveDeceleration = 0.05f;
        
        [LabelText("Décélération active")]
        [MinValue(0)]
        [SerializeField] private float activeDeceleration = 0.1f;
        
        [FormerlySerializedAs("velocityChanged")]
        [FoldoutGroup("Évènements", false)] 
        [LabelText("Changement de vitesse")]
        [SerializeField] private FloatEvent speedChanged;
        
        [FormerlySerializedAs("reachedMaxVelocity")]
        [FoldoutGroup("Évènements")]
        [LabelText("Vitesse maximum atteinte")]
        [SerializeField] private UnityEvent reachedMaxSpeed;
        
        [FormerlySerializedAs("droppedMaxVelocity")]
        [FoldoutGroup("Évènements")]
        [LabelText("Fin de la vitesse maximum")]
        [SerializeField] private UnityEvent droppedMaxSpeed;

        private readonly float _timeAdjustement = 200;
        
        private SpriteRendererHandler _spriteRendererHandler;
        private AnimatorHandler _animatorHandler;
        private ColliderHandler _colliderHandler;
        private Rigidbody2D _rigidbody;

        private float _spriteDirection;
        private float _moveDirection;
        private float _targetSpeed;
        private float _currentSpeed;
        private float _tempSpeed;
        private float _tempAcceleration;
        private float _tempPassiveDeceleration;
        private float _tempActiveDeceleration;

        private int _animatorHorizontalVelocity;

        public float CurrentSpeed => _currentSpeed;
        
        public void SetWalkingVelocity(float walkingVelocity)
        {
            _tempSpeed = walkingVelocity;
        }

        public void ResetWalkingVelocity()
        {
            _tempSpeed = speed;
        }

        public void SetAcceleration(float walkingAcceleration)
        {
            _tempAcceleration = walkingAcceleration;
        }

        public void ResetAcceleration()
        {
            _tempAcceleration = acceleration;
        }
        
        public void SetPassiveDeceleration(float walkingPassiveDeceleration)
        {
            _tempPassiveDeceleration = walkingPassiveDeceleration;
        }

        public void ResetPassiveDeceleration()
        {
            _tempPassiveDeceleration = passiveDeceleration;
        }
        
        public void SetActiveDeceleration(float walkingActiveDeceleration)
        {
            _tempActiveDeceleration = walkingActiveDeceleration;
        }

        public void ResetActiveDeceleration()
        {
            _tempActiveDeceleration = activeDeceleration;
        }

        public void Walk(DirectionHorizontal direction)
        {
            Walk(direction == DirectionHorizontal.Left ? -1 : 1);
        }

        public void Walk(float direction)
        {
            _moveDirection = direction;
            if (direction != 0)
            {
                _spriteDirection = direction;
                _spriteRendererHandler.HorizontalFacing = direction < 0
                    ? DirectionHorizontal.Left
                    : DirectionHorizontal.Right;
            }

            if (direction == 0)
                _targetSpeed = 0;
            else if (direction > 0)
                _targetSpeed = _tempSpeed;
            else
                _targetSpeed = -_tempSpeed;
            
            var absPreviousSpeed = Mathf.Abs(_currentSpeed);
            var previousSpeed = _currentSpeed;
            UpdateSpeed();

            var absCurrentSpeed = Mathf.Abs(_currentSpeed);
            if (Math.Abs(_currentSpeed - previousSpeed) > float.Epsilon)
            {
                speedChanged.Invoke(_currentSpeed);
            }
            
            if(absPreviousSpeed < speed && absCurrentSpeed >= speed)
                reachedMaxSpeed.Invoke();
            else if(absPreviousSpeed >= speed && absCurrentSpeed < speed)
                droppedMaxSpeed.Invoke();

            var currentSpeed = _rigidbody.velocity;
            currentSpeed = new Vector2(_currentSpeed, currentSpeed.y);
            _rigidbody.velocity = currentSpeed;
            _animatorHandler.SetAnimatorFloat(_animatorHorizontalVelocity, Mathf.Abs(currentSpeed.x));
        }

        private void UpdateSpeed()
        {
            if (_targetSpeed < 0 && _colliderHandler.IsTouchingTerrain(Direction.Left))
            {
                _currentSpeed = 0;
                return;
            }
            
            if (_targetSpeed > 0 && _colliderHandler.IsTouchingTerrain(Direction.Right))
            {
                _currentSpeed = 0;
                return;
            }
                
            var previousVelocity = _currentSpeed;
            var appliedAcceleration = GetAcceleration() * GetDeltaTime();
            if (appliedAcceleration == 0)
            {
                _currentSpeed = _targetSpeed;
                return;
            }
            
            var newVelocity = previousVelocity + appliedAcceleration;
            _currentSpeed = newVelocity;

            var factoredTargetVelocity = _targetSpeed;
            if (appliedAcceleration < 0)
            {
                previousVelocity *= -1;
                newVelocity *= -1;
                factoredTargetVelocity *= -1;
            }

            if (previousVelocity < factoredTargetVelocity && factoredTargetVelocity < newVelocity)
            {
                _currentSpeed = _targetSpeed;
            }
        }

        private float GetAcceleration()
        {
            if (Math.Abs(_currentSpeed - _targetSpeed) < float.Epsilon)
                return 0;
            
            if (_spriteDirection * _currentSpeed < 0)
            {
                if (_targetSpeed < _currentSpeed)
                    return -_tempActiveDeceleration;

                return _tempActiveDeceleration;
            }

            if (_moveDirection != 0)
            {
                if (_targetSpeed < _currentSpeed && _targetSpeed < 0)
                    return -_tempAcceleration;
                
                if(_targetSpeed > _currentSpeed && _targetSpeed > 0)
                    return _tempAcceleration;
            }
            
            if (_targetSpeed < _currentSpeed)
                return -_tempPassiveDeceleration;
                
            return _tempPassiveDeceleration;
        }

        private float GetDeltaTime()
        {
            return Time.deltaTime * _timeAdjustement;
        }
        
        private void Awake()
        {
            _spriteRendererHandler = GetComponent<SpriteRendererHandler>();
            _animatorHandler = GetComponent<AnimatorHandler>();
            _colliderHandler = GetComponent<ColliderHandler>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _tempSpeed = speed;
            _tempAcceleration = acceleration;
            _tempPassiveDeceleration = passiveDeceleration;
            _tempActiveDeceleration = activeDeceleration;
            _spriteDirection = (_spriteRendererHandler.HorizontalFacing == DirectionHorizontal.Left) ? -1 : 1;
            
            _animatorHorizontalVelocity = AnimatorHandler.GetAnimatorHash("HorizontalVelocity");
        }
    }
}
