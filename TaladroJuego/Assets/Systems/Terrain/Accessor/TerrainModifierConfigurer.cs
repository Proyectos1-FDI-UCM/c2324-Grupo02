using UnityEngine;

namespace TerrainSystem.Accessor
{
    internal readonly struct TerrainModifierConfigurer : ITerrainModifierConfigurer
    {
        private readonly TerrainModificationShaderAccessor _shaderAccessor;
        private readonly RenderTexture _terrainWindowTexture;
        private readonly ComputeBuffer _sourcesBuffer;
        private readonly ComputeBuffer _terrainModificationsBuffer;

        public TerrainModifierConfigurer(TerrainModificationShaderAccessor shaderAccessor, RenderTexture terrainWindowTexture, ComputeBuffer sourcesBuffer, ComputeBuffer terrainModificationsBuffer)
        {
            _shaderAccessor = shaderAccessor;
            _terrainWindowTexture = terrainWindowTexture;
            _sourcesBuffer = sourcesBuffer;
            _terrainModificationsBuffer = terrainModificationsBuffer;
        }

        public void ConfigureKernel(int kernel, int modificationSourcesCount, int terrainTypesCount, Texture terrainTexture, Camera camera)
        {
            _shaderAccessor.ConfigureTypeSources(kernel, modificationSourcesCount, _sourcesBuffer);
            _shaderAccessor.ConfigureTerrainTypes(kernel, terrainTypesCount, _terrainModificationsBuffer);

            ConfigureKernel(kernel, terrainTexture, camera);
        }

        public void ConfigureKernel(int kernel, Texture terrainTexture, Camera camera)
        {
            _shaderAccessor.ConfigureTerrainTexture(
                kernel,
                terrainTexture,
                Vector2.zero);
            _shaderAccessor.ConfigureTerrainTextureWindow(
                kernel,
                _terrainWindowTexture,
                (Vector2.one * 0.5f - (Vector2)camera.WorldToViewportPoint(Vector2.zero))
                    * new Vector2(_terrainWindowTexture.width, _terrainWindowTexture.height));

            _shaderAccessor.ConfigureCameraMatrices(camera);
        }
    }
}