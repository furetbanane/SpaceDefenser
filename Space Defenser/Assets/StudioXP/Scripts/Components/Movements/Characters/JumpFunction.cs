using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Handlers;
using StudioXP.Scripts.Game;
using StudioXP.Scripts.Registries;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace StudioXP.Scripts.Components.Movements.Characters
{
    [RequireComponent(typeof(ColliderHandler))]
    [RequireComponent(typeof(AnimatorHandler))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class JumpFunction : MonoBehaviour
    {
        private float speed = 10;
        
        [LabelText("Nombre de sauts (maximum)")] [MinValue(0)] 
        [SerializeField] private int maxNbJump = 0;
        
        [LabelText("Amortissement de la gravité")]
        [SerializeField] private float fallingDampening;
        
        [FoldoutGroup("Sons", false)]
        [LabelText("Saut")]
        [ValueDropdown("@SoundRegistry.Instance.GetView()")]
        [SerializeField] private Sound jumpSound;
        
        [LabelText("Début du saut")]
        [FoldoutGroup("Évènements", false)]
        [SerializeField] private UnityEvent startJumpingEvent;
        
        [LabelText("Saut")]
        [FoldoutGroup("Évènements")]
        [SerializeField] private UnityEvent stopJumpingEvent;
        
        [LabelText("Fin du saut")]
        [FoldoutGroup("Évènements")]
        [SerializeField] private UnityEvent landingAfterJumpEvent;
        
        private AnimatorHandler _animatorHandler;
        private ColliderHandler _colliderHandler;

        private Rigidbody2D _rigidbody;

        private bool _isGrounded;
        private bool _isJumping;
        private bool _hasLanded;
        private int _jumpCount;
        private int _tempsMaxNbJump;
        private float _tempSpeed;
        
        private float _jumpTolerance = 0.2f;
        private int _animatorJump;
        private int _animatorIsGrounded;
        private int _animatorVerticalVelocity;

        public bool IsJumping => _isJumping;

        public void SetJumpingVelocity()
        {
            
        }

        public void ForceJumpStart()
        {
            _isJumping = true;
            startJumpingEvent.Invoke();
            JumpStartCommon();
        }
        
        public void JumpStart()
        {
            _isJumping = true;
            
            if (_isGrounded)
                _jumpCount = 0;
            else
                _jumpCount++;
            
            startJumpingEvent.Invoke();

            if (_jumpCount >= _tempsMaxNbJump) return;
            
            JumpStartCommon();
        }

        private void JumpStartCommon()
        {
            _hasLanded = false;
            _animatorHandler.SetAnimatorTrigger(_animatorJump);
            SoundRegistry.Instance.Play(jumpSound);
            _rigidbody.position += _jumpTolerance * Vector2.up;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _tempSpeed);
        }

        public void JumpStay()
        {
            var newVelocity = _rigidbody.velocity.y;
            if (_rigidbody.velocity.y < 0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, newVelocity + fallingDampening);

                if (_isGrounded)
                {
                    _hasLanded = true;
                    _isJumping = false;
                    landingAfterJumpEvent.Invoke();
                }
            }
        }

        public void JumpStop()
        {
            _isJumping = false;
            if (!_isGrounded)
            {
                var newVelocity = _rigidbody.velocity.y;
                if (newVelocity > 0)
                    newVelocity = 0;
                
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, newVelocity);
            }
            stopJumpingEvent.Invoke();
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animatorHandler = GetComponent<AnimatorHandler>();
            _colliderHandler = GetComponent<ColliderHandler>();
            _animatorJump = AnimatorHandler.GetAnimatorHash("Jump");
            _animatorIsGrounded = AnimatorHandler.GetAnimatorHash("IsGrounded");
            _animatorVerticalVelocity = AnimatorHandler.GetAnimatorHash("VerticalVelocity");

            _hasLanded = true;
            _tempSpeed = speed;
            _tempsMaxNbJump = maxNbJump;
        }

        private void Update()
        {
            _isGrounded = _colliderHandler.IsTouchingTerrain(Direction.Down);
            _animatorHandler.SetAnimatorBool(_animatorIsGrounded, _isGrounded);
            _animatorHandler.SetAnimatorFloat(_animatorVerticalVelocity, _rigidbody.velocity.y);

            if (!_hasLanded)
            {
                _hasLanded = _isGrounded;
            }
        }
    }
}
