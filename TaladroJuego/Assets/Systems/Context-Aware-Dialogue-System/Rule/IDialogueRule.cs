using ContextualDialogueSystem.Rule.Criteria;

namespace ContextualDialogueSystem.Rule
{
    public interface IDialogueRule<out TContent, out TCriteria>
        where TCriteria : ICriteria
    {
        TContent Content { get; }
        TCriteria Criteria { get; }
    }
}
