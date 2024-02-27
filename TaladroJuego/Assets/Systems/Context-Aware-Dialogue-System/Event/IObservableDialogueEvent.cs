
using ContextualDialogueSystem.RuleHandler;

namespace ContextualDialogueSystem.Event
{
    public interface IObservableDialogueEvent
    {
        bool Subscribe<TRuleContent>(IDialogueRuleHandler<TRuleContent> dialogueRuleHandler);
        bool Unsubscribe<TRuleContent>(IDialogueRuleHandler<TRuleContent> dialogueRuleHandler);
    }
}