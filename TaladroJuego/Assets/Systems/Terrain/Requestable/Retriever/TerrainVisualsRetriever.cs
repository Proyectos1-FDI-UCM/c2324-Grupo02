using TerrainSystem.Accessor;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal readonly struct TerrainVisualsRetriever : ITerrainDataRetriever<RenderTexture>
    {
        private readonly TerrainModificationShaderAccessor _accessor;
        private readonly Texture2DArray _terrainTypesTextures;
        private readonly RenderTexture _terrainWindowTexture;
        private readonly Camera _camera;

        public TerrainVisualsRetriever(ComputeShader terrainComputeShader, Texture2DArray terrainTypesTextures, RenderTexture terrainWindowTexture, Camera camera)
        {
            _accessor = new TerrainModificationShaderAccessor(terrainComputeShader);
            _terrainTypesTextures = terrainTypesTextures;
            _terrainWindowTexture = terrainWindowTexture;
            _camera = camera;
        }

        public void Retrieve(RenderTexture destination)
        {
            int kernel = _accessor.kernelCopyToVisualsFromWindow;
            _accessor.ConfigureTerrainTypes(kernel, _terrainTypesTextures);

            Vector2 cameraSize = new Vector2(
                _camera.orthographicSize * _camera.aspect,
                _camera.orthographicSize) * 2.0f;
            _accessor.ConfigureTerrainTextureWindow(
                kernel,
                _terrainWindowTexture,
                (_camera.transform.position / cameraSize) * new Vector2(_terrainWindowTexture.width, _terrainWindowTexture.height));

            RenderTexture visuals = new RenderTexture(destination.descriptor)
            {
                width = _terrainWindowTexture.width,
                height = _terrainWindowTexture.height,
                enableRandomWrite = true
            };

            _accessor.ConfigureVisuals(kernel, visuals);
            _accessor.Dispatch(kernel, new Vector3(visuals.width, visuals.height, 1));

            Graphics.Blit(visuals, destination);
            visuals.Release();
        }
    }
}