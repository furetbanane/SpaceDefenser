using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Handlers;
using StudioXP.Scripts.Events;
using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Components.Movements.Characters
{
    [RequireComponent(typeof(AnimatorHandler))]
    public class RunFunction : MonoBehaviour
    {
        private float speed = 10;
        
        [FoldoutGroup("Évènements", false)] 
        [LabelText("Début de la course")]
        [SerializeField] private FloatEvent startRunningEvent;
        
        [FoldoutGroup("Évènements")] 
        [LabelText("Fin de la course")]
        [SerializeField] private UnityEvent stopRunningEvent;

        private AnimatorHandler _animatorHandler;
        private bool _isRunning;
        private float _tempVelocity;
        
        private int _animatorIsRunning;

        public void SetRunningVelocity(float runningVelocity)
        {
            _tempVelocity = runningVelocity;
            if(_isRunning)
                startRunningEvent.Invoke(_tempVelocity);
        }

        public void ResetRunningVelocity()
        {
            _tempVelocity = speed;
        }
        
        public void RunStart()
        {
            _isRunning = true;
            _animatorHandler.SetAnimatorBool(_animatorIsRunning, _isRunning);
            startRunningEvent.Invoke(_tempVelocity);
        }

        public void RunStop()
        {
            _isRunning = false;
            _animatorHandler.SetAnimatorBool(_animatorIsRunning, _isRunning);
            stopRunningEvent.Invoke();
        }

        private void Awake()
        {
            _animatorHandler = GetComponent<AnimatorHandler>();
            _animatorIsRunning = AnimatorHandler.GetAnimatorHash("IsRunning");

            _tempVelocity = speed;
        }
    }
}
