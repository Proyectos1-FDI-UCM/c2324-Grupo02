using TerrainSystem.Accessor;
using UnityEngine;

namespace TerrainSystem.Requestable
{
    internal readonly struct TerrainWindowModifierRequestable<TSource> :
        ITerrainModifierRequestable<TSource[]>,
        IConfigurableTerrainModifierRequestable,
        ITerrainInitializerRequestable
    {
        private readonly TerrainModificationShaderAccessor _shaderAccessor;
        private readonly ITerrainModifierConfigurer _configurer;

        private readonly ComputeBuffer _sourcesBuffer;
        private readonly RenderTexture _terrainWindowTexture;

        public TerrainWindowModifierRequestable(TerrainModificationShaderAccessor shaderAccessor, ITerrainModifierConfigurer terrainModifierConfigurer, RenderTexture terrainWindowTexture, ComputeBuffer sourcesBuffer)
        {
            _shaderAccessor = shaderAccessor;
            _configurer = terrainModifierConfigurer;

            _sourcesBuffer = sourcesBuffer;
            _terrainWindowTexture = terrainWindowTexture;
        }

        public void ConfigureWith(int modificationSourcesCount, int terrainTypesCount, Texture terrainTexture, Camera camera)
        {
            _configurer.ConfigureKernel(_shaderAccessor.kernelCopyFromTerrainTexture, modificationSourcesCount, terrainTypesCount, terrainTexture, camera);
            _configurer.ConfigureKernel(_shaderAccessor.kernelModifyTerrainWindow, modificationSourcesCount, terrainTypesCount, terrainTexture, camera);
            _configurer.ConfigureKernel(_shaderAccessor.kernelCopyToTerrainTexture, modificationSourcesCount, terrainTypesCount, terrainTexture, camera);
        }

        public void InitializeTerrainWith(uint state, Camera camera, RenderTexture terrainTexture)
        {
            _shaderAccessor.ConfigureInitializationTerrainType(state);
            _configurer.ConfigureKernel(_shaderAccessor.kernelInitializeTerrainTexture, terrainTexture, camera);
            _shaderAccessor.DispatchIntializeTerrain(terrainTexture);
        }

        public void ModifyTerrainWith(TSource[] source, Camera camera, RenderTexture terrainTexture)
        {
            _sourcesBuffer.SetData(source);

            _configurer.ConfigureKernel(_shaderAccessor.kernelCopyFromTerrainTexture, terrainTexture, camera);
            _configurer.ConfigureKernel(_shaderAccessor.kernelModifyTerrainWindow, terrainTexture, camera);
            _configurer.ConfigureKernel(_shaderAccessor.kernelCopyToTerrainTexture, terrainTexture, camera);

            _shaderAccessor.DispatchModifyWindow(_terrainWindowTexture);
        }
    }
}