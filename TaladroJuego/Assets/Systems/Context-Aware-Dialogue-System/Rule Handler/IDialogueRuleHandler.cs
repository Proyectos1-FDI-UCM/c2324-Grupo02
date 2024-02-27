using ContextualDialogueSystem.Event;
using ContextualDialogueSystem.Rule;
using ContextualDialogueSystem.Rule.Criteria;
using System.Threading.Tasks;

namespace ContextualDialogueSystem.RuleHandler
{
    public interface IDialogueRuleHandler<in TRuleContent> : ISubscribableDialogueRuleHandler
    {
        bool ISubscribableDialogueRuleHandler.TrySubscribeToDialogueEvent(IObservableDialogueEvent dialogueEvent) =>
            dialogueEvent != null && dialogueEvent.Subscribe(this);

        bool ISubscribableDialogueRuleHandler.TryUnsubscribeFromDialogueEvent(IObservableDialogueEvent dialogueEvent) =>
            dialogueEvent != null && dialogueEvent.Unsubscribe(this);

        Task<bool> HandleRule(IDialogueRule<TRuleContent, ICriteria> dialogueRule);
    }
}
