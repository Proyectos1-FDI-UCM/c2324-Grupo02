using System.Threading.Tasks;
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

        public Task<bool> TryRetrieve(in PositionedTerrainRawData destination)
        {
            int kernel = _accessor.kernelCopyFromTerrainTexture;
            _accessor.ConfigureTerrainTexture(kernel, _terrainTexture, Vector2.zero);

            //RenderTexture rawDataWindow = new RenderTexture(destination.renderTexture.descriptor)
            //{
            //    width = destination.renderTexture.width,
            //    height = destination.renderTexture.height,
            //    enableRandomWrite = true
            //};

            Vector2 cameraSize = new Vector2(
                _camera.orthographicSize * _camera.aspect,
                _camera.orthographicSize) * 2.0f;
            _accessor.ConfigureTerrainTextureWindow(
                kernel,
                destination.renderTexture,
                (destination.position / cameraSize) * new Vector2(destination.renderTexture.width, destination.renderTexture.height));

            _accessor.Dispatch(kernel, new Vector3(destination.renderTexture.width, destination.renderTexture.height, 1));
            //Graphics.Blit(rawDataWindow, destination.renderTexture);
            //rawDataWindow.Release();
            return Task.FromResult(true);
        }

        public Task<PositionedTerrainRawData> Retrieve()
        {
            PositionedTerrainRawData destination = new PositionedTerrainRawData(
                new RenderTexture(_camera.targetTexture.descriptor)
                {
                    width = _camera.targetTexture.width,
                    height = _camera.targetTexture.height,
                    enableRandomWrite = true
                },
                _camera.transform.position);
            TryRetrieve(in destination);
            return Task.FromResult(destination);
        }
    }
}