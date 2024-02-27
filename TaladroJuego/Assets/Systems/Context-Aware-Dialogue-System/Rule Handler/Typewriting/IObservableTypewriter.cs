namespace ContextualDialogueSystem.RuleHandler.Typewriting
{
    internal interface IObservableTypewriter
    {
        event TypingStateFunc CharacterTyped;
    }
}
