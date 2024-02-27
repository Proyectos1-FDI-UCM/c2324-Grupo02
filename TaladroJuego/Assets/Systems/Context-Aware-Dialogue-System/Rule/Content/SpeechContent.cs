using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ContextualDialogueSystem.Rule.Content
{
    [Serializable]
    internal struct SpeechContent : ISpeechContent<string>
    {
        [Serializable]
        private struct TextString
        {
            [field: SerializeField]
            [field: TextArea]
            public string Text { get; private set; }
        }

        [SerializeField]
        private TextString[] _textStrings;

        public IEnumerable<string> GetSpeechContent() => _textStrings.Select(textString => textString.Text);
    }
}
