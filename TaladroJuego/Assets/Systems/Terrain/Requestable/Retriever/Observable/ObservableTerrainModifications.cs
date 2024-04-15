using System;
using System.Linq;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever.Observable
{
    internal class ObservableTerrainModifications : MonoBehaviour, IObservableTerrainData<TerrainModification>
    {
        private IObservableTerrainData<TerrainModification> _observableTerrainData;
        [SerializeField]
        [Min(0.0f)]
        private float _lowerThreshold;

        [SerializeField]
        [Min(0.0f)]
        private float _upperThreshold;

        public event EventHandler<TerrainModification> DataRetrieved;

        private void Awake()
        {
            _observableTerrainData = GetComponentsInChildren<IObservableTerrainData<TerrainModification>>().FirstOrDefault(o => o != (IObservableTerrainData<TerrainModification>)this);
            _observableTerrainData.DataRetrieved += OnModificationRetrieved;
        }

        private void OnDestroy()
        {
            _observableTerrainData.DataRetrieved -= OnModificationRetrieved;
        }

        private void OnModificationRetrieved(object sender, TerrainModification e)
        {
            float absAmount = Mathf.Abs(e.amount);
            if (absAmount >= _lowerThreshold && absAmount < _upperThreshold)
                DataRetrieved?.Invoke(this, e);
        }
    }
}