using ContextualDialogueSystem.Fact;
using System;
using UnityEngine;

namespace ContextualDialogueSystem.Rule.Criteria.Condition
{
    [Serializable]
    internal class OrderingCondition<T> : IFactCondition<T>
        where T : IComparable<T>
    {
        [SerializeField]
        private T _value;
        [SerializeField]
        private OrderingComparison _orderingComparison;
        public OrderingCondition(T value, OrderingComparison orderingComparison)
        {
            _value = value;
            _orderingComparison = orderingComparison;
        }

        bool IFactCondition<T>.Satisfies<U>(IFact<U> fact) => _orderingComparison switch
        {
            OrderingComparison.LessThan => fact.Value.CompareTo(_value) < 0,
            OrderingComparison.LessThanOrEqual => fact.Value.CompareTo(_value) <= 0,
            OrderingComparison.Equal => fact.Value.CompareTo(_value) == 0,
            OrderingComparison.NotEqual => fact.Value.CompareTo(_value) != 0,
            OrderingComparison.GreaterThanOrEqual => fact.Value.CompareTo(_value) >= 0,
            OrderingComparison.GreaterThan => fact.Value.CompareTo(_value) > 0,
            _ => throw new NotImplementedException()
        };
    }
}