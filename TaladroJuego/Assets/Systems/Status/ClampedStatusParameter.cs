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
            //if(_statusParameter.StatusValue + value >= _maxValue)
            //{
            //    float valToMax = _maxValue - _statusParameter.StatusValue;
            //    _statusParameter.AugmentValue(valToMax);
            //    ReachedMax.Invoke();
            //}
            //else
            //{
            //    _statusParameter.AugmentValue(value);
            //}

            _statusParameter.AugmentValue(value);
            float excessValue = Mathf.Max(_statusParameter.StatusValue - _maxValue, 0.0f);
            _statusParameter.ReduceValue(excessValue);
        }

        public void ReduceValue(float value)
        {
            //if (_statusParameter.StatusValue - value <= _minValue)
            //{
            //    float valToMin = _statusParameter.StatusValue - _minValue;
            //    _statusParameter.ReduceValue(valToMin);
            //    ReachedMin.Invoke();
            //}
            //else
            //{
            //    _statusParameter.ReduceValue(value);
            //}

            _statusParameter.ReduceValue(value);
            float excessValue = Mathf.Max(_minValue - _statusParameter.StatusValue, 0.0f);
            _statusParameter.AugmentValue(excessValue);
            
        }
    }
}

