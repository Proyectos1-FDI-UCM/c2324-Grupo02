using ContextualDialogueSystem.Rule.Criteria.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ContextualDialogueSystem.Rule.Criteria
{
    [Serializable]
    internal class SimultaneousCriteria : ICriteria
    {
        [SerializeReference]
        private List<ICriteriaCondition> _conditions;
        public IList<ICriteriaCondition> Conditions => _conditions;

        public SimultaneousCriteria(IEnumerable<ICriteriaCondition> conditions)
        {
            _conditions = new List<ICriteriaCondition>(conditions);
        }

        public SimultaneousCriteria(params ICriteriaCondition[] conditions) : this(conditions.AsEnumerable())
        {
        }

        public SimultaneousCriteria() : this(Enumerable.Empty<ICriteriaCondition>())
        {
        }

        public bool IsMet() => Conditions.All(condition => condition.Satisfies());
    }
}