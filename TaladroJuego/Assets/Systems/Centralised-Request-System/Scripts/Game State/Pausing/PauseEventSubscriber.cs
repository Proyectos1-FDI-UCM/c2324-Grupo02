using UnityEngine;
using UnityEngine.Events;

public class PauseEventSubscriber : MonoBehaviour
{
    [SerializeField]
    private PauseRequesterObject _pauseRequesterObject;

    [field: SerializeField]
    public UnityEvent PauseRequested { get; private set; }

    [field: SerializeField]
    public UnityEvent ResumeRequested { get; private set; }

    private void Awake()
    {
        _pauseRequesterObject.OnPauseRequested.AddListener(PauseRequested.Invoke);
        _pauseRequesterObject.OnResumeRequested.AddListener(ResumeRequested.Invoke);
    }

    private void OnDestroy()
    {
        _pauseRequesterObject.OnPauseRequested.RemoveListener(PauseRequested.Invoke);
        _pauseRequesterObject.OnResumeRequested.RemoveListener(ResumeRequested.Invoke);
    }
}
