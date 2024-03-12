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


        public void AugmentValue(float value)
        {
            if (_requesterObject.IsPaused)
            {
                _statusParameter.AugmentValue(0);
            }
            else
            {
                _statusParameter.AugmentValue(value);
            }
        }

        public void ReduceValue(float value)
        {
            if (_requesterObject.IsPaused)
            {
                _statusParameter.ReduceValue(0);
            }
            else
            {
                _statusParameter.ReduceValue(value);
            }
        }
    }
}
