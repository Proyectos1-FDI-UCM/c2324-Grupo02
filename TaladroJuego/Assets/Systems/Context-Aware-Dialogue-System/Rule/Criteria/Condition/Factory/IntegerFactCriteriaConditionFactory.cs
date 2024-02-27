using ContextualDialogueSystem.Fact;
using System;
using UnityEngine;

namespace ContextualDialogueSystem.Rule.Criteria.Condition.Factory
{
    internal class IntegerFactCriteriaConditionFactory : ICriteriaConditionFactory
    {
        private readonly IFactCondition<int> _condition;

        public IntegerFactCriteriaConditionFactory(IFactCondition<int> condition = null)
        {
            _condition = condition;
        }

        public ICriteriaCondition Create() => _condition switch
        {
            ValueEqualityCondition<int> condition => new IntegerFactCriteriaCondition(new FactCriteriaCondition<int>(null, new ValueEqualityCondition(condition))),
            OrderingCondition<int> condition => new IntegerFactCriteriaCondition(new FactCriteriaCondition<int>(null, new OrderingCondition(condition))),
            _ => throw new NotImplementedException()
        };

        public ICriteriaConditionFactory TryConfigureWith<T>(IFactCondition<T> value)
        {
            if (value is IFactCondition<int> condition)
                return new IntegerFactCriteriaConditionFactory(condition);
            return this;
        }

        private class IntegerFactCriteriaCondition : ICriteriaCondition
        {
            [SerializeField]
            private FactCriteriaCondition<int> _condition;

            public IntegerFactCriteriaCondition(FactCriteriaCondition<int> condition)
            {
                _condition = condition;
            }

            public bool Satisfies()
            {
                return ((ICriteriaCondition)_condition).Satisfies();
            }
        }

        private class ValueEqualityCondition : IFactCondition<int>
        {
            [SerializeField]
            private ValueEqualityCondition<int> _condition;

            public ValueEqualityCondition(ValueEqualityCondition<int> condition)
            {
                _condition = condition;
            }

            bool IFactCondition<int>.Satisfies<U>(IFact<U> fact)
            {
                return ((IFactCondition<int>)_condition).Satisfies(fact);
            }
        }

        private class OrderingCondition : IFactCondition<int>
        {
            [SerializeField]
            private OrderingCondition<int> _condition;

            public OrderingCondition(OrderingCondition<int> condition)
            {
                _condition = condition;
            }

            bool IFactCondition<int>.Satisfies<U>(IFact<U> fact)
            {
                return ((IFactCondition<int>)_condition).Satisfies(fact);
            }
        }
    }
}