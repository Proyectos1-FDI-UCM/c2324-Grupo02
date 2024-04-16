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
        [Serializable]
        private struct DebugTerrainModification
        {
            public uint terrainType;
            public uint modificationSourceIndex;
            public float amount;

            public DebugTerrainModification(uint terrainType, uint modificationSourceIndex, float amount)
            {
                this.terrainType = terrainType;
                this.modificationSourceIndex = modificationSourceIndex;
                this.amount = amount;
            }

            public static DebugTerrainModification FromTerrainModification(TerrainModification terrainModification) =>
                new DebugTerrainModification(terrainModification.terrainType, terrainModification.modificationSourceIndex, terrainModification.amount);
        }

        [SerializeField]
        private TerrainModificationRequester _terrainModificationRequester;
        private readonly TerrainModification[] _terrainModifications = new TerrainModification[TerrainModificationRequester.MAX_TERRAIN_TYPES];

        [SerializeField]
        private DebugTerrainModification[] _cachedTerrainModifications;
        [SerializeField]
        private DebugTerrainModification[] _incomingTerrainModifications;

        [SerializeField]
        private bool _debug;

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

            _incomingTerrainModifications = Array.ConvertAll(modifications, DebugTerrainModification.FromTerrainModification);

            //Debug.ClearDeveloperConsole();
            for (uint i = 0; i < modifications.Length; i++)
            {
                TerrainModification terrainModification = modifications[i];
                float diference = terrainModification.amount - _terrainModifications[i].amount;
                //Debug.Log($"Diference: {diference}");

                _terrainModifications[i] = terrainModification;
                modifications[i] = terrainModification.WithAmount(diference);

                DataRetrieved?.Invoke(this, modifications[i]);
            }
            TerrainModificationsRetrieved?.Invoke(this, modifications);
            _cachedTerrainModifications = Array.ConvertAll(_terrainModifications, DebugTerrainModification.FromTerrainModification);
            //Debug.Break();
        }
    }
}