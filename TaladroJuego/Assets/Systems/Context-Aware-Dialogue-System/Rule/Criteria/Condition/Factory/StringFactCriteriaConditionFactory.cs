using ContextualDialogueSystem.Fact;
using System;
using UnityEngine;

namespace ContextualDialogueSystem.Rule.Criteria.Condition.Factory
{
    internal class StringFactCriteriaConditionFactory : ICriteriaConditionFactory
    {
        private readonly IFactCondition<string> _condition;

        public StringFactCriteriaConditionFactory(IFactCondition<string> condition = null)
        {
            _condition = condition;
        }

        public ICriteriaCondition Create() => _condition switch
        {
            ValueEqualityCondition<string> condition => new StringFactCriteriaCondition(new FactCriteriaCondition<string>(null, new ValueEqualityCondition(condition))),
            _ => throw new NotImplementedException()
        };

        public ICriteriaConditionFactory TryConfigureWith<T>(IFactCondition<T> value)
        {
            if (value is IFactCondition<string> condition)
                return new StringFactCriteriaConditionFactory(condition);
            return this;
        }

        private class StringFactCriteriaCondition : ICriteriaCondition
        {
            [SerializeField]
            private FactCriteriaCondition<string> _condition;

            public StringFactCriteriaCondition(FactCriteriaCondition<string> condition)
            {
                _condition = condition;
            }

            public bool Satisfies()
            {
                return ((ICriteriaCondition)_condition).Satisfies();
            }
        }
        
        private class ValueEqualityCondition : IFactCondition<string>
        {
            [SerializeField]
            private ValueEqualityCondition<string> _condition;

            public ValueEqualityCondition(ValueEqualityCondition<string> condition)
            {
                _condition = condition;
            }

            bool IFactCondition<string>.Satisfies<U>(IFact<U> fact)
            {
                return ((IFactCondition<IEquatable<string>>)_condition).Satisfies(fact);
            }
        }
    }
}