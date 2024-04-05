using TerrainSystem.Accessor;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal readonly struct TerrainVisualsRetriever : ITerrainDataRetriever<PositionedTerrainVisuals>
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

        public void Retrieve(in PositionedTerrainVisuals destination)
        {
            int kernel = _accessor.kernelCopyToVisualsFromWindow;
            _accessor.ConfigureTerrainTypes(kernel, _terrainTypesTextures);

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

            _accessor.ConfigureVisuals(kernel, visuals);
            _accessor.Dispatch(kernel, new Vector3(visuals.width, visuals.height, 1));

            Graphics.Blit(visuals, destination.renderTexture);
            visuals.Release();
        }

        public PositionedTerrainVisuals Retrieve()
        {
            PositionedTerrainVisuals destination = new PositionedTerrainVisuals(
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