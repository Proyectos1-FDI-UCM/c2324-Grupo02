using ContextualDialogueSystem.Rule;
using ContextualDialogueSystem.Rule.Criteria;
using System.Threading.Tasks;

namespace ContextualDialogueSystem.Event
{
    public delegate Task<bool> RuleDispatch<in TRuleContent>(IDialogueRule<TRuleContent, ICriteria> dialogueRule);
}