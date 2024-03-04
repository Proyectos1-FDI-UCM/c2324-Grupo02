using UnityEngine;

namespace TerrainSystem.Requester
{
    public interface IInitializableTerrainModificationRequester
    {
        bool Initialized { get; }
        bool Initialize(Vector2Int terrainTextureSize, Vector2Int terrainWindowTextureSize, Camera camera);
        bool Finalize();
    }
}