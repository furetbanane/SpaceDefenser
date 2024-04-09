using System;

namespace StudioXP.Scripts.Registries
{
    [Serializable]
    public class Item : RegistryElement
    {
        public static readonly Item None = new Item{Identifier = -1};
        
        public int Get()
        {
            return ItemRegistry.Instance.Get(this);
        }

        public void Set(int value)
        {
            ItemRegistry.Instance.Set(this, value);
        }

        public void Add(int value)
        {
            ItemRegistry.Instance.Add(this, value);
        }

        public void Remove(int value)
        {
            ItemRegistry.Instance.Remove(this, value);
        }
    }
}
