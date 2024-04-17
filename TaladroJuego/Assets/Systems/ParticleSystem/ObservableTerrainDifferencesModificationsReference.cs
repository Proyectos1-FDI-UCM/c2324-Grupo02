using UnityEngine;

using TerrainSystem.Requestable.Retriever.Observable;
using System;

namespace Particles
{
    internal class ObservableTerrainDifferencesModificationsReference : MonoBehaviour,
        IObservableTerrainData<TerrainModification>,
        IObservableTerrainData<TerrainModification[]>
    {
        [SerializeField]
        private GameObject _observableTerrainDifferencesModificationsObject;
        private IObservableTerrainData<TerrainModification> _terrainModificationObservable;
        private IObservableTerrainData<TerrainModification[]> _terrainModificationsObservable;

        public event EventHandler<TerrainModification[]> DataRetrieved
        {
            add
            {
                _terrainModificationsObservable.DataRetrieved += value;
            }

            remove
            {
                _terrainModificationsObservable.DataRetrieved -= value;
            }
        }

        event EventHandler<TerrainModification> IObservableTerrainData<TerrainModification>.DataRetrieved
        {
            add
            {
                _terrainModificationObservable.DataRetrieved += value;
            }

            remove
            {
                _terrainModificationObservable.DataRetrieved -= value;
            }
        }

        private void Awake()
        {
            _terrainModificationObservable = _observableTerrainDifferencesModificationsObject.GetComponentInChildren<IObservableTerrainData<TerrainModification>>();
            _terrainModificationsObservable = _observableTerrainDifferencesModificationsObject.GetComponentInChildren<IObservableTerrainData<TerrainModification[]>>();
        }
    }
}