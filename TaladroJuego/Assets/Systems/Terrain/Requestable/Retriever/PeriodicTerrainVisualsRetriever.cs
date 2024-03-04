using TerrainSystem.Requester;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal class PeriodicTerrainVisualsRetriever : MonoBehaviour
    {
        [SerializeField]
        private RenderTexture _destination;

        [SerializeField]
        private TerrainModificationRequester _terrainModificationRequester;

        private void LateUpdate()
        {
            _terrainModificationRequester.Retrieve(in _destination);
        }
    }
}