using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = COLOR_CHANGE_REQUEST_OBJECT_NAME, menuName = COLOR_CHANGE_REQUEST_OBJECT_PATH)]
public class ColorChangeRequesterObject : ScriptableObject
{
    private const string COLOR_CHANGE_REQUEST_OBJECT_NAME = "Color Change Requester";
    private const string COLOR_CHANGE_REQUEST_OBJECT_PATH = "UI State/" + COLOR_CHANGE_REQUEST_OBJECT_NAME;

    [Serializable]
    public struct ColorChangeData
    {
        public float colorChangeDuration;
        public float colorChangeDelay;
        public Color color;
        public AnimationCurve colorChangeCurve;
    }

    [SerializeField] private ColorChangeData _colorChangeData = new ColorChangeData()
    {
        colorChangeDuration = 1.0f,
        colorChangeDelay = 0.0f,
        color = Color.white,
        colorChangeCurve = AnimationCurve.Linear(0, 0, 1, 1)
    };

    [field: SerializeField] public UnityEvent<ColorChangeData> OnColorChangeRequested { get; private set; }
    public void RequestColorChange() => OnColorChangeRequested?.Invoke(_colorChangeData);
}