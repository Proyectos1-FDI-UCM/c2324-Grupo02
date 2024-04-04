using SaveSystem.Saveable;
using SaveSystem.Saveable.Components;
using System.Linq;
using TerrainSystem.Requester;
using UnityEngine;

namespace SaveableSystem.TerrainSystem
{
    internal class SaveableTerrainTexture : MonoBehaviour,
        IInitializableTerrainModificationRequester,
        ISaveable<IReadOnlySaveable<Texture2D>>
    {
        [SerializeField]
        private SaveableTexture.EncodingType _encodingType = SaveableTexture.EncodingType.Qoi;

        private IInitializableTerrainModificationRequester _terrainModificationRequester;
        private RenderTexture _terrainTexture;
        private RenderTexture _terrainWindowTexture;
        private Camera _camera;

        public bool Initialized => _terrainModificationRequester.Initialized;

        public IReadOnlySaveable<Texture2D> GetData() => new SaveableTexture(_terrainTexture, _encodingType).GetData();
        public bool TrySetData(IReadOnlySaveable<Texture2D> saveData) => new SaveableTexture(_terrainTexture, _encodingType).TrySetData(saveData);

        private void Awake()
        {
            _terrainModificationRequester =
                GetComponentsInChildren<IInitializableTerrainModificationRequester>()
                .FirstOrDefault(i => i != (IInitializableTerrainModificationRequester)this);
        }

        public bool Initialize(RenderTexture terrainTexture, RenderTexture terrainWindowTexture, Camera camera)
        {
            (_terrainTexture, _terrainWindowTexture, _camera) = (terrainTexture, terrainWindowTexture, camera);
            return _terrainModificationRequester.Initialize(
                _terrainTexture,
                _terrainWindowTexture,
                _camera);
        }

        public bool Initialize(Vector2Int terrainTextureSize, Vector2Int terrainWindowTextureSize, Camera camera, out RenderTexture terrainRenderTexture, out RenderTexture terrainWindowRenderTexture)
        {
            bool success = _terrainModificationRequester.Initialize(
                terrainTextureSize,
                terrainWindowTextureSize,
                _camera = camera,
                out _terrainTexture,
                out _terrainWindowTexture);

            terrainRenderTexture = _terrainTexture;
            terrainWindowRenderTexture = _terrainWindowTexture;
            return success;
        }

        public bool Finalize() => _terrainModificationRequester.Finalize();
    }
}