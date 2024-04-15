using ContextualDialogueSystem.Rule;
using ContextualDialogueSystem.Rule.Criteria;
using RequireAttributes;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace ContextualDialogueSystem.RuleHandler
{
    [Obsolete]
    internal class EventForwardingSentenceDelayDialogueRuleHandler : MonoBehaviour, IDialogueRuleHandler<string>
    {
        [RequireInterface(typeof(IDialogueRuleHandler<string>))]
        [SerializeField]
        private Object _sentenceDialogueHandlerObject;
        private IDialogueRuleHandler<string> _sentenceDialogueHandler;

        [field: SerializeField]
        public UnityEvent SentenceHandlingStarted { get; private set; }

        [field: SerializeField]
        public UnityEvent SentenceHandlingFinished { get; private set; }

        private void Awake()
        {
            _sentenceDialogueHandler = _sentenceDialogueHandlerObject as IDialogueRuleHandler<string>;
        }


        public async Task<bool> HandleRule(IDialogueRule<string, ICriteria> dialogueRule)
        {
            SentenceHandlingStarted?.Invoke();
            bool success = await _sentenceDialogueHandler.HandleRule(dialogueRule);
            SentenceHandlingFinished?.Invoke();
            return success;
        }
    }
}
