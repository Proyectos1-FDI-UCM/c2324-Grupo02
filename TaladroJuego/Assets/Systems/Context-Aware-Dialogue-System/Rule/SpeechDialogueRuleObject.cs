using ContextualDialogueSystem.Rule.Content;
using ContextualDialogueSystem.Rule.Criteria;
using ContextualDialogueSystem.Rule.Criteria.Condition;
using ContextualDialogueSystem.Rule.Criteria.Condition.Factory;
using UnityEngine;

namespace ContextualDialogueSystem.Rule
{
    [CreateAssetMenu(fileName = OBJECT_NAME, menuName = OBJECT_PATH)]
    public class SpeechDialogueRuleObject : ScriptableObject, IDialogueRule<ISpeechContent<string>, ICriteria>
    {
        private const string OBJECT_NAME = "Speech Dialogue Rule";
        private const string OBJECT_PATH = "Context-Aware-Dialogue-System/Rule/" + OBJECT_NAME;

        [SerializeField]
        [HideInInspector]
        private SimultaneousCriteria _cachedSimultaneousCriteria;
        [SerializeField]
        private DialogueRule<SpeechContent, ICriteria> _rule;
        private ICriteriaConditionFactory _criteriaConditionFactory;

        public ISpeechContent<string> Content => _rule.Content;
        public ICriteria Criteria => _rule.Criteria;

        [ContextMenu(nameof(ClearCriteria))]
        private void ClearCriteria() => _rule = new DialogueRule<SpeechContent, ICriteria>(_rule.Content, null);

        [ContextMenu(nameof(ClearContent))]
        private void ClearContent() => _rule = new DialogueRule<SpeechContent, ICriteria>(default, _rule.Criteria);

        [ContextMenu(nameof(SetCriteria))]
        private void SetCriteria() => _rule = new DialogueRule<SpeechContent, ICriteria>(_rule.Content, _cachedSimultaneousCriteria);

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
        private void AddCondition() => _cachedSimultaneousCriteria.Conditions.Add(_criteriaConditionFactory.Create());
    }
}
