using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GraphicsColorChanger : MonoBehaviour
{
    [SerializeField] private Graphic _graphic;
    [SerializeField] private ColorChangeRequesterObject[] _colorChangeRequestObjects;
    [SerializeField] private bool _overrideOncolorChangeRequestOverflow;
    private Coroutine _colorChangeCoroutine;

    [field: SerializeField] public UnityEvent OnColorChangeStarted { get; private set; }
    [field: SerializeField] public UnityEvent OnColorChangeEnded { get; private set; }

    private void Awake()
    {
        foreach (ColorChangeRequesterObject colorChangeRequestObject in _colorChangeRequestObjects)
            colorChangeRequestObject.OnColorChangeRequested.AddListener(OnColorChangeRequested);
    }

    private void OnDestroy()
    {
        foreach (ColorChangeRequesterObject colorChangeRequestObject in _colorChangeRequestObjects)
            colorChangeRequestObject.OnColorChangeRequested.RemoveListener(OnColorChangeRequested);
    }

    private void OnColorChangeRequested(ColorChangeRequesterObject.ColorChangeData colorChangeData)
    {
        if (_overrideOncolorChangeRequestOverflow)
            StopcolorChange();
        _colorChangeCoroutine ??= StartCoroutine(ColorChangeCoroutine(colorChangeData));
    }

    private IEnumerator ColorChangeCoroutine(ColorChangeRequesterObject.ColorChangeData colorChangeData)
    {
        yield return new WaitForSeconds(colorChangeData.colorChangeDelay);
        Color initialColor = _graphic.color;
        OnColorChangeStarted?.Invoke();

        for (float t = 0; t < colorChangeData.colorChangeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / colorChangeData.colorChangeDuration;
            float shapedTime = colorChangeData.colorChangeCurve.Evaluate(normalizedTime);

            Color color = Color.Lerp(initialColor, colorChangeData.color, shapedTime);
            _graphic.color = color;

            yield return null;
        }

        _graphic.color = colorChangeData.color;
        _colorChangeCoroutine = null;
        OnColorChangeEnded?.Invoke();
    }

    public void StopcolorChange()
    {
        if (_colorChangeCoroutine == null) return;

        StopCoroutine(_colorChangeCoroutine);
        _colorChangeCoroutine = null;
    }
}