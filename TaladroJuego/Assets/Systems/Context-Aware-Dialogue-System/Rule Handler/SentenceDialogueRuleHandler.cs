using ContextualDialogueSystem.Rule;
using ContextualDialogueSystem.Rule.Criteria;
using ContextualDialogueSystem.RuleHandler.Typewriting;
using RequireAttributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler
{
    internal class SentenceDialogueRuleHandler : MonoBehaviour, IDialogueRuleHandler<string>
    {
        [RequireInterface(typeof(ITypewriter))]
        [SerializeField]
        private Object _typewriterObject;
        private ITypewriter _typewriter;

        [SerializeField]
        private bool _overrideTypewritingOnOverflow;

        [SerializeField]
        private bool _storeTypewritingTaskOnOverflow;

        private readonly Queue<Task> _typewritingTasks = new Queue<Task>();
        private readonly Queue<Task> _ongoingTypewritingTasks = new Queue<Task>();

        public async Task<bool> HandleRule(IDialogueRule<string, ICriteria> dialogueRule)
        {
            _typewriter ??= _typewriterObject as ITypewriter;

            if (_overrideTypewritingOnOverflow)
            {
                while (_ongoingTypewritingTasks.Count > 0)
                    _ongoingTypewritingTasks.Dequeue().Dispose();
            }

            if (_ongoingTypewritingTasks.Count < 1 || _storeTypewritingTaskOnOverflow)
                _typewritingTasks.Enqueue(_typewriter.TypeOver(dialogueRule.Content));

            while (_typewritingTasks.Count > 0)
            {
                Task task = _typewritingTasks.Dequeue();
                _ongoingTypewritingTasks.Enqueue(task);

                await task;
            }

            return true;
        }
    }
}
