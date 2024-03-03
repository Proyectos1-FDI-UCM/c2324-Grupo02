using System.Linq;
using TerrainSystem.Accessor;
using TerrainSystem.Data;
using UnityEngine;

namespace TerrainSystem.Requestable
{
    internal readonly struct TerrainModifierRequestable<TSource> :
        ITerrainModifierRequestable<TSource[]>,
        IConfigurableTerrainModifierRequestable,
        ITerrainInitializerRequestable
        where TSource : ITerrainModificationSource
    {
        private readonly TerrainTextureModifierRequestable<TSource> _terrainTextureModifierRequestable;
        private readonly TerrainWindowModifierRequestable<TSource> _terrainWindowModifierRequestable;

        public TerrainModifierRequestable(TerrainModificationShaderAccessor accessor, ITerrainModifierConfigurer terrainModifierConfigurer, RenderTexture terrainWindowTexture, ComputeBuffer sourcesBuffer)
        {
            _terrainTextureModifierRequestable = new TerrainTextureModifierRequestable<TSource>(accessor, terrainModifierConfigurer, terrainWindowTexture, sourcesBuffer);
            _terrainWindowModifierRequestable = new TerrainWindowModifierRequestable<TSource>(accessor, terrainModifierConfigurer, terrainWindowTexture, sourcesBuffer);
        }

        public void InitializeTerrainWith(uint state, Camera camera, RenderTexture terrainTexture)
        {
            _terrainTextureModifierRequestable.InitializeTerrainWith(state, camera, terrainTexture);
            _terrainWindowModifierRequestable.InitializeTerrainWith(state, camera, terrainTexture);
        }

        public void ConfigureWith(int modificationSourcesCount, int terrainTypesCount, Texture terrainTexture, Camera camera)
        {
            _terrainTextureModifierRequestable.ConfigureWith(modificationSourcesCount, terrainTypesCount, terrainTexture, camera);
            _terrainWindowModifierRequestable.ConfigureWith(modificationSourcesCount, terrainTypesCount, terrainTexture, camera);
        }

        public void ModifyTerrainWith(TSource[] source, Camera camera, RenderTexture terrainTexture)
        {
            if (source.All(s => InViewport(s.GetPosition(), camera)))
                _terrainWindowModifierRequestable.ModifyTerrainWith(source, camera, terrainTexture);
            else
                _terrainTextureModifierRequestable.ModifyTerrainWith(source, camera, terrainTexture);
        }

        private static bool InViewport(Vector3 positionWS, Camera camera)
        {
            Vector3 viewportPoint = camera.WorldToViewportPoint(positionWS);
            return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
        }
    }

    internal readonly struct TerrainModifierRequestable :
        ITerrainModifierRequestable<TerrainModificationSource[]>,
        ITerrainModifierRequestable<TexturedTerrainModificationSource[]>,
        ITerrainInitializerRequestable
    {
        public readonly TerrainModifierRequestable<TerrainModificationSource> terrainModifierRequestable;
        public readonly TerrainModifierRequestable<TexturedTerrainModificationSource> texturedTerrainModifierRequestable;

        public TerrainModifierRequestable(ComputeShader terrainComputeShader, RenderTexture terrainWindowTexture, ComputeBuffer typedSourcesBuffer, ComputeBuffer texturedSourcesBuffer, ComputeBuffer terrainModificationsBuffer, Texture2DArray alphaTextures)
        {
            TerrainModificationShaderAccessor accessor = new TerrainModificationShaderAccessor(terrainComputeShader);
            ITerrainModifierConfigurer terrainModifierConfigurer = new TerrainModifierConfigurer(accessor, terrainWindowTexture, typedSourcesBuffer, terrainModificationsBuffer);

            ITerrainModifierConfigurer texturedTerrainModifierConfigurer = new TexturedTerrainModifierConfigurer(terrainModifierConfigurer, accessor, texturedSourcesBuffer, alphaTextures);

            terrainModifierRequestable = new TerrainModifierRequestable<TerrainModificationSource>(accessor, terrainModifierConfigurer, terrainWindowTexture, typedSourcesBuffer);
            texturedTerrainModifierRequestable = new TerrainModifierRequestable<TexturedTerrainModificationSource>(accessor, texturedTerrainModifierConfigurer, terrainWindowTexture, texturedSourcesBuffer);
        }

        public void InitializeTerrainWith(uint state, Camera camera, RenderTexture terrainTexture)
        {
            terrainModifierRequestable.InitializeTerrainWith(state, camera, terrainTexture);
            texturedTerrainModifierRequestable.InitializeTerrainWith(state, camera, terrainTexture);
        }

        public void ModifyTerrainWith(TerrainModificationSource[] source, Camera camera, RenderTexture terrainTexture)
        {
            terrainModifierRequestable.ModifyTerrainWith(source, camera, terrainTexture);
        }

        public void ModifyTerrainWith(TexturedTerrainModificationSource[] source, Camera camera, RenderTexture terrainTexture)
        {
            texturedTerrainModifierRequestable.ModifyTerrainWith(source, camera, terrainTexture);
        }
    }
}