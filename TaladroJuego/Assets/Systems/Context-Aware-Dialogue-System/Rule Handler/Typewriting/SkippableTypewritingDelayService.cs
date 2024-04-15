using RequireAttributes;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler.Typewriting
{
    internal class SkippableTypewritingDelayService : MonoBehaviour, ITypewritingDelayService
    {
        [RequireInterface(typeof(ITypewritingDelayService))]
        [SerializeField]
        private Object _typewritingDelayObject;
        private ITypewritingDelayService _typewritingDelayService;

        [RequireInterface(typeof(ITypewritingDelayService))]
        [SerializeField]
        private Object _skipTypewritingDelayObject;
        private ITypewritingDelayService _skipTypewritingDelayService;

        private ITypewritingDelayService _currentTypewritingDelayService;

        private void Awake()
        {
            _typewritingDelayService = _typewritingDelayObject as ITypewritingDelayService;
            _skipTypewritingDelayService = _skipTypewritingDelayObject as ITypewritingDelayService;

            _currentTypewritingDelayService = _typewritingDelayService;
        }

        public SkippableTypewritingDelayService WithBaseService()
        {
            _currentTypewritingDelayService = _typewritingDelayService;
            return this;
        }

        public SkippableTypewritingDelayService WithSkipService()
        {
            _currentTypewritingDelayService = _skipTypewritingDelayService;
            return this;
        }

        public void SetCurrentToBaseService()
        {
            _currentTypewritingDelayService = _typewritingDelayService;
        }

        public void SetCurrentToSkipService()
        {
            _currentTypewritingDelayService = _skipTypewritingDelayService;
        }

        public float GetDelay(string typedMessage) => _currentTypewritingDelayService.GetDelay(typedMessage);
    }
}
