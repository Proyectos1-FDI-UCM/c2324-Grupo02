using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal readonly struct PositionedTerrainVisuals
    {
        public readonly RenderTexture renderTexture;
        public readonly Vector3 position;

        public PositionedTerrainVisuals(RenderTexture renderTexture, Vector3 position)
        {
            this.renderTexture = renderTexture;
            this.position = position;
        }
    }
}