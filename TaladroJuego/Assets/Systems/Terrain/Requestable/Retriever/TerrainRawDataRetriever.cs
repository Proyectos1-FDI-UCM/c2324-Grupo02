using TerrainSystem.Accessor;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal readonly struct TerrainRawDataRetriever : ITerrainDataRetriever<PositionedTerrainRawData>
    {
        private readonly TerrainModificationShaderAccessor _accessor;
        private readonly RenderTexture _terrainTexture;
        private readonly RenderTexture _terrainWindowTexture;
        private readonly Camera _camera;

        public TerrainRawDataRetriever(ComputeShader terrainComputeShader, RenderTexture terrainTexture, RenderTexture terrainWindowTexture, Camera camera)
        {
            _accessor = new TerrainModificationShaderAccessor(terrainComputeShader);
            _terrainTexture = terrainTexture;
            _terrainWindowTexture = terrainWindowTexture;
            _camera = camera;
        }

        public void Retrieve(in PositionedTerrainRawData destination)
        {
            int kernel = _accessor.kernelCopyFromTerrainTexture;
            _accessor.ConfigureTerrainTexture(kernel, _terrainTexture, Vector2.zero);

            Vector2 cameraSize = new Vector2(
                _camera.orthographicSize * _camera.aspect,
                _camera.orthographicSize) * 2.0f;
            _accessor.ConfigureTerrainTextureWindow(
                kernel,
                _terrainWindowTexture,
                (destination.position / cameraSize) * new Vector2(_terrainWindowTexture.width, _terrainWindowTexture.height));

            RenderTexture visuals = new RenderTexture(destination.renderTexture.descriptor)
            {
                width = _terrainWindowTexture.width,
                height = _terrainWindowTexture.height,
                enableRandomWrite = true
            };

            _accessor.Dispatch(kernel, new Vector3(visuals.width, visuals.height, 1));
            Graphics.Blit(visuals, destination.renderTexture);
            visuals.Release();
        }

        public PositionedTerrainRawData Retrieve()
        {
            PositionedTerrainRawData destination = new PositionedTerrainRawData(
                new RenderTexture(_terrainWindowTexture.descriptor)
                {
                    width = _terrainWindowTexture.width,
                    height = _terrainWindowTexture.height,
                    enableRandomWrite = true
                },
                _camera.transform.position);
            Retrieve(in destination);
            return destination;
        }
    }
}