using System;
using UnityEngine;

namespace ContextualDialogueSystem.Fact
{
    [Serializable]
    internal class ObservableFact<T> : IFact<T>, IObservableFact<T>
    {
        [SerializeField]
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueSet?.Invoke(_value);
            }
        }

        public event FactValueAction<T> ValueSet;
    }
}
