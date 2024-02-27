using RequireAttributes;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ContextualDialogueSystem.RuleHandler.TextMeshPro
{
    internal class PeriodicTextMeshProTagEffectApplier : MonoBehaviour
    {
        [SerializeField]
        [Min(float.Epsilon)]
        private float _effectApplicationFrequency = 30.0f;

        [SerializeField]
        private TMP_Text _textMeshProText;

        [RequireInterface(typeof(ITextMeshProTagHandler))]
        [SerializeField]
        private Object _tagHandlerObject;
        private ITextMeshProTagHandler _tagHandler;

        [SerializeField]
        private bool _caseInsensitiveTagHandling = true;

        [SerializeField]
        private bool _applyEffectOnAwake = true;

        private Coroutine _tagEffectCoroutine;

        private void Awake()
        {
            _tagHandler = _tagHandlerObject as ITextMeshProTagHandler;
            if (_applyEffectOnAwake)
                TryStartTagEffectCoroutine();
        }

        public bool TryStartTagEffectCoroutine()
        {
            if (_tagEffectCoroutine != null)
                return false;

            _tagEffectCoroutine = StartCoroutine(TagEffectCoroutine());
            return true;
        }

        public bool TryStopTagEffectCoroutine()
        {
            if (_tagEffectCoroutine == null)
                return false;

            StopCoroutine(_tagEffectCoroutine);
            _tagEffectCoroutine = null;
            return true;
        }

        private IEnumerator TagEffectCoroutine()
        {
            while (enabled)
            {
                ApplyTagEffect();
                yield return new WaitForSeconds(1.0f / _effectApplicationFrequency);
            }
        }

        private void ApplyTagEffect()
        {
            // TODO - 
            _textMeshProText.ForceMeshUpdate(forceTextReparsing: true);

            foreach (TMP_LinkInfo link in _textMeshProText.textInfo.linkInfo)
            {
                if (_tagHandler.HandledTags.Contains(_caseInsensitiveTagHandling ? link.GetLinkID().ToLower() : link.GetLinkID()))
                    _tagHandler.TryHandle(link);
            }

            _textMeshProText.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }
    }
}
