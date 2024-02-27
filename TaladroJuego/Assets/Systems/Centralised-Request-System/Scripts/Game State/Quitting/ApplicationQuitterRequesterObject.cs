using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = APPLICATION_EXITTER_OBJECT_NAME, menuName = APPLICATION_EXITTER_OBJECT_PATH)]
public class ApplicationQuitterRequesterObject : ScriptableObject
{
    private const string APPLICATION_EXITTER_OBJECT_NAME = "Application Quitter Requester";
    private const string APPLICATION_EXITTER_OBJECT_PATH = "Game State/" + APPLICATION_EXITTER_OBJECT_NAME;

    [field: SerializeField] public UnityEvent OnApplicationExitRequested { get; private set; }
    public void RequestApplicationExit()
    {
        OnApplicationExitRequested?.Invoke();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Application.OpenURL("about:blank");
#elif UNITY_STANDALONE
        Application.Quit();
#endif
    }
}
