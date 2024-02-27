using RequireAttributes;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler.Typewriting
{
    internal class TextMeshTypewriter : MonoBehaviour, ITypewriter
    {
        [SerializeField]
        private TMP_Text _textMesh;

        [RequireInterface(typeof(ITypewriter))]
        [SerializeField]
        private Object _typewriterObject;
        private ITypewriter _typewriter;

        [RequireInterface(typeof(IObservableTypewriter))]
        [SerializeField]
        private Object _observableTypewriterObject;
        private IObservableTypewriter _observableTypewriter;

        private void Awake()
        {
            _typewriter = _typewriterObject as ITypewriter;

            _observableTypewriter = _observableTypewriterObject as IObservableTypewriter;
            _observableTypewriter.CharacterTyped += OnCharacterTyped;
        }

        private void OnDestroy() => _observableTypewriter.CharacterTyped -= OnCharacterTyped;

        private Task OnCharacterTyped(string typedMessage)
        {
            _textMesh.maxVisibleCharacters = typedMessage.Length;
            return Task.CompletedTask;
        }

        public Task Type(string text)
        {
            _textMesh.text += text;
            return _typewriter.Type(text);
        }

        public Task TypeOver(string text)
        {
            _textMesh.text = text;
            return _typewriter.TypeOver(text);
        }

        public Task Delete(int amount)
        {
            _textMesh.text = _textMesh.text.Remove(_textMesh.text.Length - amount);
            return _typewriter.Delete(amount);
        }

        public Task DeleteAll()
        {
            _textMesh.text = string.Empty;
            return _typewriter.DeleteAll();
        }
    }
}
