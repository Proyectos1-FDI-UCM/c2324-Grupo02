using UnityEngine;

namespace TerrainSystem.Data
{
    public readonly struct TexturedTerrainModificationSource : ITerrainModificationSource, ITerrainModificationConfiguration
    {
        public const int SIZE_OF = ((sizeof(float) * 3) * 3) + (sizeof(uint) * 2) + (sizeof(int) * 2) + (sizeof(float) * 4) + (sizeof(float) * 2);

        public readonly Vector3 positionWS;
        public readonly Vector3 rotationWS;

        public readonly uint alphaTextureIndex;
        public readonly Vector2Int alphaTextureSize;
        public readonly Vector3 alphaTextureScale;

        public readonly Vector4 sizeAndRadius;
        public readonly float strength;
        public readonly float falloff;

        public readonly uint type;

        public TexturedTerrainModificationSource(Vector3 positionWS, Vector3 rotationWS, uint alphaTextureIndex, Vector2Int alphaTextureSize, Vector3 alphaTextureScale, Vector4 sizeAndRadius, float strength, float falloff, uint type)
        {
            this.positionWS = positionWS;
            this.rotationWS = rotationWS;
            this.alphaTextureIndex = alphaTextureIndex;
            this.alphaTextureSize = alphaTextureSize;
            this.alphaTextureScale = alphaTextureScale;
            this.sizeAndRadius = sizeAndRadius;
            this.strength = strength;
            this.falloff = falloff;
            this.type = type;
        }

        public Vector3 GetPosition() => positionWS;
        public Quaternion GetRotation() => Quaternion.Euler(rotationWS);
        public uint GetTerrainType() => type;

        public Vector3 Size => sizeAndRadius;
        public float Radius => sizeAndRadius.w;
        public float Strength => strength;
        public float Falloff => falloff;
        public uint Type => type;
    };
}