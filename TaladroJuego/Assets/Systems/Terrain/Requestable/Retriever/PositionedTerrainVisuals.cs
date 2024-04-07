using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    public readonly struct PositionedTerrainVisuals
    {
        public readonly RenderTexture renderTexture;
        public readonly Vector3 position;

        internal PositionedTerrainVisuals(RenderTexture renderTexture, Vector3 position)
        {
            this.renderTexture = renderTexture;
            this.position = position;
        }

        public static PositionedTerrainVisuals From(RenderTexture renderTexture, Vector3 position)
        {
            return new PositionedTerrainVisuals(renderTexture, position);
        }
    }
}