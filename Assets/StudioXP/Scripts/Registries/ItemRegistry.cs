using System.Collections.Generic;
using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [ExecuteInEditMode]
    public class ItemRegistry : Registry<ItemType, Item>
    {
        public static ItemRegistry Instance => (ItemRegistry) InstanceHidden;

        private readonly List<int> _nbItems = new List<int>();

        public int Get(Item item)
        {
            return !IsValid(item) ? 0 : _nbItems[item.Identifier];
        }

        public void Set(Item item, int value)
        {
            if (!IsValid(item))
                return;

            var itemType = Elements[item.Identifier];
            
            if (value < 0)
                value = 0;
            else if (itemType.HasMaximum && value > itemType.Maximum)
                value = itemType.Maximum;
            
            if(value == 0 && _nbItems[item.Identifier] != 0)
                itemType.OnMinimumReached.Invoke();
            else if(itemType.HasMaximum && value == itemType.Maximum && _nbItems[item.Identifier] != itemType.Maximum)
                itemType.OnMaximumReached.Invoke();

            _nbItems[item.Identifier] = value;
        }

        public void Add(Item item, int value)
        {
            if (!IsValid(item))
                return;
            
            Set(item, _nbItems[item.Identifier] + value);
        }

        public void Remove(Item item, int value)
        {
            if (!IsValid(item))
                return;
            
            Add(item, -value);
        }

        protected override void OnAwake()
        {
            if (!Application.isPlaying)
                return;

            for(var i = 0; i < Elements.Count; i++)
                _nbItems.Add(Elements[i].StartingValue);
        }

        private bool IsValid(Item item)
        {
            return item.Identifier >= 0 && item.Identifier < Elements.Count;
        }
    }
}
