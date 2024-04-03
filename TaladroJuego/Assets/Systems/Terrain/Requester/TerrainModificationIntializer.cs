using UnityEngine;

namespace TerrainSystem.Requester
{
    internal class TerrainModificationIntializer : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Vector2Int _terrainTextureSize;

        [SerializeField]
        private Vector2Int _terrainWindowTextureSize;

        [SerializeField]
        private bool _initializeOnAwake = true;

        private IInitializableTerrainModificationRequester _initializable;

        private void Awake()
        {
            _initializable = GetComponentInChildren<IInitializableTerrainModificationRequester>();
           _ = _initializeOnAwake && Initialize(out _, out _);
        }

        private void OnDestroy()
        {
            _initializable.Finalize();
        }

        public bool Initialize(out RenderTexture terrainRenderTexture, out RenderTexture terrainWindowRenderTexture) =>
            _initializable.Initialize(_terrainTextureSize, _terrainWindowTextureSize, _camera, out terrainRenderTexture, out terrainWindowRenderTexture);
    }
}