using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [ExecuteInEditMode]
    public abstract class Registry<TType, TElement> : MonoBehaviour 
        where TType : IRegistryType 
        where TElement : RegistryElement, new()
    {
        [SerializeField] private List<TType> elements;

        private readonly Dictionary<string, int> _elementsById = new Dictionary<string, int>();

        protected List<TType> Elements => elements;

        protected static Registry<TType, TElement> InstanceHidden { get; private set; }

        public TElement GetElement(string identifier)
        {
            if (!_elementsById.ContainsKey(identifier))
                throw new ArgumentException("L'identifiant <" + identifier + "> n'existe pas dans le registre.");

            return CreateElement(_elementsById[identifier]);
        }

        public TType GetElementType(TElement element)
        {
            return elements[element.Identifier];
        }

        public virtual ValueDropdownList<TElement> GetView()
        {
            var valueDropdown = new ValueDropdownList<TElement>();
            valueDropdown.Add("Aucun", CreateElement(-1));
            for(var i = 0; i < elements.Count; i++)
                valueDropdown.Add(elements[i].Name, CreateElement(i));

            return valueDropdown;
        }

        protected abstract void OnAwake();

        private static TElement CreateElement(int identifier)
        {
            var newElement = new TElement {Identifier = identifier};
            return newElement;
        }

        private void OnValidate()
        {
            InstanceHidden = this;
        }

        private void Awake()
        {
            if (InstanceHidden == null)
                InstanceHidden = this;

            for (var i = 0; i < elements.Count; i++)
                _elementsById.Add(elements[i].Name, i);

            OnAwake();
        }
    }
}

