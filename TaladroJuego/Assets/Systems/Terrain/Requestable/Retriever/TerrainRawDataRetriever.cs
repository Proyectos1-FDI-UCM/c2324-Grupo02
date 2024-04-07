using TerrainSystem.Accessor;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal readonly struct TerrainRawDataRetriever : ITerrainDataRetriever<PositionedTerrainRawData>
    {
        private readonly TerrainModificationShaderAccessor _accessor;
        private readonly RenderTexture _terrainTexture;
        private readonly Camera _camera;

        public TerrainRawDataRetriever(ComputeShader terrainComputeShader, RenderTexture terrainTexture, RenderTexture terrainWindowTexture, Camera camera)
        {
            _accessor = new TerrainModificationShaderAccessor(terrainComputeShader);
            _terrainTexture = terrainTexture;
            //_terrainWindowTexture = terrainWindowTexture;
            _camera = camera;
        }

        public void Retrieve(in PositionedTerrainRawData destination)
        {
            int kernel = _accessor.kernelCopyFromTerrainTexture;
            _accessor.ConfigureTerrainTexture(kernel, _terrainTexture, Vector2.zero);

            RenderTexture rawDataWindow = new RenderTexture(destination.renderTexture.descriptor)
            {
                width = destination.renderTexture.width,
                height = destination.renderTexture.height,
                enableRandomWrite = true
            };

            Vector2 cameraSize = new Vector2(
                _camera.orthographicSize * _camera.aspect,
                _camera.orthographicSize) * 2.0f;
            _accessor.ConfigureTerrainTextureWindow(
                kernel,
                rawDataWindow,
                (destination.position / cameraSize) * new Vector2(rawDataWindow.width, rawDataWindow.height));

            _accessor.Dispatch(kernel, new Vector3(rawDataWindow.width, rawDataWindow.height, 1));
            Graphics.Blit(rawDataWindow, destination.renderTexture);
            rawDataWindow.Release();
        }

        public PositionedTerrainRawData Retrieve()
        {
            //PositionedTerrainRawData destination = new PositionedTerrainRawData(
            //    new RenderTexture(descriptor)
            //    {
            //        width = _terrainWindowTexture.width,
            //        height = _terrainWindowTexture.height,
            //        enableRandomWrite = true
            //    },
            //    _camera.transform.position);
            //Retrieve(in destination);
            return default;
        }
    }
}