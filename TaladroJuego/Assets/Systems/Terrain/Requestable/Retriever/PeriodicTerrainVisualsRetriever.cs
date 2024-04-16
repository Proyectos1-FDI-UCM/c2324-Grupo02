using TerrainSystem.Requester;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal class PeriodicTerrainVisualsRetriever : MonoBehaviour
    {
        [SerializeField]
        private RenderTexture _destination;
        private RenderTexture _destinationNormals;
        [SerializeField]
        private Material _material;

        [SerializeField]
        private Transform _visualsAnchor;

        [SerializeField]
        private bool _sendToMaterial;

        [SerializeField]
        private TerrainModificationRequester _terrainModificationRequester;

        private void Awake()
        {
            if (_sendToMaterial)
            {
                _destinationNormals = new RenderTexture(_destination.descriptor)
                {
                    enableRandomWrite = true
                };

                const string ALBEDO_TEXTURE_NAME = "_MainTex";
                _material.SetTexture(ALBEDO_TEXTURE_NAME, _destination);

                const string NORMALS_TEXTURE_NAME = "_NormalMap";
                _material.SetTexture(NORMALS_TEXTURE_NAME, _destinationNormals);
            }
        }

        private void OnDestroy()
        {
            if (_sendToMaterial)
            {
                _destination.Release();
                _destinationNormals.Release();
            }
        }

        private void LateUpdate()
        {
            PositionedTerrainVisuals visuals = new PositionedTerrainVisuals(_destination, _visualsAnchor.position);
            if (_sendToMaterial)
                _terrainModificationRequester.TryRetrieve(new PositionedTerrainVisualsWithNormals(
                    visuals,
                    _destinationNormals));
            else
                _terrainModificationRequester.TryRetrieve(in visuals);
        }
    }
}