using System.Collections.Generic;

namespace ContextualDialogueSystem.Rule.Content
{
    public interface ISpeechContent<out T>
    {
        IEnumerable<T> GetSpeechContent();
    }
}
