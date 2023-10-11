using System.Collections.Generic;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Registries;
using UnityEngine;

namespace StudioXP.Scripts.Components.Objects
{
    [ExecuteInEditMode]
    public class Door : MonoBehaviour
    {
        private static readonly HashSet<Door> Registered = new HashSet<Door>();
        private static readonly Dictionary<int, List<Door>> Doors = new Dictionary<int, List<Door>>();
        private static readonly Dictionary<Door, int> Channels = new Dictionary<Door, int>();

        [LabelText("Destination"), SerializeField] private Door destination;
        [LabelText("Serrure"), SerializeField] private bool isLocked;
        [LabelText("Clé"), ShowIf("isLocked"), ValueDropdown("@ItemRegistry.Instance.GetView()"), SerializeField] private Item key;
        [LabelText("Clé (quantité)"), ShowIf("isLocked"), Min(1), SerializeField] private int keyQuantity;
        [FoldoutGroup("Visuel"), LabelText("Couleur (Barré)"), ShowIf("isLocked"), SerializeField] private Color colorLocked = Color.red;
        [FoldoutGroup("Visuel"), LabelText("Couleur (Débarré)"), ShowIf("isLocked"), SerializeField] private Color colorUnlocked = Color.green;
        [FoldoutGroup("Visuel"), LabelText("Couleur (Charnières)"), ShowIf("isLocked"), SerializeField] private Color colorHinges = Color.yellow;
        [FoldoutGroup("Visuel"), LabelText("Sprites (Porte)"), ShowIf("isLocked"), SerializeField] private List<SpriteRenderer> doorSprites;
        [FoldoutGroup("Visuel"), LabelText("Sprites (Charnières)"), ShowIf("isLocked"), SerializeField] private List<SpriteRenderer> hingesSprites;
        
        [FoldoutGroup("Sons")]
        [LabelText("Ouverture")]
        [ValueDropdown("@SoundRegistry.Instance.GetView()")]
        [SerializeField] private Sound openSound;

        public void Open(GameObject openingObject)
        {
            Debug.Log("Ouvrir porte");
        }

        private bool Unlock()
        {
            if (!isLocked) return true;
            var available = ItemRegistry.Instance.Get(key);
            if (available < keyQuantity) return false;
            
            ItemRegistry.Instance.Set(key, available - keyQuantity);
            
            isLocked = false;

            if (destination)
            {
                destination.isLocked = false;
                foreach (var sprite in destination.doorSprites)
                    sprite.color = colorUnlocked;
            }

            foreach (var sprite in doorSprites)
                sprite.color = colorUnlocked;
            
            return true;
        }

        private void OnValidate()
        {
            if (destination == null) return;
            
            destination.isLocked = isLocked;
            destination.colorHinges = colorHinges;
            destination.colorLocked = colorLocked;
            destination.colorUnlocked = colorUnlocked;
            destination.keyQuantity = keyQuantity;
            destination.key = key;
            
            SetLockSprite(this);
            SetLockSprite(destination);
        }

        private void SetLockSprite(Door door)
        {
            foreach (var sprite in door.doorSprites)
                sprite.color = door.isLocked ? door.colorLocked : Color.white;
                
            foreach (var sprite in hingesSprites)
                sprite.color = door.isLocked ? door.colorHinges : Color.white;
        }
    }
}
