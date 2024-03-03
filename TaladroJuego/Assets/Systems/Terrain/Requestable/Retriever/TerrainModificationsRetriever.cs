using System.Collections.Generic;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal readonly struct TerrainModificationsRetriever : ITerrainDataRetriever<float[]>
    {
        private readonly ComputeBuffer _modificationsBuffer;

        public TerrainModificationsRetriever(ComputeBuffer modificationsBuffer)
        {
            _modificationsBuffer = modificationsBuffer;
        }

        public void Retrieve(float[] destination)
        {
            _modificationsBuffer.GetData(destination);
        }
    }
}