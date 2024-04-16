using UnityEngine;

namespace TerrainSystem.Data.Flyweight
{
    [CreateAssetMenu(fileName = "TexturedTerrainModificationSource", menuName = "Terrain/Configuration/TexturedTerrainModificationSource")]
    internal class TexturedTerrainModificationSourceFlyweight : ScriptableObject,
        ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource, ITerrainModificationSource>
    {
        [SerializeField]
        [Min(0)]
        private int _alphaTextureIndex;

        [SerializeField]
        private Vector2Int _alphaTextureSize;

        [SerializeField]
        private Vector3 _alphaTextureScale;

        [SerializeField]
        private Vector3 _positionOffset;

        [SerializeField]
        private Vector3 _rotationOffset;

        [SerializeField]
        private TerrainModificationConfigurationFlyweight _configuration;

        public TexturedTerrainModificationSource CreateFrom<UFromSource>(UFromSource terrainModificationSource) where UFromSource : ITerrainModificationSource
        {
            ITerrainModificationConfiguration configuration = _configuration.Create();
            Vector3 scale = terrainModificationSource.GetScale();
            return new TexturedTerrainModificationSource(
                _positionOffset + terrainModificationSource.GetPosition(),
                _rotationOffset - terrainModificationSource.GetRotation().eulerAngles,
                (uint)_alphaTextureIndex,
                _alphaTextureSize,
                _alphaTextureScale,
                new Vector4(configuration.Size.x * scale.x, configuration.Size.y * scale.y, configuration.Size.z * scale.z, configuration.Radius),
                configuration.Strength,
                configuration.Falloff,
                configuration.Type);
        }
    }

}