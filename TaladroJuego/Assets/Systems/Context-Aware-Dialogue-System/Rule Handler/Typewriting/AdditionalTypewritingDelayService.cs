using RequireAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler.Typewriting
{
    internal class AdditionalTypewritingDelayService : MonoBehaviour, ITypewritingDelayService
    {
        [RequireInterface(typeof(ITypewritingDelayService))]
        [SerializeField]
        private Object _typewritingDelayServiceObject;
        private ITypewritingDelayService _typewritingDelayService;

        [SerializeField]
        private bool _relative;

        [SerializeField]
        private Dictionary<char, float> _typewritingDelaysPerTerminalString = new Dictionary<char, float>()
        {
            { ',', 0.5f },

            { ';', 1.0f },
            { ':', 1.0f },

            { '.', 1.5f },
            { '!', 1.5f },
            { '?', 1.5f },
        };

        private void Awake()
        {
            _typewritingDelayService = _typewritingDelayServiceObject as ITypewritingDelayService;
        }

        public float GetDelay(string typedMessage) =>
            !string.IsNullOrEmpty(typedMessage) && _typewritingDelaysPerTerminalString.TryGetValue(typedMessage[^1], out float delay)
            ? _relative
              ? _typewritingDelayService.GetDelay(typedMessage) * delay
              : _typewritingDelayService.GetDelay(typedMessage) + delay
            : _typewritingDelayService.GetDelay(typedMessage);
    }
}
