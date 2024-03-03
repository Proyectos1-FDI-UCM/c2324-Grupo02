using UnityEngine;

namespace TerrainSystem.Data.Flyweight
{
    [CreateAssetMenu(fileName = "TerrainModificationConfiguration", menuName = "Terrain/Configuration/TerrainModificationConfiguration")]
    internal class TerrainModificationConfigurationFlyweight : ScriptableObject,
        ITerrainModificationSourceFlyweight<ITerrainModificationConfiguration>
    {
        private readonly struct Configuration : ITerrainModificationConfiguration
        {
            public float Radius { get; }
            public float Strength { get; }
            public float Falloff { get; }
            public uint Type { get; }
            public Configuration(float radius, float strength, float falloff, uint type)
            {
                Radius = radius;
                Strength = strength;
                Falloff = falloff;
                Type = type;
            }
        }

        [SerializeField]
        [Min(0.0f)]
        private float _radius;

        [SerializeField]
        [Min(0.0f)]
        private float _strength;

        [SerializeField]
        [Min(0.0f)]
        private float _falloff;

        [SerializeField]
        [Min(0)]
        private int _type;

        public ITerrainModificationConfiguration Create() =>
            new Configuration(_radius, _strength, _falloff, (uint)_type);
    }
}