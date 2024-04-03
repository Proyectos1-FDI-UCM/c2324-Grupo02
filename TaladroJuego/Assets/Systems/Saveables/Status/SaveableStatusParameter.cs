using SaveSystem.Saveable;
using StatusSystem;
using UnityEngine;

namespace SaveablesSystem.StatusSystem
{
    internal class SaveableStatusParameter : MonoBehaviour, ISaveable<float>
    {
        private IStatusParameter _statusParameter;

        public float GetData() =>
            _statusParameter.Value;

        public bool TrySetData(float saveData)
        {
            _statusParameter.Value = saveData;
            return true;
        }

        private void Awake()
        {
            _statusParameter = GetComponent<IStatusParameter>();
        }
    }
}