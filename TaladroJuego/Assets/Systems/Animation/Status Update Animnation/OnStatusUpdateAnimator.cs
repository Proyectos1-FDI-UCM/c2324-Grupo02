using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusSystem;

namespace AnimationSystem.Status
{
    internal class OnStatusUpdateAnimator : MonoBehaviour
    {
        [SerializeField] private StatusParameter _statusParameter;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private Color _hitColor;
        [SerializeField] private Color _healColor;

        private float _lastStatusValue;
        private Color _baseSpriteColor;

        void Start()
        {
            _lastStatusValue = _statusParameter.Value;
            _baseSpriteColor = _spriteRenderer.color;
        }

        private IEnumerator PlayAnimation(Color transitionColor)
        {
            _spriteRenderer.color = Color.Lerp(_baseSpriteColor, transitionColor, 1);
            yield return new WaitForSeconds(0.2f);
            _spriteRenderer.color = Color.Lerp(transitionColor, _baseSpriteColor, 1);
            yield return new WaitForSeconds(0.2f);
        }

        public void HandleStatusChange()
        {
            Color color;
            float value = _statusParameter.Value;

            if (_lastStatusValue > value) color = _hitColor;
            else color = _healColor;

            _lastStatusValue = value;

            StartCoroutine(PlayAnimation(color));
        }
    }

}
