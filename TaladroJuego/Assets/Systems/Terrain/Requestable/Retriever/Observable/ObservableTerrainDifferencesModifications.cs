using System;
using System.Threading.Tasks;
using TerrainSystem.Requester;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever.Observable
{
    internal class ObservableTerrainDifferencesModifications : MonoBehaviour,
        IObservableTerrainData<TerrainModification>,
        IObservableTerrainData<TerrainModification[]>
    {
        [SerializeField]
        private TerrainModificationRequester _terrainModificationRequester;
        private readonly TerrainModification[] _terrainModifications = new TerrainModification[TerrainModificationRequester.MAX_TERRAIN_TYPES];

        public event EventHandler<TerrainModification> DataRetrieved;
        private event EventHandler<TerrainModification[]> TerrainModificationsRetrieved;

        event EventHandler<TerrainModification[]> IObservableTerrainData<TerrainModification[]>.DataRetrieved
        {
            add => TerrainModificationsRetrieved += value;
            remove => TerrainModificationsRetrieved -= value;
        }

        private void Awake()
        {
            _terrainModificationRequester.ModificationRequested += OnModificationRequested;
        }

        private void OnDestroy()
        {
            _terrainModificationRequester.ModificationRequested -= OnModificationRequested;
        }

        private void OnModificationRequested(object sender, EventArgs e) =>
            _ = OnModificationRequestedAsync();

        private async Task OnModificationRequestedAsync()
        {
            TerrainModification[] modifications = new TerrainModification[_terrainModifications.Length];
            if (!await _terrainModificationRequester.TryRetrieve(in modifications))
                return;

            for (uint i = 0; i < modifications.Length; i++)
            {
                TerrainModification terrainModification = modifications[i];
                float diference = terrainModification.amount - _terrainModifications[i].amount;

                _terrainModifications[i] = terrainModification;
                modifications[i] = terrainModification.WithAmount(diference);

                DataRetrieved?.Invoke(this, modifications[i]);
            }
            TerrainModificationsRetrieved?.Invoke(this, modifications);
        }
    }
}