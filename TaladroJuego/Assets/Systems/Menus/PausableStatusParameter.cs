using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusSystem;

namespace MenusSystem
{
    internal class PausableStatusParameter : MonoBehaviour, IStatusParameter
    {
        [SerializeField]
        private PauseRequesterObject _requesterObject;

        private IStatusParameter _statusParameter;

        public float Value
        {
            get => _statusParameter.Value;
            set
            {
                if (!_requesterObject.IsPaused) { Debug.Log("Not Paused", this); _statusParameter.Value = value; }
            }
        }

        private void Awake()
        {
            IStatusParameter[] statusParameters = GetComponentsInChildren<IStatusParameter>();
            int i = 0;
            while (statusParameters[i] == (IStatusParameter)this)
            {
                i++;
            }
            _statusParameter = statusParameters[i];
        }
    }
}
