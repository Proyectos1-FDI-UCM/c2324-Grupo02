using ContextualDialogueSystem.Rule.Criteria.Condition;
using ContextualDialogueSystem.Rule.Criteria.Condition.Factory;
using UnityEngine;

namespace ContextualDialogueSystem.Rule.Criteria
{
    [CreateAssetMenu(fileName = OBJECT_NAME, menuName = OBJECT_PATH)]
    public class CriteriaObject : ScriptableObject, ICriteria
    {
        private const string OBJECT_NAME = "Criteria Object";
        private const string OBJECT_PATH = "Context-Aware-Dialogue-System/Rule/Criteria/" + OBJECT_NAME;

        [SerializeField]
        private SimultaneousCriteria _simultaneousCriteria;
        private ICriteriaConditionFactory _criteriaConditionFactory;

        public bool IsMet() => _simultaneousCriteria.IsMet();

        [ContextMenu(nameof(SetCriteriaFactoryToInteger))]
        private void SetCriteriaFactoryToInteger() => _criteriaConditionFactory = new IntegerFactCriteriaConditionFactory();

        [ContextMenu(nameof(SetCriteriaFactoryToFloat))]
        private void SetCriteriaFactoryToFloat() => _criteriaConditionFactory = new FloatFactCriteriaConditionFactory();

        [ContextMenu(nameof(SetCriteriaFactoryToString))]
        private void SetCriteriaFactoryToString() => _criteriaConditionFactory = new StringFactCriteriaConditionFactory();

        [ContextMenu(nameof(SetFactConditionToValueEquality))]
        private void SetFactConditionToValueEquality() =>
            _criteriaConditionFactory = _criteriaConditionFactory
                .TryConfigureWith(new ValueEqualityCondition<int>(default))
                .TryConfigureWith(new ValueEqualityCondition<float>(default))
                .TryConfigureWith(new ValueEqualityCondition<string>(string.Empty));

        [ContextMenu(nameof(SetFactConditionToOrdering))]
        private void SetFactConditionToOrdering() =>
            _criteriaConditionFactory = _criteriaConditionFactory
                .TryConfigureWith(new OrderingCondition<int>(default, default))
                .TryConfigureWith(new OrderingCondition<float>(default, default))
                .TryConfigureWith(new OrderingCondition<string>(string.Empty, default));

        [ContextMenu(nameof(AddCondition))]
        private void AddCondition() => _simultaneousCriteria.Conditions.Add(_criteriaConditionFactory.Create());
    }
}