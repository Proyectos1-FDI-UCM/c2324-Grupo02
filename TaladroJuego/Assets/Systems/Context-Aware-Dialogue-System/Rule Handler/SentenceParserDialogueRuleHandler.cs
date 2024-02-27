using ContextualDialogueSystem.Rule;
using ContextualDialogueSystem.Rule.Criteria;
using RequireAttributes;
using System.Threading.Tasks;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler
{
    internal class SentenceParserDialogueRuleHandler : MonoBehaviour, IDialogueRuleHandler<string>
    {
        [RequireInterface(typeof(IStringParser))]
        [SerializeField]
        private Object _stringParserObject;
        private IStringParser _stringParser;

        [RequireInterface(typeof(IDialogueRuleHandler<string>))]
        [SerializeField]
        private Object _sentenceDialogueHandlerObject;
        private IDialogueRuleHandler<string> _sentenceDialogueHandler;

        public Task<bool> HandleRule(IDialogueRule<string, ICriteria> dialogueRule)
        {
            _sentenceDialogueHandler ??= _sentenceDialogueHandlerObject as IDialogueRuleHandler<string>;
            _stringParser ??= _stringParserObject as IStringParser ?? new UnitStringParser();

            return _sentenceDialogueHandler.HandleRule(new DialogueRule<string, ICriteria>(_stringParser.Parse(dialogueRule.Content), dialogueRule.Criteria));
        }

        private class UnitStringParser : IStringParser
        {
            public string Parse(string content) => content;
        }
    }
}
