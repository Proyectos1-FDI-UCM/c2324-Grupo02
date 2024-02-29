using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace StatusSystem
{
    internal class ClampedStatusParameter : MonoBehaviour, IStatusParameter
    {
        [SerializeField] private float _minValue, _maxValue;
        [SerializeField] private StatusParameter _statusParameter;
        [field: SerializeField] public UnityEvent  ReachedMin { get; private set; }
        [field: SerializeField] public UnityEvent ReachedMax { get; private set; }

        
        public void AugmentValue(float value)
        {
            _statusParameter.AugmentValue(value);
            float excessValue = Mathf.Max(_statusParameter.StatusValue - _maxValue, 0.0f);
            _statusParameter.ReduceValue(excessValue);

            if (excessValue != 0) ReachedMax?.Invoke();
        }

        public void ReduceValue(float value)
        {
            _statusParameter.ReduceValue(value);
            float excessValue = Mathf.Max(_minValue - _statusParameter.StatusValue, 0.0f);
            _statusParameter.AugmentValue(excessValue);

            if (excessValue != 0) ReachedMin?.Invoke();
        }
    }
}

