using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler
{
    internal class InterpolatedStringParser : MonoBehaviour, IStringParser
    {
        private static readonly string s_DefaultPattern = @"(?<=\{)[^}]*(?=\})";

        [SerializeField]
        [TextArea]
        private string _regexPattern = s_DefaultPattern;
        private Regex _regex = null;
        private Regex Regex
        {
            get => _regex ??= OverrideRegex ?? new Regex(_regexPattern);
            set => _regex = value;
        }
        public Regex OverrideRegex { private get; set; } = null;
        public Dictionary<string, string> PatternSubstitutions { get; private set; } = new Dictionary<string, string>();

        public string Parse(string text)
        {
            Regex regex = Regex;
            MatchCollection matches = regex.Matches(text);

            foreach (Match match in matches.Cast<Match>())
            {
                string key = match.Value;
                if (PatternSubstitutions.TryGetValue(key, out string value))
                    text = text.Replace($"{{{key}}}", value);
            }
            return text;
        }
    }
}
