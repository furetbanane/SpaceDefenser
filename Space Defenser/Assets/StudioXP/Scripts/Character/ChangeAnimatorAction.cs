using UnityEditor.Animations;
using UnityEngine;

namespace StudioXP.Scripts.Character
{
    public class ChangeAnimatorAction : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        public void Set(RuntimeAnimatorController animatorController)
        {
            if (animator == null) return;
            animator.runtimeAnimatorController = animatorController;
        }
    }
}
