using UnityEngine;

namespace TerrainSystem.Requestable
{
    public interface ITerrainInitializerRequestable
    {
        void InitializeTerrainWith(uint state, Camera camera, RenderTexture terrainTexture);
    }
}