using UnityEngine;

namespace TerrainSystem.Requester
{
    public interface IInitializableTerrainModificationRequester
    {
        bool Initialized { get; }
        bool Initialize(RenderTexture terrainTexture, RenderTexture terrainWindowTexture, Camera camera);
        bool Initialize(Vector2Int terrainTextureSize, Vector2Int terrainWindowTextureSize, Camera camera, out RenderTexture terrainRenderTexture, out RenderTexture terrainWindowRenderTexture);
        bool Finalize();
    }
}