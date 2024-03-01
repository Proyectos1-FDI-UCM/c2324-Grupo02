using TerrainSystem.Source;
using UnityEngine;

namespace TerrainSystem.Requester
{
    internal readonly struct TerrainModificationSource
    {
        public const int SIZE_OF = sizeof(float) * 3 + sizeof(float) * 3 + sizeof(uint) * 2 + sizeof(float) * 3;

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

        public static TerrainModificationSource From(ITerrainModificationSource source, uint sdfType)
        {
            ITerrainModificationConfiguration configuration = source.GetConfiguration();
            return new TerrainModificationSource(
                source.GetPosition(),
                source.GetRotation().eulerAngles,
                sdfType,
                configuration.Radius,
                configuration.Strength,
                configuration.Falloff,
                source.GetTerrainType());
        }
    };
}