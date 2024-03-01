using System;
using UnityEngine;

namespace TerrainSystem.Test
{
    internal class TerrainDestructionTest : MonoBehaviour
    {
        public const string MODIFY_TERRAIN_KERNEL_NAME = "ModifyTerrain";
        public const string INITIALIZE_TERRAIN_KERNEL_NAME = "InitializeTerrain";
        public const string APPLY_VISUALS_KERNEL_NAME = "ApplyVisuals";

        [SerializeField]
        private ComputeShader _terrainComputeShader;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private TerrainModificationSource.Data[] _sourcesData;
        private ComputeBuffer _sourcesBuffer;

        [SerializeField]
        private Texture2DArray _terrainTextures;

        [SerializeField]
        private RenderTexture _terrainRenderTexture;

        private RenderTexture _temporaryTerrainTexture0;
        private RenderTexture _temporaryTerrainTexture1;

        private readonly struct TerrainModificationSource
        {
            public readonly Vector3 positionWS;
            public readonly float radius;
            public readonly float strength;
            public readonly float falloff;
            public readonly uint type;

            public TerrainModificationSource(Vector3 positionWS, float radius, float strength, float falloff, uint type)
            {
                this.positionWS = positionWS;
                this.radius = radius;
                this.strength = strength;
                this.falloff = falloff;
                this.type = type;
            }

            public static int SizeOf() => sizeof(float) * 3 + sizeof(float) * 3 + sizeof(uint);
            public static TerrainModificationSource FromData(Data data) =>
                new TerrainModificationSource(
                    data.Source.position,
                    data.Radius,
                    data.Strength,
                    data.Falloff,
                    data.Type);

            [Serializable]
            public struct Data
            {
                [field: SerializeField]
                public Transform Source { get; private set; }

                [field: SerializeField]
                [field: Min(0)]
                public float Radius { get; private set; }

                [field: SerializeField]
                [field: Min(0)]
                public float Strength { get; private set; }

                [field: SerializeField]
                [field: Min(0)]
                public float Falloff { get; private set; }

                [field: SerializeField]
                public uint Type { get; private set; }
            }
        }

        public bool TryInitialize()
        {
            _temporaryTerrainTexture0 = new RenderTexture(_terrainRenderTexture.width, _terrainRenderTexture.height, 0, RenderTextureFormat.RG16);
            _temporaryTerrainTexture0.enableRandomWrite = true;

            _temporaryTerrainTexture1 = new RenderTexture(_terrainRenderTexture.width, _terrainRenderTexture.height, 0, RenderTextureFormat.ARGB32);
            _temporaryTerrainTexture1.enableRandomWrite = true;

            _sourcesBuffer = new ComputeBuffer(_sourcesData.Length, TerrainModificationSource.SizeOf(), ComputeBufferType.Structured);

            return true;
        }

        public bool TryConfigure(string kernelName)
        {
            if (!_terrainComputeShader.HasKernel(kernelName))
                return false;
            int kernel = _terrainComputeShader.FindKernel(kernelName);

            const string SOURCE_BUFFER_NAME = "_Sources";
            _sourcesBuffer.SetData(Array.ConvertAll(_sourcesData, TerrainModificationSource.FromData));
            _terrainComputeShader.SetBuffer(kernel, SOURCE_BUFFER_NAME, _sourcesBuffer);

            const string SOURCE_COUNT_NAME = "_SourceCount";
            _terrainComputeShader.SetInt(SOURCE_COUNT_NAME, _sourcesData.Length);

            const string TERRAIN_RESULT_NAME = "_TerrainResult";
            _terrainComputeShader.SetTexture(kernel, TERRAIN_RESULT_NAME, _temporaryTerrainTexture0);

            const string TERRAIN_TYPES_COUNT = "_TerrainTypesCount";
            _terrainComputeShader.SetInt(TERRAIN_TYPES_COUNT, _terrainTextures.depth);

            const string CLIP_TO_WORLD_MATRIX_NAME = "_ClipToWorldMatrix";
            Matrix4x4 projectionMatrix = GL.GetGPUProjectionMatrix(_camera.projectionMatrix, false);
            _terrainComputeShader.SetMatrix(
                CLIP_TO_WORLD_MATRIX_NAME, 
                (_camera.worldToCameraMatrix * projectionMatrix).inverse);

            const string TEXTURE_OFFSET_AND_SIZE_NAME = "_TextureOffsetAndSize";
            _terrainComputeShader.SetInts(
                TEXTURE_OFFSET_AND_SIZE_NAME,
                    0,
                    0,
                    _temporaryTerrainTexture0.width,
                    _temporaryTerrainTexture0.height);

            const string TERRAIN_TEXTURES_NAME = "_TerrainTypesTextures";
            _terrainComputeShader.SetTexture(kernel, TERRAIN_TEXTURES_NAME, _terrainTextures);

            const string TERRAIN_TEXTURES_OFFSET_AND_SIZE_NAME = "_TerrainTexturesOffsetAndSize";
            _terrainComputeShader.SetInts(
                TERRAIN_TEXTURES_OFFSET_AND_SIZE_NAME,
                0,
                0,
                _terrainTextures.width,
                _terrainTextures.height);

            const string TERRAIN_VISUALS_NAME = "_TerrainVisuals";
            _terrainComputeShader.SetTexture(kernel, TERRAIN_VISUALS_NAME, _temporaryTerrainTexture1);

            return true;
        }

        public bool TryDispatch(string kernelName)
        {
            if (!_terrainComputeShader.HasKernel(kernelName))
                return false;
            int kernel = _terrainComputeShader.FindKernel(kernelName);

            _terrainComputeShader.GetKernelThreadGroupSizes(
                kernel,
                out uint groupSizeX,
                out uint groupSizeY,
                out uint groupSizeZ);

            Vector3Int threadGroups = Vector3Int.CeilToInt(new Vector3(
                _temporaryTerrainTexture0.width / (float)groupSizeX,
                _temporaryTerrainTexture0.height / (float)groupSizeY,
                1 / (float)groupSizeZ));

            _terrainComputeShader.Dispatch(
                kernel,
                threadGroups.x,
                threadGroups.y,
                threadGroups.z);
            return true;
        }

        public bool TryRetrieve()
        {
            Graphics.Blit(_temporaryTerrainTexture1, _terrainRenderTexture);
            //Graphics.Blit(_temporaryTerrainTexture0, _terrainRenderTexture);
            return true;
        }

        public bool TryFinalize()
        {
            _temporaryTerrainTexture0.Release();
            _temporaryTerrainTexture1.Release();
            return true;
        }
    }
}