using System;
using TerrainSystem.Data;
using TerrainSystem.Data.Flyweight;
using TerrainSystem.Requestable;
using TerrainSystem.Requestable.Retriever;
using UnityEngine;

namespace TerrainSystem.Test
{
    [Obsolete]
    internal class TerrainDestructionTest : MonoBehaviour
    {
        [Serializable]
        public struct Data : ITerrainModificationSource
        {
            [field: SerializeField]
            public Transform Source { get; private set; }

            [field: SerializeField]
            public TexturedTerrainModificationSourceFlyweight Flyweight { get; private set; }

            public readonly Vector3 GetPosition() => Source.position;
            public readonly Quaternion GetRotation() => Source.rotation;
            public readonly Vector3 GetScale() => Source.lossyScale;
        }

        [SerializeField]
        private Data[] _data;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private ComputeShader _terrainComputeShader;

        [SerializeField]
        private Vector2Int _terrainSize;

        [SerializeField]
        private Vector2Int _terrainWindowSize;

        private const int MAX_TERRAIN_TYPES = 32;
        private RenderTexture _terrainRenderTexture;
        private RenderTexture _terrainWindowRenderTexture;

        private ComputeBuffer _typedSourcesBuffer;
        private ComputeBuffer _texturedSourcesBuffer;
        private ComputeBuffer _terrainModificationsBuffer;

        [SerializeField]
        private Texture2DArray _alphaTextures;

        [SerializeField]
        private Texture2DArray _visualsTextures;

        [SerializeField]
        private RenderTexture _visualsTexture;

        private TerrainModifierRequestable _terrainModifierRequestable;
        private ITerrainDataRetriever<RenderTexture> _terrainDataRetriever;

        private void Awake()
        {
            _terrainRenderTexture = new RenderTexture(
                _terrainSize.x, _terrainSize.y, 0,
                RenderTextureFormat.R8, RenderTextureReadWrite.Linear)
            {
                filterMode = FilterMode.Point,
                enableRandomWrite = true
            };

            _terrainWindowRenderTexture = new RenderTexture(
                _terrainWindowSize.x, _terrainWindowSize.y, 0,
                RenderTextureFormat.R8, RenderTextureReadWrite.Linear)
            {
                filterMode = FilterMode.Point,
                enableRandomWrite = true
            };

            const int MAX_SOURCES = 32;
            _typedSourcesBuffer = new ComputeBuffer(MAX_SOURCES, TerrainModificationSource.SIZE_OF);

            _texturedSourcesBuffer = new ComputeBuffer(MAX_SOURCES, TexturedTerrainModificationSource.SIZE_OF);

            _terrainModificationsBuffer = new ComputeBuffer(MAX_TERRAIN_TYPES, sizeof(float));

            _terrainModifierRequestable = new TerrainModifierRequestable(
                _terrainComputeShader,
                _terrainWindowRenderTexture,
                _typedSourcesBuffer,
                _texturedSourcesBuffer,
                _terrainModificationsBuffer,
                _alphaTextures);

            //_terrainDataRetriever = new TerrainVisualsRetriever(_terrainComputeShader, _visualsTextures, _terrainWindowRenderTexture, _camera);
        }

        private void Start()
        {
            _terrainModifierRequestable.InitializeTerrainWith(0, _camera, _terrainRenderTexture);
        }

        private void FixedUpdate()
        {
            _terrainModifierRequestable.texturedTerrainModifierRequestable.ConfigureWith(_data.Length, MAX_TERRAIN_TYPES, _terrainRenderTexture, _camera);
            _terrainModifierRequestable.ModifyTerrainWith(Array.ConvertAll(_data, d => d.Flyweight.CreateFrom(d)), _camera, _terrainRenderTexture);
        }

        private void LateUpdate()
        {
            _terrainDataRetriever.TryRetrieve(_visualsTexture);
        }

        private void OnDestroy()
        {
            _terrainRenderTexture.Release();
            _terrainWindowRenderTexture.Release();

            _typedSourcesBuffer.Release();
            _texturedSourcesBuffer.Release();
            _terrainModificationsBuffer.Release();
        }

    }
}