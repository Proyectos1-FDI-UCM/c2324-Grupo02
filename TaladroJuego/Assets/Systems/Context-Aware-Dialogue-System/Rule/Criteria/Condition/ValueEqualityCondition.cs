using ContextualDialogueSystem.Fact;
using System;
using UnityEngine;

namespace ContextualDialogueSystem.Rule.Criteria.Condition
{
    [Serializable]
    internal class ValueEqualityCondition<T> : IFactCondition<T>
        where T : IEquatable<T>
    {
        [SerializeField]
        private T _value;
        public ValueEqualityCondition(T value) => _value = value;

        bool IFactCondition<T>.Satisfies<U>(IFact<U> fact) =>
            fact.Value.Equals(_value);
    }
}