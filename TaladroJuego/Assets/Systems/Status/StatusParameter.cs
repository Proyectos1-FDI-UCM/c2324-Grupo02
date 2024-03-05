using UnityEngine;
using UnityEngine.Events;

namespace StatusSystem
{
    public class StatusParameter : MonoBehaviour, IStatusParameter
    {
        [SerializeField] private float _statusValue = 0.0f;

        [field: SerializeField] public UnityEvent<float> ValueSet { get; private set; }
        public float StatusValue
        {
            get { return _statusValue; }
            private set
            {
                _statusValue = value;
                ValueSet.Invoke(_statusValue);
            }
        }

        public void AugmentValue(float value)
        {
            StatusValue += value;
        }

        public void ReduceValue(float value)
        {
            StatusValue -= value;
        }
    }
}

