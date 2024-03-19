using UnityEngine;

using TerrainSystem.Requestable.Retriever.Observable;
using System.Collections.Generic;
using UpgradesSystem.Flyweight;

namespace Particles
{
    [RequireComponent(typeof(ParticleSystem))]
    internal class ParticlesSwitch : MonoBehaviour
    {
        private ParticleSystem ps;

        [SerializeField] private IObservableTerrainData<TerrainModification> terrainData;

        [SerializeField] private Dictionary<ResourceType, uint> terrainType;

        private void Awake() {
            ps = GetComponent<ParticleSystem>();

            terrainData = GetComponent<IObservableTerrainData<TerrainModification>>();

            terrainData.DataRetrieved += TerrainData;

            terrainType.Add(ResourceType.Lead, 1);
        }

        private void TerrainData(object sender, TerrainModification e) {
            print(e.amount);
            print(e.terrainType);
            if (e.amount > 0) ps.Play();
            else ps.Stop();
        }
    }
}