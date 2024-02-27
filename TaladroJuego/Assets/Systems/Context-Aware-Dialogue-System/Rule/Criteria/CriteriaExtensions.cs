using ContextualDialogueSystem.Fact;
using ContextualDialogueSystem.Rule.Criteria.Condition;
using System;

namespace ContextualDialogueSystem.Rule.Criteria
{
    internal static class CriteriaExtensions
    {
        public class IntegerFactCriteriaCondition : FactCriteriaCondition<int>
        {
            public IntegerFactCriteriaCondition(IFact<int> fact, IFactCondition<int> condition) : base(fact, condition)
            {
            }
        }

        public class IntegerOrderingCondition : OrderingCondition<int>
        {
            public IntegerOrderingCondition(int value, OrderingComparison comparison) : base(value, comparison)
            {
            }
        }

        public class IntegerValueEqualityCondition : ValueEqualityCondition<int>
        {
            public IntegerValueEqualityCondition(int value) : base(value)
            {
            }
        }
    }
}
