using UnityEngine;

namespace TerrainSystem.Data
{
    internal readonly struct TerrainModificationSource : ITerrainModificationSource, ITerrainModificationConfiguration
    {
        public const int SIZE_OF = ((sizeof(float) * 3) * 2) + (sizeof(uint) * 2) + (sizeof(float) * 3);

        public readonly Vector3 positionWS;
        public readonly Vector3 rotationWS;

        public readonly uint sdfType;
        public readonly float radius;
        public readonly float strength;
        public readonly float falloff;

        public readonly uint type;

        public TerrainModificationSource(Vector3 positionWS, Vector3 rotationWS, uint sdfType, float radius, float strength, float falloff, uint type)
        {
            this.positionWS = positionWS;
            this.rotationWS = rotationWS;
            this.sdfType = sdfType;
            this.radius = radius;
            this.strength = strength;
            this.falloff = falloff;
            this.type = type;
        }

        public Vector3 GetPosition() => positionWS;
        public Quaternion GetRotation() => Quaternion.Euler(rotationWS);
        public uint GetTerrainType() => type;

        public float Radius => radius;
        public float Strength => strength;
        public float Falloff => falloff;
        public uint Type => type;
    };
}