using UnityEngine;

namespace TerrainSystem.Requestable
{
    public interface IConfigurableTerrainModifierRequestable
    {
        void ConfigureWith(int modificationSourcesCount, int terrainTypesCount, Texture terrainTexture, Camera camera);
    }
}