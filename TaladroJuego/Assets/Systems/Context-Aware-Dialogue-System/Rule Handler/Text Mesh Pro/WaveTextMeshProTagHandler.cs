using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler.TextMeshPro
{
    internal class WaveTextMeshProTagHandler : MonoBehaviour, ITextMeshProTagHandler
    {
        [SerializeField]
        private Vector3 _movementAmplitude = Vector2.one;

        [SerializeField]
        private Vector3 _movementFrequency = Vector2.one;

        [SerializeField]
        private Vector3 _characterPhaseOffset = Vector2.one;

        [SerializeField]
        private string[] _handledTags = new string[] { "wave", "w" };

        private HashSet<string> _handledTagsHashSet;
        public ISet<string> HandledTags { get => _handledTagsHashSet ??= _handledTags.ToHashSet(); }

        public bool TryHandle(TMP_LinkInfo linkInfo)
        {
            TMP_Text textComponent = linkInfo.textComponent;

            for (int i = linkInfo.linkTextfirstCharacterIndex; InTagRange(i); i++)
            {
                TMP_CharacterInfo charInfo = textComponent.textInfo.characterInfo[i];
                int materialIndex = charInfo.materialReferenceIndex;

                Vector3[] vertices = textComponent.textInfo.meshInfo[materialIndex].vertices;

                for (int j = 0; j < 4; j++)
                {
                    const char WHITESPACE = ' ';
                    if (charInfo.character == WHITESPACE)
                        continue;

                    int vertexIndex = charInfo.vertexIndex + j;

                    const float TWO_PI = 2.0f * Mathf.PI;
                    Vector3 offset = new Vector3(
                        _movementAmplitude.x * Mathf.Sin(TWO_PI * (_movementFrequency.x * Time.time + _characterPhaseOffset.x * i)),
                        _movementAmplitude.y * Mathf.Sin(TWO_PI * (_movementFrequency.y * Time.time + _characterPhaseOffset.y * i)),
                        _movementAmplitude.z * Mathf.Sin(TWO_PI * (_movementFrequency.z * Time.time + _characterPhaseOffset.z * i)));
                    vertices[vertexIndex] += offset;
                }
            }

            return true;
            bool InTagRange(int characterIndex) =>
                characterIndex >= linkInfo.linkTextfirstCharacterIndex
                && characterIndex < linkInfo.linkTextfirstCharacterIndex + linkInfo.linkTextLength;
        }
    }
}