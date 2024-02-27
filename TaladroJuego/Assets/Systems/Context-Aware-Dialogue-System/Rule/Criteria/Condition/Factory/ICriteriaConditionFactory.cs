namespace ContextualDialogueSystem.Rule.Criteria.Condition.Factory
{
    internal interface ICriteriaConditionFactory
    {
        ICriteriaCondition Create();
        ICriteriaConditionFactory TryConfigureWith<T>(IFactCondition<T> value);
    }
}