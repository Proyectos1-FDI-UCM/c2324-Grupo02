using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler.Typewriting
{
    internal class ConstantTypewritingDelayService : MonoBehaviour, ITypewritingDelayService
    {
        [SerializeField]
        [Min(0.0f)]
        private float _speed = 10.0f;

        public float GetDelay(string typedMessage) => 1.0f / _speed;
    }
}
