using System.Collections.Generic;
using UnityEngine;

namespace TerrainSystem.Requester
{
    internal readonly struct TerrainModificationShaderAccessor
    {
        public const string KERNEL_INITIALIZE_TERRAIN_TEXTURE = "InitializeTerrainTexture";

        public const string KERNEL_COPY_TO_TERRAIN_TEXTURE = "CopyToTerrainTexture";
        public const string KERNEL_COPY_FROM_TERRAIN_TEXTURE = "CopyFromTerrainTexture";

        public const string KERNEL_MODIFY_TERRAIN_TEXTURE = "ModifyTerrainTexture";
        public const string KERNEL_MODIFY_TERRAIN_WINDOW = "ModifyTerrainWindow";

        public const string KERNEL_COPY_TO_VISUALS_FROM_WINDOW = "CopyToVisualsFromWindow";

        private readonly Dictionary<string, int> _kernels;
        private readonly ComputeShader _terrainComputeShader;

        private readonly RenderTexture _terrainTexture;
        private readonly RenderTexture _terrainWindowTexture;

        private readonly ComputeBuffer _sourcesBuffer;
        private readonly ComputeBuffer _terrainModificationsBuffer;

        public TerrainModificationShaderAccessor(ComputeShader terrainComputeShader, RenderTexture terrainTexture, RenderTexture terrainWindowTexture, ComputeBuffer sourcesBuffer, ComputeBuffer terrainModificationsBuffer)
        {
            _kernels = new Dictionary<string, int>(6)
            {
                { KERNEL_INITIALIZE_TERRAIN_TEXTURE,  terrainComputeShader.FindKernel(KERNEL_INITIALIZE_TERRAIN_TEXTURE) },
                { KERNEL_COPY_TO_TERRAIN_TEXTURE,     terrainComputeShader.FindKernel(KERNEL_COPY_TO_TERRAIN_TEXTURE) },
                { KERNEL_COPY_FROM_TERRAIN_TEXTURE,   terrainComputeShader.FindKernel(KERNEL_COPY_FROM_TERRAIN_TEXTURE) },
                { KERNEL_MODIFY_TERRAIN_TEXTURE,      terrainComputeShader.FindKernel(KERNEL_MODIFY_TERRAIN_TEXTURE) },
                { KERNEL_MODIFY_TERRAIN_WINDOW,       terrainComputeShader.FindKernel(KERNEL_MODIFY_TERRAIN_WINDOW) },
                { KERNEL_COPY_TO_VISUALS_FROM_WINDOW, terrainComputeShader.FindKernel(KERNEL_COPY_TO_VISUALS_FROM_WINDOW) }
            };

            _terrainComputeShader = terrainComputeShader;
            _terrainTexture = terrainTexture;
            _terrainWindowTexture = terrainWindowTexture;
            _sourcesBuffer = sourcesBuffer;
            _terrainModificationsBuffer = terrainModificationsBuffer;
        }

        public void ConfigureCameraMatrices(Camera camera)
        {
            const string CLIP_TO_WORLD_MATRIX_NAME = "_ClipToWorldMatrix";
            //camera.matrix
            //Render
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

        public void ConfigureTerrainTexture(int kernel)
        {
            const string TERRAIN_TEXTURE_NAME = "_TerrainTexture";
            _terrainComputeShader.SetTexture(
                kernel,
                TERRAIN_TEXTURE_NAME,
                _terrainTexture);

            const string TERRAIN_TEXTURE_SIZE_NAME = "_TerrainTextureSize";
            _terrainComputeShader.SetVector(
                TERRAIN_TEXTURE_SIZE_NAME,
                new Vector2(_terrainTexture.width, _terrainTexture.height));
        }

        public void ConfigureTerrainTextureWindow(int kernel)
        {
            const string TERRAIN_WINDOW_TEXTURE_NAME = "_TerrainWindowTexture";
            _terrainComputeShader.SetTexture(
                kernel,
                TERRAIN_WINDOW_TEXTURE_NAME,
                _terrainWindowTexture);

            const string TERRAIN_WINDOW_TEXTURE_SIZE_AND_OFFSET_NAME = "_TerrainWindowTextureSizeAndOffset";
            _terrainComputeShader.SetVector(
                TERRAIN_WINDOW_TEXTURE_SIZE_AND_OFFSET_NAME,
                new Vector4(
                    _terrainWindowTexture.width,
                    _terrainWindowTexture.height,
                    0,
                    0));
            // TODO
        }

        public void ConfigureTerrainTypes(int kernel, Texture2DArray terrainTextureTypes)
        {
            const string TERRAIN_MODIFICATIONS_BUFFER_NAME = "_TerrainModifications";
            _terrainComputeShader.SetBuffer(
                kernel,
                TERRAIN_MODIFICATIONS_BUFFER_NAME,
                _terrainModificationsBuffer);

            const string TERRAIN_TYPES_COUNT_NAME = "_TerrainTypesCount";
            _terrainComputeShader.SetInt(
                TERRAIN_TYPES_COUNT_NAME,
                terrainTextureTypes.depth);

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

        public void ConfigureTypeSources(int kernel, int count)
        {
            const string SOURCE_BUFFER_NAME = "_Sources";
            _terrainComputeShader.SetBuffer(
                kernel,
                SOURCE_BUFFER_NAME,
                _sourcesBuffer);

            const string SOURCE_COUNT_NAME = "_SourceCount";
            _terrainComputeShader.SetInt(
                SOURCE_COUNT_NAME,
                count);
        }

        public void ConfigureTexturedSources(int kernel, int count)
        {
            const string TEXTURED_SOURCES_NAME = "_TexturedSources";
            _terrainComputeShader.SetBuffer(
                kernel,
                TEXTURED_SOURCES_NAME,
                _sourcesBuffer);

            const string TEXTURED_SOURCE_COUNT_NAME = "_TexturedSourceCount";
            _terrainComputeShader.SetInt(
                TEXTURED_SOURCE_COUNT_NAME,
                count);
        }

        public void ConfigureVisuals(int kernel, Texture texture)
        {
            const string TERRAIN_WINDOW_VISUALS_TEXTURE_NAME = "_TerrainWindowVisualsTexture";
            _terrainComputeShader.SetTexture(
                kernel,
                TERRAIN_WINDOW_VISUALS_TEXTURE_NAME,
                texture);
        }

        public int GetKernel(string kernel) =>
            _kernels[kernel];

        public void Dispatch(string kernel, Vector3 dimensions) =>
            Dispatch(_kernels[kernel], dimensions);

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

        public void DispatchModifyTerrain()
        {
            Dispatch(_kernels[KERNEL_MODIFY_TERRAIN_TEXTURE], new Vector3(_terrainTexture.width, _terrainTexture.height, 1));
            Dispatch(_kernels[KERNEL_COPY_FROM_TERRAIN_TEXTURE], new Vector3(_terrainWindowTexture.width, _terrainWindowTexture.height, 1));
            //Dispatch(_kernels[KERNEL_COPY_TO_VISUALS_FROM_WINDOW], new Vector3(_terrainWindowTexture.width, _terrainWindowTexture.height, 1));
        }

        public void DispatchModifyWindow()
        {
            Dispatch(_kernels[KERNEL_COPY_FROM_TERRAIN_TEXTURE], new Vector3(_terrainWindowTexture.width, _terrainWindowTexture.height, 1));
            Dispatch(_kernels[KERNEL_MODIFY_TERRAIN_WINDOW], new Vector3(_terrainWindowTexture.width, _terrainWindowTexture.height, 1));
            Dispatch(_kernels[KERNEL_COPY_TO_TERRAIN_TEXTURE], new Vector3(_terrainWindowTexture.width, _terrainWindowTexture.height, 1));
            //Dispatch(_kernels[KERNEL_COPY_TO_VISUALS_FROM_WINDOW], new Vector3(_terrainWindowTexture.width, _terrainWindowTexture.height, 1));
        }
    }
}