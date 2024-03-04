using System;
using System.Linq;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever.Observable
{
    internal class ObservableGreatestTerrainModifications : MonoBehaviour, IObservableTerrainData<TerrainModification>
    {
        private IObservableTerrainData<TerrainModification[]> _observableTerrainData;

        [SerializeField]
        [Min(0)]
        private int _maxModifications;

        public event EventHandler<TerrainModification> DataRetrieved;

        private void Awake()
        {
            _observableTerrainData = GetComponentInChildren<IObservableTerrainData<TerrainModification[]>>();
            _observableTerrainData.DataRetrieved += OnDataRetrieved;
        }

        private void OnDestroy()
        {
            _observableTerrainData.DataRetrieved -= OnDataRetrieved;
        }

        private void OnDataRetrieved(object sender, TerrainModification[] e)
        {
            TerrainModification[] modifications = 
                e.OrderByDescending(m => Mathf.Abs(m.amount))
                    .Take(_maxModifications)
                    .ToArray();

            foreach (TerrainModification modification in modifications)
                DataRetrieved?.Invoke(this, modification);
        }
    }
}