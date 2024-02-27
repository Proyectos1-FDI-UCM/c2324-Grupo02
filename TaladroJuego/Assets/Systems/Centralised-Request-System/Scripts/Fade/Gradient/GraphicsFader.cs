using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GraphicsFader : MonoBehaviour
{
    [SerializeField] private Graphic _graphic;
    [SerializeField] private FadeRequesterObject[] _fadeRequestObjects;
    [SerializeField] private bool _overrideOnFadeRequestOverflow;
    private Coroutine _fadeCoroutine;

    [field: SerializeField] public UnityEvent OnFadeStarted { get; private set; }
    [field: SerializeField] public UnityEvent OnFadeEnded { get; private set; }

    private void Awake()
    {
        foreach (FadeRequesterObject fadeRequestObject in _fadeRequestObjects)
            fadeRequestObject.OnFadeRequested.AddListener(OnFadeRequested);
    }

    private void OnDestroy()
    {
        foreach (FadeRequesterObject fadeRequestObject in _fadeRequestObjects)
            fadeRequestObject.OnFadeRequested.RemoveListener(OnFadeRequested);
    }

    private void OnFadeRequested(FadeRequesterObject.FadeData fadeData)
    {
        if (_overrideOnFadeRequestOverflow)
            StopFade();
        _fadeCoroutine ??= StartCoroutine(FadeCoroutine(fadeData));
    }

    private IEnumerator FadeCoroutine(FadeRequesterObject.FadeData fadeData)
    {
        yield return new WaitForSeconds(fadeData.fadeDelay);
        OnFadeStarted?.Invoke();

        for (float t = 0; t < fadeData.fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeData.fadeDuration;
            float shapedTime = fadeData.fadeCurve.Evaluate(normalizedTime);

            Color color = fadeData.fadeGradient.Evaluate(shapedTime);
            _graphic.color = color;

            yield return null;
        }

        _graphic.color = fadeData.fadeGradient.Evaluate(1.0f);
        _fadeCoroutine = null;
        OnFadeEnded?.Invoke();
    }

    public void StopFade()
    {
        if (_fadeCoroutine == null) return;

        StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = null;
    }
}
