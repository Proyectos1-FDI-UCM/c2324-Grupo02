using ContextualDialogueSystem.Rule;
using ContextualDialogueSystem.Rule.Criteria;
using ContextualDialogueSystem.RuleHandler;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ContextualDialogueSystem.Event
{
    [CreateAssetMenu(fileName = OBJECT_NAME, menuName = OBJECT_PATH)]
    public class DialogueEventObject : ScriptableObject, IDispatchableDialogueEvent, IObservableDialogueEvent
    {
        private const string OBJECT_NAME = "Dialogue Event Object";
        private const string OBJECT_PATH = "Context-Aware-Dialogue-System/Event/" + OBJECT_NAME;

        [SerializeField]
        private ScriptableObject[] _dialogueRuleObjects;
        private IEnumerable<IDialogueRule<object, ICriteria>> _dialogueRules;

        public event RuleDispatch<object> RuleDispatched;

        [ContextMenu(nameof(Dispatch))]
        public void Dispatch()
        {
            _dialogueRules = _dialogueRuleObjects.OfType<IDialogueRule<object, ICriteria>>();

            foreach (var rule in _dialogueRules)
                if (rule.Criteria.IsMet())
                    RuleDispatched?.Invoke(rule);
        }

        public bool Subscribe<TRuleContent>(IDialogueRuleHandler<TRuleContent> dialogueRuleHandler)
        {
            RuleDispatched += dialogueRuleHandler.HandleRule<TRuleContent>;
            return true;
        }

        public bool Unsubscribe<TRuleContent>(IDialogueRuleHandler<TRuleContent> dialogueRuleHandler)
        {
            RuleDispatched -= dialogueRuleHandler.HandleRule<TRuleContent>;
            return true;
        }
    }
}