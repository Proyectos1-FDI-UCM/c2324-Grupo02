using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    public readonly struct PositionedTerrainRawData
    {
        public readonly RenderTexture renderTexture;
        public readonly Vector3 position;

        internal PositionedTerrainRawData(RenderTexture renderTexture, Vector3 position)
        {
            this.renderTexture = renderTexture;
            this.position = position;
        }

        public static PositionedTerrainRawData From(RenderTexture renderTexture, Vector3 position)
        {
            return new PositionedTerrainRawData(renderTexture, position);
        }
    }
}