using ContextualDialogueSystem.Fact;
using System;
using UnityEngine;

namespace ContextualDialogueSystem.Rule.Criteria.Condition
{
    [Serializable]
    internal class FactCriteriaCondition<T> : ICriteriaCondition
    {
        [SerializeField]
        private ScriptableObject _factObject;

        private IFact<T> _fact;
        public IFact<T> Fact 
        { 
            get => _fact ?? _factObject as IFact<T>; 
            private set => _fact = value;
        }

        [field: SerializeReference]
        public IFactCondition Condition { private get; set; }

        public FactCriteriaCondition(IFact<T> fact, IFactCondition<T> condition)
        {
            Fact = fact;
            Condition = condition;
        }

        public bool Satisfies() => Condition.Satisfies(Fact);
    }
}