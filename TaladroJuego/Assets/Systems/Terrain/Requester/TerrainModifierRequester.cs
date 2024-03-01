using System.Collections.Generic;
using TerrainSystem.Modifier;
using TerrainSystem.Source;
using UnityEngine;

namespace TerrainSystem.Requester
{
    [CreateAssetMenu(fileName = "TerrainModifierRequester", menuName = "Terrain/Requester/TerrainModifierRequester")]
    internal class TerrainModifierRequester : ScriptableObject,
        ITerrainModifierRequestable<ITerrainModifier<TerrainModificationSource>>,
        ITerrainModifierRequestable<ITerrainModifier<TexturedTerrainModificationSource>>,
        ITerrainModifierRequester<TerrainModificationSource>,
        ITerrainModifierRequester<TexturedTerrainModificationSource>
    {
        private TerrainModificationShaderAccessor _accessor;

        [SerializeField]
        private ComputeShader _terrainComputeShader;

        [SerializeField]
        private Texture2DArray _terrainTypesTextures;

        private Camera _camera;

        [SerializeField]
        private Vector2Int _testSize;

        private RenderTexture _terrainTexture;
        private RenderTexture _terrainWindowTexture;

        private ComputeBuffer _sourcesBuffer;
        private ComputeBuffer _terrainModificationsBuffer;

        private void OnEnable()
        {
            // TODO
            _camera = Camera.main;

            _terrainTexture = new RenderTexture(
                _testSize.x,
                _testSize.y,
                0,
                RenderTextureFormat.R8,
                RenderTextureReadWrite.Linear);

            _terrainWindowTexture = new RenderTexture(
                _testSize.x,
                _testSize.y,
                0,
                RenderTextureFormat.R8,
                RenderTextureReadWrite.Linear);

            _terrainModificationsBuffer = new ComputeBuffer(
                32,
                sizeof(float));

            _sourcesBuffer = new ComputeBuffer(
                32,
                TerrainModificationSource.SIZE_OF);

            _accessor = new TerrainModificationShaderAccessor(
                _terrainComputeShader,
                _terrainTexture,
                _terrainWindowTexture,
                _sourcesBuffer,
                _terrainModificationsBuffer);
        }

        public void Initialize()
        {
            _camera = Camera.main;

            _terrainTexture = new RenderTexture(
                _testSize.x,
                _testSize.y,
                0,
                RenderTextureFormat.R8,
                RenderTextureReadWrite.Linear);
            _terrainTexture.enableRandomWrite = true;

            _terrainWindowTexture = new RenderTexture(
                _testSize.x,
                _testSize.y,
                0,
                RenderTextureFormat.R8,
                RenderTextureReadWrite.Linear);
            _terrainWindowTexture.enableRandomWrite = true;

            _terrainModificationsBuffer = new ComputeBuffer(
                32,
                sizeof(float));

            _sourcesBuffer = new ComputeBuffer(
                32,
                TerrainModificationSource.SIZE_OF);

            _accessor = new TerrainModificationShaderAccessor(
                _terrainComputeShader,
                _terrainTexture,
                _terrainWindowTexture,
                _sourcesBuffer,
                _terrainModificationsBuffer);
        }

        public bool TryInitializeTerrainTo(uint type)
        {
            int kernel = _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_INITIALIZE_TERRAIN_TEXTURE);

            _accessor.ConfigureTerrainTexture(kernel);

            _accessor.ConfigureInitializationTerrainType(type);

            _accessor.Dispatch(TerrainModificationShaderAccessor.KERNEL_INITIALIZE_TERRAIN_TEXTURE, new Vector3(_terrainTexture.width, _terrainTexture.height, 1));
            return true;
        }

