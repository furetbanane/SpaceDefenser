using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Registries
{
    [Serializable]
    public class ItemType : IRegistryType
    {
        [LabelText("Identifiant")]
        [SerializeField] private string name;

        [LabelText("Valeur de départ")] 
        [SerializeField] private int startingValue = 0;

        [LabelText("As un maximum")] 
        [SerializeField] private bool hasMaximum;

        [ShowIf("$hasMaximum")]
        [SerializeField] private int maximum;

        [FoldoutGroup("Évènements")]
        [SerializeField] private UnityEvent onMinimumReached;
        
        [FoldoutGroup("Évènements")]
        [ShowIf("$hasMaximum")]
        [SerializeField] private UnityEvent onMaximumReached;

        public String Name => name;
        public int StartingValue => startingValue;
        public bool HasMaximum => hasMaximum;
        public int Maximum => maximum;
        public UnityEvent OnMinimumReached => onMinimumReached;
        public UnityEvent OnMaximumReached => onMaximumReached;

        private ItemType()
        {
        }
    }
}
