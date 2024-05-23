using SaveSystem.Saveable;
using StatusSystem;
using UnityEngine;

namespace SaveablesSystem.StatusSystem
{
    internal class SaveableStatusParameter : MonoBehaviour, ISaveable<float>, ISaveable<double>, ISaveable<decimal>
    {
        private IStatusParameter _statusParameter;

        public float GetData() =>
            _statusParameter.Value;
        double ISaveable<double>.GetData() =>
            _statusParameter.Value;

        decimal ISaveable<decimal>.GetData() =>
            (decimal)_statusParameter.Value;

        object ISaveable.GetData() =>
            GetData();

        public bool TrySetData(float saveData)
        {
            _statusParameter.Value = saveData;
            return true;
        }

        public bool TrySetData(double saveData)
        {
            _statusParameter.Value = (float)saveData;
            return true;
        }

        public bool TrySetData(decimal saveData)
        {
            _statusParameter.Value = (float)saveData;
            return true;
        }

        public bool TrySetData<T>(T saveData) => saveData switch
        {
            float floatData => TrySetData(floatData),
            double doubleData => TrySetData(doubleData),
            decimal decimalData => TrySetData(decimalData),
            _ => false
        };

        private void Awake()
        {
            _statusParameter = GetComponent<IStatusParameter>();
        }
    }
}