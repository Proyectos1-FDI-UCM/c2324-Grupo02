using UnityEngine;

namespace TerrainSystem.Data
{
    internal readonly struct TexturedTerrainModificationSource : ITerrainModificationSource, ITerrainModificationConfiguration
    {
        public const int SIZE_OF = ((sizeof(float) * 3) * 3) + (sizeof(uint) * 2) + (sizeof(int) * 2) + (sizeof(float) * 3);

        public readonly Vector3 positionWS;
        public readonly Vector3 rotationWS;

        public readonly uint alphaTextureIndex;
        public readonly Vector2Int alphaTextureSize;
        public readonly Vector3 alphaTextureScale;

        public readonly float radius;
        public readonly float strength;
        public readonly float falloff;

        public readonly uint type;

        public TexturedTerrainModificationSource(Vector3 positionWS, Vector3 rotationWS, uint alphaTextureIndex, Vector2Int alphaTextureSize, Vector3 alphaTextureScale, float radius, float strength, float falloff, uint type)
        {
            this.positionWS = positionWS;
            this.rotationWS = rotationWS;
            this.alphaTextureIndex = alphaTextureIndex;
            this.alphaTextureSize = alphaTextureSize;
            this.alphaTextureScale = alphaTextureScale;
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