using ContextualDialogueSystem.Rule;
using ContextualDialogueSystem.Rule.Criteria;
using System.Threading.Tasks;

namespace ContextualDialogueSystem.RuleHandler
{
    internal static class DialogueRuleHandlerExtensions
    {
        public static Task<bool> HandleRule<TRuleContent>(this IDialogueRuleHandler<TRuleContent> dialogueRuleHandler, IDialogueRule<object, ICriteria> dialogueRule) =>
            dialogueRule is IDialogueRule<TRuleContent, ICriteria> rule
            ? dialogueRuleHandler.HandleRule(rule)
            : Task.FromResult(false);
    }
}
