using TerrainSystem.Requestable.Retriever;
using TerrainSystem.Requestable.Retriever.Component;
using UnityEngine;

namespace UISystem.RadarSystem.Test
{
    internal class PeriodicRadarUpdaterTest : MonoBehaviour
    {
        [SerializeField]
        private TerrainRawDataRetriever _terrainRawDataRetriever;

        [SerializeField]
        private RenderTexture _radarRawDataRenderTexture;
        [SerializeField]
        private RenderTexture _terrainWindowTexture;
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Transform _radarDataCentreAnchor;

        private void LateUpdate()
        {
            Vector2 relativeSize = new Vector2(
                _terrainWindowTexture.width / (float)_radarRawDataRenderTexture.width,
                _terrainWindowTexture.height / (float)_radarRawDataRenderTexture.height);
            Vector2 cameraSize = new Vector2(
                _camera.orthographicSize * _camera.aspect,
                _camera.orthographicSize) * 2.0f;
            Vector2 relativeTextureCenteringOffset = new Vector2(
                _terrainWindowTexture.width - _radarRawDataRenderTexture.width,
                _terrainWindowTexture.height - _radarRawDataRenderTexture.height) * 0.5f / new Vector2(_terrainWindowTexture.width, _terrainWindowTexture.height);

            _terrainRawDataRetriever.TryRetrieve(PositionedTerrainRawData.From(
                _radarRawDataRenderTexture, 
                ((Vector2)_radarDataCentreAnchor.position + relativeTextureCenteringOffset * cameraSize) * relativeSize));
        }
    }
}