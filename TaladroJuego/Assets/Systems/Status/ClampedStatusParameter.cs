using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace StatusSystem
{
    /// <summary>
    /// Represents a status parameter that is clamped between a minimum and maximum value.
    /// </summary>
    public class ClampedStatusParameter : MonoBehaviour, IStatusParameter
    {
        [SerializeField] private float _maxValue;
        [SerializeField] private StatusParameter _statusParameter;
        [SerializeField] private float _minValue;

        [field: SerializeField] public UnityEvent ReachedMin { get; private set; }
        [field: SerializeField] public UnityEvent ReachedMax { get; private set; }

        /// <summary>
        /// Gets or sets the maximum value of the clamped status parameter.
        /// </summary>
        public float MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        /// <summary>
        /// Gets or sets the current value of the clamped status parameter.
        /// </summary>
        public float Value
        {
            get => _statusParameter.Value;
            set
            {  
                if (value <= _minValue) 
                {
                    _statusParameter.Value = _minValue;
                    ReachedMin.Invoke();
                }
                else if (value >= _maxValue)
                {
                    _statusParameter.Value = _maxValue;
                    ReachedMax.Invoke();
                }
                else _statusParameter.Value = value;
            }
        }
    }
}

