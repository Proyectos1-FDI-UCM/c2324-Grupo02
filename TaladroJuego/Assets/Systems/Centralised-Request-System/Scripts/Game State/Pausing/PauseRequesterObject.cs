using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = PAUSE_REQUESTER_OBJECT_NAME, menuName = PAUSE_REQUESTER_OBJECT_PATH)]
public class PauseRequesterObject : ScriptableObject
{
    private const string PAUSE_REQUESTER_OBJECT_NAME = "Pause Requester";
    private const string PAUSE_REQUESTER_OBJECT_PATH = "Game State/" + PAUSE_REQUESTER_OBJECT_NAME;

    [SerializeField] private bool _isPaused = false;
    public bool IsPaused
    { 
        get => _isPaused;
        private set
        {
            bool wasPaused = _isPaused;
            _isPaused = value;
            
            if (wasPaused && !_isPaused)
                OnResumeRequested?.Invoke();
            if (!wasPaused && _isPaused)
                OnPauseRequested?.Invoke();
        }
    }

    [field: SerializeField] public UnityEvent OnPauseRequested { get; private set; }
    [field: SerializeField] public UnityEvent OnResumeRequested { get; private set; }

    public void RequestPause() => IsPaused = true;
    public void RequestResume() => IsPaused = false;
    public void RequestPause(bool pause) => IsPaused = pause;
    public void RequestTogglePause() => IsPaused = !IsPaused;
}
