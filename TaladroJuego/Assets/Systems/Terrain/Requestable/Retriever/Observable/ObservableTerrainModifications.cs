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
        private float _threshold;

        public event EventHandler<TerrainModification> DataRetrieved;

        private void Awake()
        {
            _observableTerrainData = GetComponentsInChildren<IObservableTerrainData<TerrainModification>>().FirstOrDefault(o => (object)o != this);
            _observableTerrainData.DataRetrieved += OnModificationRetrieved;
        }

        private void OnDestroy()
        {
            _observableTerrainData.DataRetrieved -= OnModificationRetrieved;
        }

        private void OnModificationRetrieved(object sender, TerrainModification e)
        {
            if (Mathf.Abs(e.amount) > _threshold)
                DataRetrieved?.Invoke(this, e);
        }
    }
}