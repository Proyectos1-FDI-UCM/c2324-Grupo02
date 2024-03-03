using UnityEngine;

namespace TerrainSystem.Requestable
{
    public interface ITerrainModifierRequestable<in TSource>
    {
        void ModifyTerrainWith(TSource source, Camera camera, RenderTexture terrainTexture);
    }
}