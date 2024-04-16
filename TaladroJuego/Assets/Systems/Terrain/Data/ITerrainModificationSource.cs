using UnityEngine;

namespace TerrainSystem.Data
{
    public interface ITerrainModificationSource
    {
        Vector3 GetPosition();
        Quaternion GetRotation();
        Vector3 GetScale();
    }
}