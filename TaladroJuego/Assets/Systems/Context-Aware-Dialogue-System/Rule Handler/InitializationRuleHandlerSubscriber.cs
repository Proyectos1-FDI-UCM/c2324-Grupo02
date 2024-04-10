using ContextualDialogueSystem.Event;
using RequireAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler
{
    internal class InitializationRuleHandlerSubscriber : MonoBehaviour
    {
        [SerializeField]
        private ScriptableObject[] _dialogueEventObjects = new ScriptableObject[0];
        private IEnumerable<IObservableDialogueEvent> _dialogueEvents;

        [RequireInterface(typeof(ISubscribableDialogueRuleHandler))]
        [SerializeField]
        private Object _dialogueRuleHandlerObject;
        private ISubscribableDialogueRuleHandler _dialogueRuleHandler;

        [SerializeField]
        private bool _subscribeOnAwake = true;

        [SerializeField]
        private bool _unsubscribeOnDestroy = true;

        private void Awake()
        {
            _dialogueEvents = _dialogueEventObjects.OfType<IObservableDialogueEvent>();
            _dialogueEventObjects = _dialogueEvents.OfType<ScriptableObject>().ToArray();

            _dialogueRuleHandler = _dialogueRuleHandlerObject as ISubscribableDialogueRuleHandler;

            if (_subscribeOnAwake)
                foreach (var dialogueEvent in _dialogueEvents)
                    _dialogueRuleHandler.TrySubscribeToDialogueEvent(dialogueEvent);
        }

        private void OnDestroy()
        {
            if (_unsubscribeOnDestroy)
                foreach (var dialogueEvent in _dialogueEvents)
                    _dialogueRuleHandler.TryUnsubscribeFromDialogueEvent(dialogueEvent);
        }

        private void OnValidate()
        {
            _dialogueEvents = _dialogueEventObjects.OfType<IObservableDialogueEvent>();
            _dialogueEventObjects = _dialogueEvents.OfType<ScriptableObject>().ToArray();

            _dialogueRuleHandler = _dialogueRuleHandlerObject as ISubscribableDialogueRuleHandler;
        }
    }
}
