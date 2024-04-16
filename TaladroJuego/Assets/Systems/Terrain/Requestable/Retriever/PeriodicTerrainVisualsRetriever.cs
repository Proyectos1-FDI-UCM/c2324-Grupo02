using TerrainSystem.Requester;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal class PeriodicTerrainVisualsRetriever : MonoBehaviour
    {
        [SerializeField]
        private RenderTexture _destination;
        private RenderTexture _destinationAlbedo;
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
            _destinationAlbedo = new RenderTexture(_destination.descriptor)
            {
                enableRandomWrite = true
            };

            _destinationNormals = new RenderTexture(_destination.descriptor)
            {
                enableRandomWrite = true
            };

            const string ALBEDO_TEXTURE_NAME = "_MainTex";
            _material.SetTexture(ALBEDO_TEXTURE_NAME, _destinationAlbedo);

            const string NORMALS_TEXTURE_NAME = "_NormalMap";
            _material.SetTexture(NORMALS_TEXTURE_NAME, _destinationNormals);
        }

        private void OnDestroy()
        {
            _destination.Release();
            _destinationAlbedo.Release();
            _destinationNormals.Release();
        }

        private void LateUpdate()
        {
            PositionedTerrainVisuals visuals = new PositionedTerrainVisuals(_destinationAlbedo, _visualsAnchor.position);

            _terrainModificationRequester.TryRetrieve(new PositionedTerrainVisualsWithNormals(
                visuals,
                _destinationNormals));

            if (!_sendToMaterial)
            {
                _terrainModificationRequester.TryRetrieve(in visuals);
                Graphics.Blit(_destinationAlbedo, _destination);
            }
        }
    }
}