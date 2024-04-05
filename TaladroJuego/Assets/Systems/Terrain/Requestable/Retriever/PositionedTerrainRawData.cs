using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal readonly struct PositionedTerrainRawData
    {
        public readonly RenderTexture renderTexture;
        public readonly Vector3 position;

        public PositionedTerrainRawData(RenderTexture renderTexture, Vector3 position)
        {
            this.renderTexture = renderTexture;
            this.position = position;
        }
    }
}