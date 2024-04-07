using ContextualDialogueSystem.Event;
using ContextualDialogueSystem.Rule;
using ContextualDialogueSystem.Rule.Criteria;
using System.Threading.Tasks;

namespace ContextualDialogueSystem.RuleHandler
{

    public interface IDialogueRuleHandler
    {
        Task<bool> HandleRule<TRuleContent>(IDialogueRule<TRuleContent, ICriteria> dialogueRule);
    }

    public interface IDialogueRuleHandler<in TRuleContent> : ISubscribableDialogueRuleHandler, IDialogueRuleHandler
    {
        bool ISubscribableDialogueRuleHandler.TrySubscribeToDialogueEvent(IObservableDialogueEvent dialogueEvent) =>
            dialogueEvent != null && dialogueEvent.Subscribe(this);

        bool ISubscribableDialogueRuleHandler.TryUnsubscribeFromDialogueEvent(IObservableDialogueEvent dialogueEvent) =>
            dialogueEvent != null && dialogueEvent.Unsubscribe(this);

        Task<bool> IDialogueRuleHandler.HandleRule<URuleContent>(IDialogueRule<URuleContent, ICriteria> dialogueRule) =>
            dialogueRule is IDialogueRule<TRuleContent, ICriteria> rule ? HandleRule(rule) : Task.FromResult(false);

        Task<bool> HandleRule(IDialogueRule<TRuleContent, ICriteria> dialogueRule);
    }
}
