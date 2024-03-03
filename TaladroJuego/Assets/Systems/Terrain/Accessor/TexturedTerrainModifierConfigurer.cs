using UnityEngine;

namespace TerrainSystem.Accessor
{
    internal readonly struct TexturedTerrainModifierConfigurer : ITerrainModifierConfigurer
    {
        private readonly ITerrainModifierConfigurer _configurer;
        private readonly TerrainModificationShaderAccessor _shaderAccessor;
        private readonly ComputeBuffer _sourcesBuffer;
        private readonly Texture2DArray _alphaTextures;

        public TexturedTerrainModifierConfigurer(ITerrainModifierConfigurer configurer, TerrainModificationShaderAccessor shaderAccessor, ComputeBuffer sourcesBuffer, Texture2DArray alphaTextures)
        {
            _configurer = configurer;
            _shaderAccessor = shaderAccessor;
            _sourcesBuffer = sourcesBuffer;
            _alphaTextures = alphaTextures;
        }

        public void ConfigureKernel(int kernel, int modificationSourcesCount, int terrainTypesCount, Texture terrainTexture, Camera camera)
        {
            _configurer.ConfigureKernel(kernel, modificationSourcesCount, terrainTypesCount, terrainTexture, camera);
            _shaderAccessor.ConfigureTexturedSources(kernel, modificationSourcesCount, _sourcesBuffer, _alphaTextures);
        }

        public void ConfigureKernel(int kernel, Texture terrainTexture, Camera camera)
        {
            _configurer.ConfigureKernel(kernel, terrainTexture, camera);
        }
    }
}