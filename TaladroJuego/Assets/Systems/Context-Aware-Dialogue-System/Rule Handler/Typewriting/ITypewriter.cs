using System.Threading.Tasks;

namespace ContextualDialogueSystem.RuleHandler.Typewriting
{
    internal interface ITypewriter
    {
        Task Type(string text);
        Task TypeOver(string text);
        Task Delete(int amount);
        Task DeleteAll();
    }
}
