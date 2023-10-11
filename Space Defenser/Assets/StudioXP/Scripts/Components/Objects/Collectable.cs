using Sirenix.OdinInspector;
using StudioXP.Scripts.Registries;
using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Components.Objects
{
    public class Collectable : MonoBehaviour
    {
        [LabelText("Identifiant")]
        [ValueDropdown("@ItemRegistry.Instance.GetView()")]
        [SerializeField] private Item item;
        
        [LabelText("Valeur")]
        [SerializeField] private int value;
        
        [ChildGameObjectsOnly]
        [SerializeField] private Animator animator;
        
        [FoldoutGroup("Sons")] 
        [LabelText("Collecte")]
        [ValueDropdown("@SoundRegistry.Instance.GetView()")]
        [SerializeField] private Sound soundPickupId;

        [FoldoutGroup("Évènements")] 
        [LabelText("Collecte")]
        [SerializeField] private UnityEvent collectedEvent;
        
        private int _animatorPickup;

        private bool _isPickedUp;
        
        private void Awake()
        {
            _animatorPickup = Animator.StringToHash("Pickup");
        }

        public void DestroyCollectible()
        {
            Destroy(gameObject);
        }

        public void Collect()
        {
            if (_isPickedUp) return;
            
            collectedEvent.Invoke();
            ItemRegistry.Instance.Add(item, value);
            SoundRegistry.Instance.Play(soundPickupId);
            if (animator)
            {
                animator.SetTrigger(_animatorPickup);
                _isPickedUp = true;
            }
        }
    }
}
