using RequireAttributes;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler.Typewriting
{
    internal class Typewriter : MonoBehaviour, ITypewriter, IObservableTypewriter
    {
        private const int SECONDS_TO_MILLISECONDS = 1000;
        private readonly static TypingStateFunc s_DefaultTypingStateFunc = _ => Task.CompletedTask;
        [RequireInterface(typeof(ITypewritingDelayService))]
        [SerializeField]
        private Object _typewritingDelayObject;
        private ITypewritingDelayService _typewritingDelayService;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float _delaySplitPoint = 0.5f;
        private readonly StringBuilder _typedMessageBuilder = new StringBuilder();

        public event TypingStateFunc CharacterTyped = s_DefaultTypingStateFunc;

        private void Awake()
        {
            _typewritingDelayService = _typewritingDelayObject as ITypewritingDelayService ?? new UnitTypewritingDelayService();
        }

        public async Task Delete(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                float delay = _typewritingDelayService.GetDelay(_typedMessageBuilder.ToString());
                float preTypeDelay = delay * _delaySplitPoint;
                float postTypeDelay = delay - preTypeDelay;

                await Task.Delay((int)(preTypeDelay * SECONDS_TO_MILLISECONDS));

                _typedMessageBuilder.Remove(_typedMessageBuilder.Length - 1, 1);
                await CharacterTyped.Invoke(_typedMessageBuilder.ToString());

                await Task.Delay((int)(postTypeDelay * SECONDS_TO_MILLISECONDS));
            }
        }

        public async Task DeleteAll() => await Delete(_typedMessageBuilder.Length);

        public async Task Type(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                float delay = _typewritingDelayService.GetDelay(_typedMessageBuilder.ToString());
                float preTypeDelay = delay * _delaySplitPoint;
                float postTypeDelay = delay - preTypeDelay;

                await Task.Delay((int)(preTypeDelay * SECONDS_TO_MILLISECONDS));

                _typedMessageBuilder.Append(text[i]);
                await CharacterTyped.Invoke(_typedMessageBuilder.ToString());

                await Task.Delay((int)(postTypeDelay * SECONDS_TO_MILLISECONDS));
            }
        }

        public async Task TypeOver(string text)
        {
            _typedMessageBuilder.Clear();
            await CharacterTyped.Invoke(_typedMessageBuilder.ToString());

            await Type(text);
        }

        private class UnitTypewritingDelayService : ITypewritingDelayService
        {
            public float GetDelay(string typedMessage) => 0.0f;
        }
    }
}
