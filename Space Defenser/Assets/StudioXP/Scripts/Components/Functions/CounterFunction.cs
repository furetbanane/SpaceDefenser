using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Common;
using StudioXP.Scripts.Events;
using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// CounterFunction permet de compter vers le haut ou vers le bas. Des évènements sont lancés lors de
    /// l'incrémentation, la décrémentation, le changement de valeur, le maximum atteint et le minimum atteint.
    ///
    /// La valeur courante du compteur ne peux pas dépasser minimum vers le bas ou maximum vers le haut.
    /// 
    /// </summary>
    public class CounterFunction : SXPMonobehaviour
    {
        [SerializeField] private int minimum = 0;
        [SerializeField] private int maximum = 5;

        [LabelText("Valeur de départ")] [SerializeField]
        private int startingValue = 0;

        [FoldoutGroup("Évènements", false)] [LabelText("Minimum Atteint")] [SerializeField]
        private UnityEvent onMinimumReached;

        [FoldoutGroup("Évènements")] [LabelText("Maximum Atteint")] [SerializeField]
        private UnityEvent onMaximumReached;

        [FoldoutGroup("Évènements")] [LabelText("Incrementation de la valeur")] [SerializeField]
        private UnityEvent onValueIncremented;

        [FoldoutGroup("Évènements")] [LabelText("Décrementation de la valeur")] [SerializeField]
        private UnityEvent onValueDecremented;

        [FoldoutGroup("Évènements")] [LabelText("Changement de la valeur")] [SerializeField]
        private IntEvent onValueChanged;

        private int _value;
        
        public int Maximum => maximum;
        
        public int Minimum => minimum;

        /// <summary>
        /// Valeur initiale et courante du compteur
        /// </summary>
        public int Value => _value;

        private void Awake()
        {
            Reset();
        }

        /// <summary>
        /// Réinitialise la valeur courante à sa valeur initiale
        /// </summary>
        public void Reset()
        {
            _value = startingValue;
        }

        /// <summary>
        /// Incrémente la valeur courante de 1
        /// </summary>
        public void Increment()
        {
            if (_value >= maximum) return;

            _value++;
            if (_value > maximum)
                _value = maximum;
            
            onValueChanged.Invoke(_value);
            onValueIncremented.Invoke();

            if (_value < maximum) return;

            _value = maximum;
            onMaximumReached.Invoke();
        }
        
        /// <summary>
        /// Incrémente la valeur courante de : quantity
        /// </summary>
        /// <param name="quantity"></param>
        public void Increment(int quantity)
        {
            
        }

        /// <summary>
        /// Incrémente la valeur courante de info.CollisionInfoValue.ValueInt.
        /// </summary>
        /// <param name="info"></param>
        public void Increment(CollisionInfo info)
        {
            if (info.CollisionInfoValue == null)
                Increment();
            else
                Increment(info.CollisionInfoValue.ValueInt);
        }

        /// <summary>
        /// Décrémente la valeur courante de 1
        /// </summary>
        [Button]
        public void Decrement()
        {
            Decrement(1);
        }

        /// <summary>
        /// Décrémente la valeur courante de : quantity
        /// </summary>
        /// <param name="quantity"></param>
        public void Decrement(int quantity)
        {
            if (_value <= minimum) return;

            _value -= quantity;
            if (_value < minimum)
                _value = minimum;

            onValueChanged.Invoke(_value);
            onValueDecremented.Invoke();

            if (_value > minimum) return;

            _value = minimum;
            onMinimumReached.Invoke();
        }

        /// <summary>
        /// Décrémente la valeur courante de : info.CollisionInfoValue.ValueInt.
        /// </summary>
        /// <param name="info"></param>
        public void Decrement(CollisionInfo info)
        {
            if (info.CollisionInfoValue == null)
                Decrement();
            else
                Decrement(info.CollisionInfoValue.ValueInt);
        }

        /// <summary>
        /// Change la valeur courante pour celle du minimum
        /// </summary>
        public void SetToMinimum()
        {
            if (_value == minimum) return;
            _value = minimum;
            
            onValueChanged.Invoke(_value);
            onValueDecremented.Invoke();
            onMinimumReached.Invoke();
        }
        
        /// <summary>
        /// Change la valeur courante pour celle du maximum
        /// </summary>
        public void SetToMaximum()
        {
            if (_value == maximum) return;
            _value = maximum;
            
            onValueChanged.Invoke(_value);
            onValueIncremented.Invoke();
            onMaximumReached.Invoke();
        }
    }
}
