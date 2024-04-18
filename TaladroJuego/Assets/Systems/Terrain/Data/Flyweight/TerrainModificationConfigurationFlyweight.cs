using UnityEngine;

namespace TerrainSystem.Data.Flyweight
{
    [CreateAssetMenu(fileName = "TerrainModificationConfiguration", menuName = "Terrain/Configuration/TerrainModificationConfiguration")]
    internal class TerrainModificationConfigurationFlyweight : ScriptableObject,
        ITerrainModificationSourceFlyweight<ITerrainModificationConfiguration>
    {
        private readonly struct Configuration : ITerrainModificationConfiguration
        {
            public Vector3 Size { get; }
            public float Radius { get; }
            public float Strength { get; }
            public float Falloff { get; }
            public uint Type { get; }
            public int ModificationsBufferWriteIndex { get; }
            public Configuration(Vector3 size, float radius, float strength, float falloff, uint type, int modificationsBufferWriteIndex)
            {
                Size = size;
                Radius = radius;
                Strength = strength;
                Falloff = falloff;
                Type = type;
                ModificationsBufferWriteIndex = modificationsBufferWriteIndex;
            }
        }

        [SerializeField]
        private Vector3 _size = Vector3.one;

        [SerializeField]
        [Min(0.0f)]
        private float _radius = 1.0f;

        [SerializeField]
        [Min(0.0f)]
        private float _strength = 1.0f;

        [SerializeField]
        [Min(0.0f)]
        private float _falloff = 1.0f;

        [SerializeField]
        [Min(0)]
        private int _type = 0;

        [SerializeField]
        private bool _writesToModificationsBuffer = false;

        [SerializeField]
        [Min(0)]
        private int _modificationsBufferWriteIndex = 0;

        public ITerrainModificationConfiguration Create() =>
            new Configuration(_size, _radius, _strength, _falloff, (uint)_type, _writesToModificationsBuffer ? _modificationsBufferWriteIndex : -1);
    }
}