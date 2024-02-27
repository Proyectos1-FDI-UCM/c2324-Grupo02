namespace ContextualDialogueSystem.Fact
{
    internal interface IObservableFact<T>
    {
        event FactValueAction<T> ValueSet;
    }
}