using UnityEngine;
using UnityEngine.Events;

namespace StatusSystem
{
    public class StatusParameter : MonoBehaviour, IStatusParameter
    {
        [SerializeField] private float _statusValue = 0.0f;

        [field: SerializeField] public UnityEvent<float> ValueSet { get; private set; }
        public float Value
        {
            get { return _statusValue; }
            set
            {
                _statusValue = value;
                ValueSet.Invoke(_statusValue);
            }
        }
    }
}

