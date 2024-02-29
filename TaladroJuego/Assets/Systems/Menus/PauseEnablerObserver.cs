using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenusSystem
{
    internal class PauseEnablerObserver : MonoBehaviour
    {
        [SerializeField]
        private PauseRequesterObject _pauseObject;

        [SerializeField]
        private MonoBehaviour _behaviour;

        private void Awake()
        {
            _pauseObject.OnPauseRequested.AddListener(OnPauseRequested);

            _pauseObject.OnResumeRequested.AddListener(OnResumeRequested);

        }

        private void OnDestroy()
        {
            _pauseObject.OnPauseRequested.RemoveListener(OnPauseRequested);

            _pauseObject.OnResumeRequested.RemoveListener(OnResumeRequested);
        }

        private void OnPauseRequested()
        {
            _behaviour.enabled = false;
        }

        private void OnResumeRequested()
        {
            _behaviour.enabled = true;
        }
    }
}
