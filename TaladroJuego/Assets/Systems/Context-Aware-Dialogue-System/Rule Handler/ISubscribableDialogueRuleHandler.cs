using ContextualDialogueSystem.Event;

namespace ContextualDialogueSystem.RuleHandler
{
    public interface ISubscribableDialogueRuleHandler
    {
        bool TrySubscribeToDialogueEvent(IObservableDialogueEvent dialogueEvent);
        bool TryUnsubscribeFromDialogueEvent(IObservableDialogueEvent dialogueEvent);
    }
}