        public bool TryModifyTextureWithTyped(TerrainModificationSource[] sources)
        {
            //_sourcesBuffer?.Release();

            //_sourcesBuffer = new ComputeBuffer(
            //    sources.Length,
            //    TerrainModificationSource.SIZE_OF);
            _sourcesBuffer.SetData(sources);

            Configure(sources, _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_MODIFY_TERRAIN_TEXTURE));
            Configure(sources, _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_COPY_FROM_TERRAIN_TEXTURE));

            _accessor.DispatchModifyTerrain();

            return true;
        }

        public bool TryModifyWindowWithTyped(TerrainModificationSource[] sources)
        {
            //_sourcesBuffer?.Release();

            //_sourcesBuffer = new ComputeBuffer(
            //    sources.Length,
            //    TerrainModificationSource.SIZE_OF);
            _sourcesBuffer.SetData(sources);

            Configure(sources, _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_COPY_FROM_TERRAIN_TEXTURE));
            Configure(sources, _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_MODIFY_TERRAIN_WINDOW));
            Configure(sources, _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_COPY_TO_TERRAIN_TEXTURE));

            _accessor.DispatchModifyWindow();

            return true;
        }

        public bool TryModifyTextureWithTextured(TexturedTerrainModificationSource[] sources)
        {
            //_sourcesBuffer?.Release();

            //_sourcesBuffer = new ComputeBuffer(
            //    sources.Length,
            //    TerrainModificationSource.SIZE_OF);
            _sourcesBuffer.SetData(sources);

            Configure(sources, _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_MODIFY_TERRAIN_TEXTURE));
            Configure(sources, _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_COPY_FROM_TERRAIN_TEXTURE));

            _accessor.DispatchModifyTerrain();

            return true;
        }

        public bool TryModifyWindowWithTextured(TexturedTerrainModificationSource[] sources)
        {
            //_sourcesBuffer?.Release();

            //_sourcesBuffer = new ComputeBuffer(
            //    sources.Length,
            //    TerrainModificationSource.SIZE_OF);
            _sourcesBuffer.SetData(sources);

            Configure(sources, _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_COPY_FROM_TERRAIN_TEXTURE));
            Configure(sources, _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_MODIFY_TERRAIN_WINDOW));
            Configure(sources, _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_COPY_TO_TERRAIN_TEXTURE));

            _accessor.DispatchModifyWindow();

            return true;
        }

        private void Configure(TerrainModificationSource[] sources, int kernel)
        {
            _accessor.ConfigureTypeSources(kernel, sources.Length);
            _accessor.ConfigureTerrainTypes(kernel, _terrainTypesTextures);

            _accessor.ConfigureTerrainTexture(kernel);
            _accessor.ConfigureTerrainTextureWindow(kernel);

            _accessor.ConfigureCameraMatrices(_camera);
        }

        private void Configure(TexturedTerrainModificationSource[] sources, int kernel)
        {
            _accessor.ConfigureTexturedSources(kernel, sources.Length);
            _accessor.ConfigureTerrainTypes(kernel, _terrainTypesTextures);

            _accessor.ConfigureTerrainTexture(kernel);
            _accessor.ConfigureTerrainTextureWindow(kernel);

            _accessor.ConfigureCameraMatrices(_camera);
        }

        public void Retrieve(RenderTexture destination)
        {
            int kernel = _accessor.GetKernel(TerrainModificationShaderAccessor.KERNEL_COPY_TO_VISUALS_FROM_WINDOW);

            _accessor.ConfigureTerrainTypes(kernel, _terrainTypesTextures);
            _accessor.ConfigureTerrainTextureWindow(kernel);

            RenderTexture visuals = new RenderTexture(destination.descriptor)
            {
                width = _terrainWindowTexture.width,
                height = _terrainWindowTexture.height,
                enableRandomWrite = true
            };

            _accessor.ConfigureVisuals(kernel, visuals);
            _accessor.Dispatch(kernel, new Vector3(visuals.width, visuals.height, 1));

            Graphics.Blit(visuals, destination);
            visuals.Release();
        }

        public bool TryModifyWith(TerrainModificationSource[] sources) =>
            TryModifyTextureWithTyped(sources);

        public bool TryModifyWith(TexturedTerrainModificationSource[] sources) =>
            TryModifyTextureWithTextured(sources);

        bool ITerrainModifierRequestable<ITerrainModifier<TerrainModificationSource>>.TryInitializeTerrainTo(uint type) =>
            TryInitializeTerrainTo(type);

        bool ITerrainModifierRequestable<ITerrainModifier<TerrainModificationSource>>.TryModifyWith<UModifier>(UModifier modifier, IReadOnlyCollection<ITerrainModificationSource> modificationSources) =>
            modifier.TryModify(this, modificationSources);

        bool ITerrainModifierRequestable<ITerrainModifier<TexturedTerrainModificationSource>>.TryInitializeTerrainTo(uint type) =>
            TryInitializeTerrainTo(type);

        bool ITerrainModifierRequestable<ITerrainModifier<TexturedTerrainModificationSource>>.TryModifyWith<UModifier>(UModifier modifier, IReadOnlyCollection<ITerrainModificationSource> modificationSources) =>
            modifier.TryModify(this, modificationSources);
    }
}