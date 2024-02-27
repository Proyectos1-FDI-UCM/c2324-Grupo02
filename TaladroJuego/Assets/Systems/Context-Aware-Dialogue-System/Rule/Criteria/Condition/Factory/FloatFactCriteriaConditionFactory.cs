using ContextualDialogueSystem.Fact;
using System;
using UnityEngine;

namespace ContextualDialogueSystem.Rule.Criteria.Condition.Factory
{
    internal class FloatFactCriteriaConditionFactory : ICriteriaConditionFactory
    {
        private readonly IFactCondition<float> _condition;

        public FloatFactCriteriaConditionFactory(IFactCondition<float> condition = null)
        {
            _condition = condition;
        }

        public ICriteriaCondition Create() => _condition switch
        {
            ValueEqualityCondition<float> condition => new FloatFactCriteriaCondition(new FactCriteriaCondition<float>(null, new ValueEqualityCondition(condition))),
            OrderingCondition<float> condition => new FloatFactCriteriaCondition(new FactCriteriaCondition<float>(null, new OrderingCondition(condition))),
            _ => throw new NotImplementedException()
        };

        public ICriteriaConditionFactory TryConfigureWith<T>(IFactCondition<T> value)
        {
            if (value is IFactCondition<float> condition)
                return new FloatFactCriteriaConditionFactory(condition);
            return this;
        }

        private class FloatFactCriteriaCondition : ICriteriaCondition
        {
            [SerializeField]
            private FactCriteriaCondition<float> _condition;

            public FloatFactCriteriaCondition(FactCriteriaCondition<float> condition)
            {
                _condition = condition;
            }

            public bool Satisfies()
            {
                return ((ICriteriaCondition)_condition).Satisfies();
            }
        }

        private class ValueEqualityCondition : IFactCondition<float>
        {
            [SerializeField]
            private ValueEqualityCondition<float> _condition;

            public ValueEqualityCondition(ValueEqualityCondition<float> condition)
            {
                _condition = condition;
            }

            bool IFactCondition<float>.Satisfies<U>(IFact<U> fact)
            {
                return ((IFactCondition<float>)_condition).Satisfies(fact);
            }
        }
        
        private class OrderingCondition : IFactCondition<float>
        {
            [SerializeField]
            private OrderingCondition<float> _condition;

            public OrderingCondition(OrderingCondition<float> condition)
            {
                _condition = condition;
            }

            bool IFactCondition<float>.Satisfies<U>(IFact<U> fact)
            {
                return ((IFactCondition<float>)_condition).Satisfies(fact);
            }
        }
    }
}