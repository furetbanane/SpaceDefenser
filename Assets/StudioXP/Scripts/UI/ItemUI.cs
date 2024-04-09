using Sirenix.OdinInspector;
using StudioXP.Scripts.Registries;
using UnityEngine;
using UnityEngine.UI;

namespace StudioXP.Scripts.UI
{
    public class ItemUI : MonoBehaviour
    {
        [LabelText("Identifiant")]
        [ValueDropdown("@ItemRegistry.Instance.GetView()")]
        [SerializeField] private Item item;
        
        [LabelText("Format d'affichage")]
        [SerializeField] private string format = "D2";
        
        [LabelText("Valeur Text")]
        [Required("Ajoute l'élément Text qui servira à afficher le nombre d'items")]
        [ChildGameObjectsOnly]
        [SerializeField] private Text valueText;

        private int _lastValue;
        
        void Update()
        {
            var currentValue = ItemRegistry.Instance.Get(item);
            if (_lastValue == currentValue) return;
            _lastValue = currentValue;
            valueText.text = currentValue.ToString(format);
        }
    }
}
