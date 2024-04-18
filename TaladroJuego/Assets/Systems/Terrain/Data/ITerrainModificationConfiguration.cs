using UnityEngine;

namespace TerrainSystem.Data
{
    public interface ITerrainModificationConfiguration
    {
        Vector3 Size { get; }
        float Radius { get; }
        float Strength { get; }
        float Falloff { get; }

        uint Type { get; }
        int ModificationsBufferWriteIndex { get; }
    }
}