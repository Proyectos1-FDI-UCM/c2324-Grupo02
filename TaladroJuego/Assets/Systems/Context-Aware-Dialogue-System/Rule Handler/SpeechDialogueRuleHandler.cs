using ContextualDialogueSystem.Rule;
using ContextualDialogueSystem.Rule.Content;
using ContextualDialogueSystem.Rule.Criteria;
using RequireAttributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler
{
    internal class SpeechDialogueRuleHandler : MonoBehaviour, IDialogueRuleHandler<ISpeechContent<string>>
    {
        [RequireInterface(typeof(IDialogueRuleHandler<string>))]
        [SerializeField]
        private Object _sentenceDialogueHandlerObject;
        private IDialogueRuleHandler<string> _sentenceDialogueHandler;

        [SerializeField]
        private bool _appendEmptySentenceAtEnd;

        public async Task<bool> HandleRule(IDialogueRule<ISpeechContent<string>, ICriteria> dialogueRule)
        {
            _sentenceDialogueHandler ??= _sentenceDialogueHandlerObject as IDialogueRuleHandler<string>;
            IEnumerable<string> speechContent = _appendEmptySentenceAtEnd
                                                ? dialogueRule.Content.GetSpeechContent().Append(string.Empty)
                                                : dialogueRule.Content.GetSpeechContent();

            foreach (var message in speechContent)
                await _sentenceDialogueHandler.HandleRule(new DialogueRule<string, ICriteria>(message, dialogueRule.Criteria));

            return true;
        }
    }
}
