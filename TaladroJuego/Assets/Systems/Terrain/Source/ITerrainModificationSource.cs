using TerrainSystem.Modifier;
using UnityEngine;

namespace TerrainSystem.Source
{
    public interface ITerrainModificationSource
    {
        Vector3 GetPosition();
        Quaternion GetRotation();

        ITerrainModificationConfiguration GetConfiguration();
        uint GetTerrainType();
    }
}