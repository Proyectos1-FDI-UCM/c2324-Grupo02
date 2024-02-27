using System.Collections.Generic;
using TMPro;

namespace ContextualDialogueSystem.RuleHandler.TextMeshPro
{
    internal interface ITextMeshProTagHandler
    {
        ISet<string> HandledTags { get; }
        bool TryHandle(TMP_LinkInfo linkInfo);
    }
}