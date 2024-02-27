using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = FADE_REQUEST_OBJECT_NAME, menuName = FADE_REQUEST_OBJECT_PATH)]
public class FadeRequesterObject : ScriptableObject
{
    private const string FADE_REQUEST_OBJECT_NAME = "Fade Requester";
    private const string FADE_REQUEST_OBJECT_PATH = "UI State/" + FADE_REQUEST_OBJECT_NAME;

    [Serializable]
    public struct FadeData
    {
        public float fadeDuration;
        public float fadeDelay;
        public Gradient fadeGradient;
        public AnimationCurve fadeCurve;
    }

    [SerializeField] private FadeData _fadeData = new FadeData()
    {
        fadeDuration = 1.0f,
        fadeDelay = 0.0f,
        fadeGradient = new Gradient(),
        fadeCurve = AnimationCurve.Linear(0, 0, 1, 1)
    };

    [field: SerializeField] public UnityEvent<FadeData> OnFadeRequested { get; private set; }
    public void RequestFade() => OnFadeRequested?.Invoke(_fadeData);
}
