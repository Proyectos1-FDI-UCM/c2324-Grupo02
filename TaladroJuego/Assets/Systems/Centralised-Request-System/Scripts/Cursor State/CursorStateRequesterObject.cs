using System;
using UnityEngine;

[CreateAssetMenu(fileName = POINTER_STATE_REQUEST_NAME, menuName = POINTER_STATE_REQUEST_PATH)]
public class CursorStateRequesterObject : ScriptableObject
{
    private const string POINTER_STATE_REQUEST_NAME = "Cursor State Requester";
    private const string POINTER_STATE_REQUEST_PATH = "UI State/" + POINTER_STATE_REQUEST_NAME;

    [Serializable]
    public struct CursorStateData
    {
        public CursorLockMode cursorLockMode;
        public bool cursorVisibility;
        public Texture2D cursorTexture;
    }

    [SerializeField] private CursorStateData _cursorStateData = new CursorStateData()
    {
        cursorLockMode = CursorLockMode.None,
        cursorVisibility = true
    };

    public void SetCursorLockMode() => Cursor.lockState = _cursorStateData.cursorLockMode;
    public void SetCursorVisibility() => Cursor.visible = _cursorStateData.cursorVisibility;
    public void SetCursorTexture() => Cursor.SetCursor(_cursorStateData.cursorTexture, Vector2.zero, CursorMode.Auto);
    public void SetCursorState()
    {
        SetCursorLockMode();
        SetCursorVisibility();
        SetCursorTexture();
    }
}
