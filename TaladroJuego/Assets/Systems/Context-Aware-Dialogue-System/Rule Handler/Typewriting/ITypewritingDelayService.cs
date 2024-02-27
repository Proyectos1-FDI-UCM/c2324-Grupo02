namespace ContextualDialogueSystem.RuleHandler.Typewriting
{
    internal interface ITypewritingDelayService
    {
        float GetDelay(string typedMessage);
    }
}
