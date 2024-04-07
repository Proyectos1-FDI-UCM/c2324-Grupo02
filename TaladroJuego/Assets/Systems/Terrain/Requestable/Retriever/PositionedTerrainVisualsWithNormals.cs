using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    public readonly struct PositionedTerrainVisualsWithNormals
    {
        public readonly PositionedTerrainVisuals albedoVisuals;
        public readonly RenderTexture normalsRenderTexture;

        internal PositionedTerrainVisualsWithNormals(PositionedTerrainVisuals albedoVisuals, RenderTexture normalsRenderTexture)
        {
            this.albedoVisuals = albedoVisuals;
            this.normalsRenderTexture = normalsRenderTexture;
        }

        public static PositionedTerrainVisualsWithNormals From(PositionedTerrainVisuals albedoVisuals, RenderTexture normalsRenderTexture)
        {
            return new PositionedTerrainVisualsWithNormals(albedoVisuals, normalsRenderTexture);
        }

        public static PositionedTerrainVisualsWithNormals From(RenderTexture albedoRenderTexture, Vector3 position, RenderTexture normalsRenderTexture)
        {
            return new PositionedTerrainVisualsWithNormals(PositionedTerrainVisuals.From(albedoRenderTexture, position), normalsRenderTexture);
        }
    }
}