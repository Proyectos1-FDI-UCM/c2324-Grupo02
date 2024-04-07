using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusSystem;

internal class OnStatusUpdateAnimator : MonoBehaviour
{
    [SerializeField] private StatusParameter _statusParameter;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Color _hitColor;
    [SerializeField] private Color _healColor;

    private float _lastStatusValue;
    private Color _baseSpriteColor;

    private IEnumerator PlayAnimation(Color transitionColor)
    {
        _spriteRenderer.color = Color.Lerp(_baseSpriteColor, transitionColor, 0.8f * Time.fixedDeltaTime);
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = Color.Lerp(transitionColor, _baseSpriteColor, 0.8f * Time.fixedDeltaTime);
        yield return new WaitForSeconds(0.2f);
    }

    public void HandleStatusChange()
    {
        StartCoroutine(PlayAnimation(_hitColor));
    }

    void Start()
    {
        _lastStatusValue = _statusParameter.Value;
        _baseSpriteColor = _spriteRenderer.color;
    }

    void Update()
    {
        
    }
}
