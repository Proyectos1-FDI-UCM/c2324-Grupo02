using UnityEngine;

namespace TerrainSystem.Requester
{
    internal static class InitializableTerrainModificationRequesterExtensions
    {
        public static bool Reinitalize<TRequester>(this TRequester requester, Vector2Int terrainTextureSize, Vector2Int terrainWindowTextureSize, Camera camera)
            where TRequester : IInitializableTerrainModificationRequester =>
            (requester.Initialized
                && requester.Finalize()
                && requester.Initialize(terrainTextureSize, terrainWindowTextureSize, camera))
            || requester.Initialize(terrainTextureSize, terrainWindowTextureSize, camera);
    }
}