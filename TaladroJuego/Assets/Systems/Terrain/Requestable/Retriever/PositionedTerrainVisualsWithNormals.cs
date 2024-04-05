using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal readonly struct PositionedTerrainVisualsWithNormals
    {
        public readonly PositionedTerrainVisuals albedoVisuals;
        public readonly RenderTexture normalsRenderTexture;

        public PositionedTerrainVisualsWithNormals(PositionedTerrainVisuals albedoVisuals, RenderTexture normalsRenderTexture)
        {
            this.albedoVisuals = albedoVisuals;
            this.normalsRenderTexture = normalsRenderTexture;
        }
    }
}