using System.Threading.Tasks;

namespace ContextualDialogueSystem.RuleHandler.Typewriting
{
    public delegate Task TypingStateFunc(string typedMessage);
}