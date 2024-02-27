using UnityEngine;
using UnityEngine.InputSystem;

namespace ContextualDialogueSystem.RuleHandler
{
    internal class InputBindingsStringParser : MonoBehaviour, IStringParser
    {
        [SerializeField]
        private InterpolatedStringParser _interpolatedStringParser;

        [SerializeField]
        private InputBinding.DisplayStringOptions _displayStringOptions;

        [SerializeField]
        private InputActionReference[] _inputActions;

        public string Parse(string text)
        {
            foreach (var inputAction in _inputActions)
                _interpolatedStringParser.PatternSubstitutions[inputAction.action.name] = inputAction.action.GetBindingDisplayString(_displayStringOptions);

            return _interpolatedStringParser.Parse(text);
        }
    }
}
