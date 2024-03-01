using TerrainSystem.Source;
using UnityEngine;

namespace TerrainSystem.Requester
{
    internal readonly struct TexturedTerrainModificationSource
    {
        public const int SIZE_OF = sizeof(float) * 3 + sizeof(float) * 3 + sizeof(uint) * 2 + sizeof(float) * 2 + sizeof(float) * 3;

        public readonly Vector3 positionWS;
        public readonly Vector3 rotationWS;

        public readonly uint alphaTextureIndex;
        public readonly Vector2 alphaTextureSize;

        public readonly float radius;
        public readonly float strength;
        public readonly float falloff;

        public readonly uint type;

        public TexturedTerrainModificationSource(Vector3 positionWS, Vector3 rotationWS, uint alphaTextureIndex, Vector2 alphaTextureSize, float radius, float strength, float falloff, uint type)
        {
            this.positionWS = positionWS;
            this.rotationWS = rotationWS;
            this.alphaTextureIndex = alphaTextureIndex;
            this.alphaTextureSize = alphaTextureSize;
            this.radius = radius;
            this.strength = strength;
            this.falloff = falloff;
            this.type = type;
        }

        public static TexturedTerrainModificationSource From(ITerrainModificationSource source, uint alphaTextureIndex, Vector2Int alphaTextureSize)
        {
            ITerrainModificationConfiguration configuration = source.GetConfiguration();
            return new TexturedTerrainModificationSource(
                source.GetPosition(),
                source.GetRotation().eulerAngles,
                alphaTextureIndex,
                alphaTextureSize,
                configuration.Radius,
                configuration.Strength,
                configuration.Falloff,
                source.GetTerrainType());
        }
    };
}