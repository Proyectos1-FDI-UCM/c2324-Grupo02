using UnityEngine;

namespace TerrainSystem.Accessor
{
    public interface ITerrainModifierConfigurer
    {
        void ConfigureKernel(int kernel, int modificationSourcesCount, int terrainTypesCount, Texture terrainTexture, Camera camera);
        void ConfigureKernel(int kernel, Texture terrainTexture, Camera camera);
    }
}