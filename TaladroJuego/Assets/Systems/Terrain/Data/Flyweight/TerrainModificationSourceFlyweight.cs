using UnityEngine;

namespace TerrainSystem.Data.Flyweight
{
    [CreateAssetMenu(fileName = "TerrainModificationSource", menuName = "Terrain/Configuration/TerrainModificationSource")]
    internal class TerrainModificationSourceFlyweight : ScriptableObject,
        ITerrainModificationSourceFlyweight<TerrainModificationSource, ITerrainModificationSource>
    {
        [SerializeField]
        [Min(0)]
        private int _sdfType;

        [SerializeField]
        private TerrainModificationConfigurationFlyweight _configuration;

        public TerrainModificationSource CreateFrom<UFromSource>(UFromSource terrainModificationSource) where UFromSource : ITerrainModificationSource
        {
            ITerrainModificationConfiguration configuration = _configuration.Create();
            return new TerrainModificationSource(
                terrainModificationSource.GetPosition(),
                -terrainModificationSource.GetRotation().eulerAngles,
                (uint)_sdfType,
                new Vector4(configuration.Size.x, configuration.Size.y, configuration.Size.z, configuration.Radius),
                configuration.Strength,
                configuration.Falloff,
                configuration.Type);
        }
    }

}