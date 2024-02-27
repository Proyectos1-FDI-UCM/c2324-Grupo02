using ContextualDialogueSystem.RuleHandler;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ContextualDialogueSystem.Event
{
    [CreateAssetMenu(fileName = OBJECT_NAME, menuName = OBJECT_PATH)]
    public class EventBinderObject : ScriptableObject, IObservableDialogueEvent, IDispatchableDialogueEvent
    {
        private const string OBJECT_NAME = "Event Binder Object";
        private const string OBJECT_PATH = "Context-Aware-Dialogue-System/Event/" + OBJECT_NAME;

        [SerializeField]
        private ScriptableObject[] _dialogueEventObjects;
        private IEnumerable<IObservableDialogueEvent> _dialogueEvents;

        [ContextMenu(nameof(Dispatch))]
        public void Dispatch()
        {
            foreach (var dialogueEvent in _dialogueEventObjects.OfType<IDispatchableDialogueEvent>())
                dialogueEvent.Dispatch();
        }

        public bool Subscribe<TRuleContent>(IDialogueRuleHandler<TRuleContent> dialogueRuleHandler)
        {
            _dialogueEvents = _dialogueEventObjects.OfType<IObservableDialogueEvent>();
            bool success = true;

            foreach (var dialogueEvent in _dialogueEvents)
                success &= dialogueEvent.Subscribe(dialogueRuleHandler);

            return success;
        }

        public bool Unsubscribe<TRuleContent>(IDialogueRuleHandler<TRuleContent> dialogueRuleHandler)
        {
            _dialogueEvents = _dialogueEventObjects.OfType<IObservableDialogueEvent>();
            bool success = true;

            foreach (var dialogueEvent in _dialogueEvents)
                success &= dialogueEvent.Unsubscribe(dialogueRuleHandler);

            return success;
        }
    }
}