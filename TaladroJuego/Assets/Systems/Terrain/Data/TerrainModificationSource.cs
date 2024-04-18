using UnityEngine;

namespace TerrainSystem.Data
{
    public readonly struct TerrainModificationSource : ITerrainModificationSource, ITerrainModificationConfiguration
    {
        public const int SIZE_OF = ((sizeof(float) * 3) * 2) + (sizeof(uint) * 2) + (sizeof(float) * 4) + (sizeof(float) * 2) + (sizeof(int));

        public readonly Vector3 positionWS;
        public readonly Vector3 rotationWS;

        public readonly uint sdfType;
        public readonly Vector4 sizeAndRadius;
        public readonly float strength;
        public readonly float falloff;

        public readonly uint type;
        public readonly int modificationsBufferWriteIndex;

        public TerrainModificationSource(Vector3 positionWS, Vector3 rotationWS, uint sdfType, Vector4 sizeAndRadius, float strength, float falloff, uint type, int modificationsBufferWriteIndex)
        {
            this.positionWS = positionWS;
            this.rotationWS = rotationWS;
            this.sdfType = sdfType;
            this.sizeAndRadius = sizeAndRadius;
            this.strength = strength;
            this.falloff = falloff;
            this.type = type;
            this.modificationsBufferWriteIndex = modificationsBufferWriteIndex;
        }

        public Vector3 GetPosition() => positionWS;
        public Quaternion GetRotation() => Quaternion.Euler(rotationWS);
        public Vector3 GetScale() => Vector3.one;
        public uint GetTerrainType() => type;

        public Vector3 Size => sizeAndRadius;
        public float Radius => sizeAndRadius.w;
        public float Strength => strength;
        public float Falloff => falloff;
        public uint Type => type;
        public int ModificationsBufferWriteIndex => modificationsBufferWriteIndex;
    };
}