using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TerrainSystem.Data;
using TerrainSystem.Data.Flyweight;
using TerrainSystem.Queue;
using TerrainSystem.Requestable;
using TerrainSystem.Requestable.Retriever;
using TerrainSystem.Requestable.Retriever.Observable;
using UnityEngine;
using UnityEngine.Rendering;

namespace TerrainSystem.Requester
{
    [CreateAssetMenu(fileName = "TerrainModificationRequester", menuName = "Terrain/Requester/TerrainModificationRequester")]
    internal class TerrainModificationRequester : ScriptableObject, ITerrainModificationRequester,
        IInitializableTerrainModificationRequester,
        IObservableTerrainModificationRequester,
        ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TerrainModificationSource>>,
        ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource>>,
        ITerrainDataRetriever<PositionedTerrainVisuals>,
        ITerrainDataRetriever<PositionedTerrainVisualsWithNormals>,
        ITerrainDataRetriever<PositionedTerrainRawData>,
        ITerrainDataRetriever<TerrainModification[]>
    {
        [SerializeField]
        private ComputeShader _terrainModificationShader;

        [SerializeField]
        private Texture2DArray _visualsTextures;
        [SerializeField]
        private Texture2DArray _visualsNormalTextures;

        [SerializeField]
        private Texture2DArray _alphaTextures;

        [SerializeField]
        [Min(0)]
        private int _intialTerrainType;

        //[SerializeField]
        //[Obsolete]
        //private RenderTexture _debugTerrainRenderTexture;

        private List<ITerrainModificationSourceFlyweight<TerrainModificationSource>> _typedSources;
        private List<ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource>> _texturedSources;

        private Camera _camera;

        private RenderTexture _terrainRenderTexture;
        private RenderTexture _terrainWindowRenderTexture;

        private ComputeBuffer _typedSourcesBuffer;
        private ComputeBuffer _texturedSourcesBuffer;
        private ComputeBuffer _terrainModificationsBuffer;

        private TerrainModifierRequestable _terrainModifierRequestable;
        private ITerrainDataRetriever<PositionedTerrainVisuals> _terrainVisualsRetriever;
        private ITerrainDataRetriever<PositionedTerrainVisuals> _terrainNormalsRetriever;
        private ITerrainDataRetriever<PositionedTerrainRawData> _terrainRawDataRetriever;
        private ITerrainDataRetriever<TerrainModification[]> _terrainModificationsRetriever;
        private bool _pendingTerrainModificationRequest;

        public const int MAX_TERRAIN_TYPES = 32;
        public const int MAX_TERRAIN_SOURCES = 256;
        public bool Initialized { get; private set; }

        public event EventHandler ModificationRequested;

        public Texture2DArray VisualsTextures
        {
            private get => _visualsTextures;
            set
            {
                _visualsTextures = value;
                this.Reinitalize(
                    new Vector2Int(_terrainRenderTexture.width, _terrainRenderTexture.height),
                    new Vector2Int(_terrainWindowRenderTexture.width, _terrainRenderTexture.height),
                    _camera);
            }
        }

        public Texture2DArray AlphaTextures
        {
            private get => _alphaTextures;
            set
            {
                _alphaTextures = value;
                this.Reinitalize(
                    new Vector2Int(_terrainRenderTexture.width, _terrainRenderTexture.height),
                    new Vector2Int(_terrainWindowRenderTexture.width, _terrainRenderTexture.height),
                    _camera);
            }
        }

        public bool Initialize(RenderTexture terrainTexture, RenderTexture terrainWindowTexture, Camera camera)
        {
            _camera = camera;
            _terrainRenderTexture = terrainTexture;
            _terrainWindowRenderTexture = terrainWindowTexture;
            InitializeBuffers();

            _typedSources = new List<ITerrainModificationSourceFlyweight<TerrainModificationSource>>();
            _texturedSources = new List<ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource>>();

            _terrainModifierRequestable = new TerrainModifierRequestable(
                _terrainModificationShader,
                _terrainWindowRenderTexture,
                _typedSourcesBuffer,
                _texturedSourcesBuffer,
            _terrainModificationsBuffer,
            AlphaTextures);

            _terrainVisualsRetriever = new TerrainVisualsRetriever(_terrainModificationShader, VisualsTextures, _terrainWindowRenderTexture, camera);
            _terrainNormalsRetriever = new TerrainVisualsRetriever(_terrainModificationShader, _visualsNormalTextures, _terrainWindowRenderTexture, camera);
            _terrainRawDataRetriever = new TerrainRawDataRetriever(_terrainModificationShader, _terrainRenderTexture, _terrainWindowRenderTexture, camera);
            _terrainModificationsRetriever = new TerrainModificationsRetriever(_terrainModificationsBuffer, OnTerrainModificationsRetrieved);
            _pendingTerrainModificationRequest = false;
            return Initialized = true;
        }

        public bool Initialize(Vector2Int terrainTextureSize, Vector2Int terrainWindowTextureSize, UnityEngine.Camera camera, out RenderTexture terrainRenderTexture, out RenderTexture terrainWindowRenderTexture)
        {
            terrainRenderTexture = new RenderTexture(
                terrainTextureSize.x,
                terrainTextureSize.y,
                0,
                RenderTextureFormat.R8,
                RenderTextureReadWrite.Linear)
            {
                filterMode = FilterMode.Point,
                enableRandomWrite = true
            };

            terrainWindowRenderTexture = new RenderTexture(
                terrainWindowTextureSize.x,
                terrainWindowTextureSize.y,
                0,
                RenderTextureFormat.R8,
                RenderTextureReadWrite.Linear)
            {
                filterMode = FilterMode.Point,
                enableRandomWrite = true
            };

            bool success = Initialize(terrainRenderTexture, terrainWindowRenderTexture, camera);
            _terrainModifierRequestable.InitializeTerrainWith((uint)_intialTerrainType, _camera, _terrainRenderTexture);
            return success;
        }

        private void InitializeBuffers()
        {
            _typedSourcesBuffer = new ComputeBuffer(MAX_TERRAIN_SOURCES, TerrainModificationSource.SIZE_OF);

            _texturedSourcesBuffer = new ComputeBuffer(MAX_TERRAIN_SOURCES, TexturedTerrainModificationSource.SIZE_OF);

            _terrainModificationsBuffer = new ComputeBuffer(MAX_TERRAIN_TYPES, TerrainModification.SIZE_OF);
        }

        private void OnTerrainModificationsRetrieved(AsyncGPUReadbackRequest request) =>
            _pendingTerrainModificationRequest = false;

        public bool Finalize()
        {
            if (_terrainRenderTexture != null)
                _terrainRenderTexture.Release();
            if (_terrainWindowRenderTexture != null)
                _terrainWindowRenderTexture.Release();

            _typedSourcesBuffer?.Release();
            _texturedSourcesBuffer?.Release();
            _terrainModificationsBuffer?.Release();
            return !(Initialized = false);
        }

        public void RequestModification()
        {
            _terrainModifierRequestable.terrainModifierRequestable
                .ConfigureWith(_typedSources.Count, MAX_TERRAIN_TYPES, _terrainRenderTexture, _camera);
            _terrainModifierRequestable
                .ModifyTerrainWith(Array.ConvertAll(_typedSources.ToArray(), CreateTyped), _camera, _terrainRenderTexture);

            _terrainModifierRequestable.texturedTerrainModifierRequestable
                .ConfigureWith(_texturedSources.Count, MAX_TERRAIN_TYPES, _terrainRenderTexture, _camera);
            _terrainModifierRequestable
                .ModifyTerrainWith(Array.ConvertAll(_texturedSources.ToArray(), CreateTextured), _camera, _terrainRenderTexture);

            ModificationRequested?.Invoke(this, EventArgs.Empty);
            //Graphics.Blit(_terrainRenderTexture, _debugTerrainRenderTexture);

            static TerrainModificationSource CreateTyped(ITerrainModificationSourceFlyweight<TerrainModificationSource> source) =>
                source.Create();

            static TexturedTerrainModificationSource CreateTextured(ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource> source) =>
                source.Create();
        }

        public bool Enqueue(ITerrainModificationSourceFlyweight<TerrainModificationSource> modification)
        {
            if (_typedSources.Count >= MAX_TERRAIN_SOURCES)
                return false;

            _typedSources.Add(modification);
            return true;
        }

        public bool Dequeue(ITerrainModificationSourceFlyweight<TerrainModificationSource> modification) =>
            _typedSources.Remove(modification);

        public bool Enqueue(ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource> modification)
        {
            if (_texturedSources.Count >= MAX_TERRAIN_SOURCES)
                return false;

            _texturedSources.Add(modification);
            return true;
        }

        public bool Dequeue(ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource> modification) =>
            _texturedSources.Remove(modification);

        public Task Retrieve(in PositionedTerrainVisuals destination) => _terrainVisualsRetriever.Retrieve(in destination);
        public Task<PositionedTerrainVisuals> Retrieve() => _terrainVisualsRetriever.Retrieve();
        public Task Retrieve(in TerrainModification[] destination)
        {
            Task retrieval = Task.CompletedTask;
            if (!_pendingTerrainModificationRequest)
            {
                retrieval = _terrainModificationsRetriever.Retrieve(in destination);
                _pendingTerrainModificationRequest = true;
            }

            return retrieval;
        }

        Task<TerrainModification[]> ITerrainDataRetriever<TerrainModification[]>.Retrieve()
        {
            TerrainModification[] modifications = new TerrainModification[0];
            Task<TerrainModification[]> retrieval = Task.FromResult(modifications);

            if (!_pendingTerrainModificationRequest)
            {
                retrieval = _terrainModificationsRetriever.Retrieve();
                _pendingTerrainModificationRequest = true;
            }

            return retrieval;
        }

        public Task Retrieve(in PositionedTerrainVisualsWithNormals destination)
        {
            _terrainVisualsRetriever.Retrieve(in destination.albedoVisuals);
            return _terrainNormalsRetriever.Retrieve(new PositionedTerrainVisuals(destination.normalsRenderTexture, destination.albedoVisuals.position));
        }

        async Task<PositionedTerrainVisualsWithNormals> ITerrainDataRetriever<PositionedTerrainVisualsWithNormals>.Retrieve() =>
            new PositionedTerrainVisualsWithNormals(
                await _terrainVisualsRetriever.Retrieve(),
                (await _terrainNormalsRetriever.Retrieve()).renderTexture);

        public Task Retrieve(in PositionedTerrainRawData destination) => _terrainRawDataRetriever.Retrieve(destination);
        Task<PositionedTerrainRawData> ITerrainDataRetriever<PositionedTerrainRawData>.Retrieve() =>
            _terrainRawDataRetriever.Retrieve();
    }
}