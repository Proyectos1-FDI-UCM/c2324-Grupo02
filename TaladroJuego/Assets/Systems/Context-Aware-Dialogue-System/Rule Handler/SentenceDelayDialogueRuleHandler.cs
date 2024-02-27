using ContextualDialogueSystem.Rule;
using ContextualDialogueSystem.Rule.Criteria;
using RequireAttributes;
using System.Threading.Tasks;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler
{
    internal class SentenceDelayDialogueRuleHandler : MonoBehaviour, IDialogueRuleHandler<string>
    {
        private const int SECONDS_TO_MILLISECONDS = 1000;

        [RequireInterface(typeof(IDialogueRuleHandler<string>))]
        [SerializeField]
        private Object _sentenceDialogueHandlerObject;
        private IDialogueRuleHandler<string> _sentenceDialogueHandler;

        [SerializeField]
        [Min(0.0f)]
        private float _beforeSentenceDelay = 0.0f;

        [SerializeField]
        [Min(0.0f)]
        private float _afterSentenceDelay = 2.0f;

        public async Task<bool> HandleRule(IDialogueRule<string, ICriteria> dialogueRule)
        {
            _sentenceDialogueHandler ??= _sentenceDialogueHandlerObject as IDialogueRuleHandler<string>;

            await Task.Delay((int)(_beforeSentenceDelay * SECONDS_TO_MILLISECONDS));
            await _sentenceDialogueHandler.HandleRule(dialogueRule);
            await Task.Delay((int)(_afterSentenceDelay * SECONDS_TO_MILLISECONDS));

            return true;
        }
    }
}
