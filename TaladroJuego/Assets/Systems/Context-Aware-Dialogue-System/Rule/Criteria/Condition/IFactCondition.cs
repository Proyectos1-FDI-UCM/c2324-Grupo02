using ContextualDialogueSystem.Fact;

namespace ContextualDialogueSystem.Rule.Criteria.Condition
{
    internal interface IFactCondition
    {
        bool Satisfies<T>(IFact<T> fact);
    }

    internal interface IFactCondition<in T> : IFactCondition
    {
        bool IFactCondition.Satisfies<U>(IFact<U> fact) =>
            fact is IFact<T> typedFact
            && Satisfies(typedFact);

        new bool Satisfies<U>(IFact<U> fact)
            where U : T;
    }
}