using UnityEngine;
using UnityEngine.Rendering;

namespace TerrainSystem.Accessor
{
    internal readonly struct TerrainModificationShaderAccessor
    {
        public const string KERNEL_INITIALIZE_TERRAIN_TEXTURE = "InitializeTerrainTexture";
        public const string KERNEL_COPY_TO_TERRAIN_TEXTURE = "CopyToTerrainTexture";
        public const string KERNEL_COPY_FROM_TERRAIN_TEXTURE = "CopyFromTerrainTexture";
        public const string KERNEL_MODIFY_TERRAIN_TEXTURE = "ModifyTerrainTexture";
        public const string KERNEL_MODIFY_TERRAIN_WINDOW = "ModifyTerrainWindow";
        public const string KERNEL_COPY_TO_VISUALS_FROM_WINDOW = "CopyToVisualsFromWindow";

        public readonly byte kernelInitializeTerrainTexture;
        public readonly byte kernelCopyToTerrainTexture;
        public readonly byte kernelCopyFromTerrainTexture;
        public readonly byte kernelModifyTerrainTexture;
        public readonly byte kernelModifyTerrainWindow;
        public readonly byte kernelCopyToVisualsFromWindow;

        private readonly LocalKeyword _useTypedSdfKeyword;
        private readonly LocalKeyword _useTexturedSdfKeyword;

        private readonly ComputeShader _terrainComputeShader;

        public TerrainModificationShaderAccessor(ComputeShader terrainComputeShader)
        {
            kernelInitializeTerrainTexture = (byte)terrainComputeShader.FindKernel(KERNEL_INITIALIZE_TERRAIN_TEXTURE);
            kernelCopyToTerrainTexture = (byte)terrainComputeShader.FindKernel(KERNEL_COPY_TO_TERRAIN_TEXTURE);
            kernelCopyFromTerrainTexture = (byte)terrainComputeShader.FindKernel(KERNEL_COPY_FROM_TERRAIN_TEXTURE);
            kernelModifyTerrainTexture = (byte)terrainComputeShader.FindKernel(KERNEL_MODIFY_TERRAIN_TEXTURE);
            kernelModifyTerrainWindow = (byte)terrainComputeShader.FindKernel(KERNEL_MODIFY_TERRAIN_WINDOW);
            kernelCopyToVisualsFromWindow = (byte)terrainComputeShader.FindKernel(KERNEL_COPY_TO_VISUALS_FROM_WINDOW);
            _terrainComputeShader = terrainComputeShader;

            _useTypedSdfKeyword = new LocalKeyword(_terrainComputeShader, "USE_TYPED_SDF");
            _useTexturedSdfKeyword = new LocalKeyword(_terrainComputeShader, "USE_TEXTURED_SDF");
        }

        public void ConfigureCameraMatrices(Camera camera)
        {
            const string CLIP_TO_WORLD_MATRIX_NAME = "_ClipToWorldMatrix";
            Matrix4x4 projectionMatrix = GL.GetGPUProjectionMatrix(camera.projectionMatrix, false);
            _terrainComputeShader.SetMatrix(
                CLIP_TO_WORLD_MATRIX_NAME,
                (projectionMatrix * camera.worldToCameraMatrix).inverse);
        }

        public void ConfigureInitializationTerrainType(uint type)
        {
            const string INITIALIZATION_TERRAIN_TYPE_NAME = "_InitializationTerrainType";
            _terrainComputeShader.SetInt(
                INITIALIZATION_TERRAIN_TYPE_NAME,
                (int)type);
        }

        public void ConfigureTerrainTexture(int kernel, Texture texture, Vector2 currentOffset)
        {
            const string TERRAIN_TEXTURE_NAME = "_TerrainTexture";
            _terrainComputeShader.SetTexture(
                kernel,
                TERRAIN_TEXTURE_NAME,
                texture);

            ConfigureTerrainTexture(new Vector4(texture.width, texture.height, currentOffset.x, currentOffset.y));
        }

        public void ConfigureTerrainTexture(Vector4 sizeAndOffset)
        {
            const string TERRAIN_TEXTURE_SIZE_AND_OFFSET_NAME = "_TerrainTextureSizeAndOffset";
            _terrainComputeShader.SetVector(
                TERRAIN_TEXTURE_SIZE_AND_OFFSET_NAME,
                sizeAndOffset);
        }

        public void ConfigureTerrainTextureWindow(int kernel, Texture texture, Vector2 currentOffset)
        {
            const string TERRAIN_WINDOW_TEXTURE_NAME = "_TerrainWindowTexture";
            _terrainComputeShader.SetTexture(
                kernel,
                TERRAIN_WINDOW_TEXTURE_NAME,
                texture);

            ConfigureTerrainTextureWindow(new Vector4(texture.width, texture.height, currentOffset.x, currentOffset.y));
        }

        public void ConfigureTerrainTextureWindow(Vector4 sizeAndOffset)
        {
            const string TERRAIN_WINDOW_TEXTURE_SIZE_AND_OFFSET_NAME = "_TerrainWindowTextureSizeAndOffset";
            _terrainComputeShader.SetVector(
                TERRAIN_WINDOW_TEXTURE_SIZE_AND_OFFSET_NAME,
                sizeAndOffset);
        }

        public void ConfigureTerrainTypes(int kernel, Texture2DArray terrainTextureTypes, ComputeBuffer terrainModificationsBuffer)
        {
            ConfigureTerrainTypes(kernel, terrainTextureTypes.depth, terrainModificationsBuffer);
            ConfigureTerrainTypes(kernel, terrainTextureTypes);
        }

        public void ConfigureTerrainTypes(int kernel, Texture2DArray terrainTextureTypes)
        {
            const string TERRAIN_TYPES_TEXTURES_NAME = "_TerrainTypesTextures";
            _terrainComputeShader.SetTexture(
                kernel,
                TERRAIN_TYPES_TEXTURES_NAME,
                terrainTextureTypes);

            const string TERRAIN_TYPES_TEXTURES_SIZE_NAME = "_TerrainTypesTexturesSize";
            _terrainComputeShader.SetVector(
                TERRAIN_TYPES_TEXTURES_SIZE_NAME,
                new Vector2(terrainTextureTypes.width, terrainTextureTypes.height));
        }

        public void ConfigureTerrainTypes(int kernel, int terrainTypesCount, ComputeBuffer terrainModificationsBuffer)
        {
            const string TERRAIN_MODIFICATIONS_BUFFER_NAME = "_TerrainModifications";
            _terrainComputeShader.SetBuffer(
                kernel,
                TERRAIN_MODIFICATIONS_BUFFER_NAME,
                terrainModificationsBuffer);

            ConfigureTerrainTypes(terrainTypesCount);
        }

        public void ConfigureTerrainTypes(int terrainTypesCount)
        {
            const string TERRAIN_TYPES_COUNT_NAME = "_TerrainTypesCount";
            _terrainComputeShader.SetInt(
                TERRAIN_TYPES_COUNT_NAME,
                terrainTypesCount);
        }


        public void ConfigureTypeSources(int kernel, int count, ComputeBuffer sourcesBuffer)
        {
            const string SOURCE_BUFFER_NAME = "_Sources";
            _terrainComputeShader.SetBuffer(
                kernel,
                SOURCE_BUFFER_NAME,
                sourcesBuffer);

            const string SOURCE_COUNT_NAME = "_SourceCount";
            _terrainComputeShader.SetInt(
                SOURCE_COUNT_NAME,
                count);

            _terrainComputeShader.SetKeyword(in _useTypedSdfKeyword, true);
            _terrainComputeShader.SetKeyword(in _useTexturedSdfKeyword, false);
        }

        public void ConfigureTexturedSources(int kernel, int count, ComputeBuffer sourcesBuffer, Texture2DArray sources)
        {
            const string TEXTURED_SOURCES_NAME = "_TexturedSources";
            _terrainComputeShader.SetBuffer(
                kernel,
                TEXTURED_SOURCES_NAME,
                sourcesBuffer);

            const string TEXTURED_SOURCE_COUNT_NAME = "_TexturedSourceCount";
            _terrainComputeShader.SetInt(
                TEXTURED_SOURCE_COUNT_NAME,
                count);

            const string SOURCES_ALPHA_TEXTURES_NAME = "_SourcesAlphaTextures";
            _terrainComputeShader.SetTexture(
                kernel,
                SOURCES_ALPHA_TEXTURES_NAME,
            sources);

            _terrainComputeShader.SetKeyword(in _useTypedSdfKeyword, false);
            _terrainComputeShader.SetKeyword(in _useTexturedSdfKeyword, true);
        }

        public void ConfigureVisuals(int kernel, Texture visualsTexture)
        {
            const string TERRAIN_WINDOW_VISUALS_TEXTURE_NAME = "_TerrainWindowVisualsTexture";
            _terrainComputeShader.SetTexture(
                kernel,
                TERRAIN_WINDOW_VISUALS_TEXTURE_NAME,
                visualsTexture);
        }

        public void Dispatch(int kernel, Vector3 dimensions)
        {
            _terrainComputeShader.GetKernelThreadGroupSizes(
                kernel,
                out uint groupSizeX,
                out uint groupSizeY,
                out uint groupSizeZ);

            Vector3Int threadGroups = Vector3Int.CeilToInt(new Vector3(
                dimensions.x / groupSizeX,
                dimensions.y / groupSizeY,
                dimensions.z / groupSizeZ));

            _terrainComputeShader.Dispatch(
                kernel,
                threadGroups.x,
                threadGroups.y,
                threadGroups.z);
        }

        public void DispatchIntializeTerrain(Texture terrainTexture)
        {
            Dispatch(kernelInitializeTerrainTexture, new Vector3(terrainTexture.width, terrainTexture.height, 1));
        }

        public void DispatchModifyTerrain(Texture terrainTexture, Texture terrainWindowTexture)
        {
            Dispatch(kernelModifyTerrainTexture, new Vector3(terrainTexture.width, terrainTexture.height, 1));
            Dispatch(kernelCopyFromTerrainTexture, new Vector3(terrainWindowTexture.width, terrainWindowTexture.height, 1));
        }

        public void DispatchModifyWindow(Texture terrainWindowTexture)
        {
            Dispatch(kernelCopyFromTerrainTexture, new Vector3(terrainWindowTexture.width, terrainWindowTexture.height, 1));
            Dispatch(kernelModifyTerrainWindow, new Vector3(terrainWindowTexture.width, terrainWindowTexture.height, 1));
            Dispatch(kernelCopyToTerrainTexture, new Vector3(terrainWindowTexture.width, terrainWindowTexture.height, 1));
        }
    }
}