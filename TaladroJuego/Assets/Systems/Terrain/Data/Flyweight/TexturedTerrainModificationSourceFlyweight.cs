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
        private Texture2D _alphaTexture;

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
            return new TexturedTerrainModificationSource(
                _positionOffset + terrainModificationSource.GetPosition(),
                _rotationOffset - terrainModificationSource.GetRotation().eulerAngles,
                (uint)_alphaTextureIndex,
                new Vector2Int(_alphaTexture.width, _alphaTexture.height),
                _alphaTextureScale,
                configuration.Radius,
                configuration.Strength,
                configuration.Falloff,
                configuration.Type);
            // TODO - Bind
        }
    }

}