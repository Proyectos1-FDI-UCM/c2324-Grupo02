using System;
using TerrainSystem.Requester;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever.Observable
{
    internal class ObservableTerrainDifferencesModifications : MonoBehaviour, IObservableTerrainData<TerrainModification>,
        IObservableTerrainData<TerrainModification[]>,
        IObservableTerrainData<float>,
        IObservableTerrainData<float[]>
    {
        [SerializeField]
        private TerrainModificationRequester _terrainModificationRequester;
        private readonly float[] _terrainModifications = new float[TerrainModificationRequester.MAX_TERRAIN_TYPES];

        public event EventHandler<TerrainModification> DataRetrieved;
        private event EventHandler<TerrainModification[]> TerrainModificationsRetrieved;
        private event EventHandler<float> ModificationRetrieved;
        private event EventHandler<float[]> ModificationsRetrieved;

        event EventHandler<TerrainModification[]> IObservableTerrainData<TerrainModification[]>.DataRetrieved
        {
            add => TerrainModificationsRetrieved += value;
            remove => TerrainModificationsRetrieved -= value;
        }

        event EventHandler<float> IObservableTerrainData<float>.DataRetrieved
        {
            add => ModificationRetrieved += value;
            remove => ModificationRetrieved -= value;
        }

        event EventHandler<float[]> IObservableTerrainData<float[]>.DataRetrieved
        {
            add => ModificationsRetrieved += value;
            remove => ModificationsRetrieved -= value;
        }

        private void Awake()
        {
            _terrainModificationRequester.ModificationRequested += OnModificationRequested;
        }

        private void OnDestroy()
        {
            _terrainModificationRequester.ModificationRequested -= OnModificationRequested;
        }

        private void OnModificationRequested(object sender, EventArgs e)
        {
            TerrainModification[] modificationDifferences = new TerrainModification[_terrainModifications.Length];
            float[] differences = new float[_terrainModifications.Length];

            float[] modifications = new float[_terrainModifications.Length];
            _terrainModificationRequester.Retrieve(in modifications);

            for (uint i = 0; i < modifications.Length; i++)
            {
                float diference = modifications[i] - _terrainModifications[i];

                _terrainModifications[i] = modifications[i];
                modificationDifferences[i] = new TerrainModification(i, diference);
                differences[i] = diference;

                DataRetrieved?.Invoke(this, new TerrainModification(i, diference));
                ModificationRetrieved?.Invoke(this, diference);
            }

            TerrainModificationsRetrieved?.Invoke(this, modificationDifferences);
            ModificationsRetrieved?.Invoke(this, differences);
        }
    }
}