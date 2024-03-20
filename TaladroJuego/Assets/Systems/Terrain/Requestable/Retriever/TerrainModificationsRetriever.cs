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

        public void Retrieve(in float[] destination)
        {
            _modificationsBuffer.GetData(destination);
        }

        public float[] Retrieve()
        {
            float[] destination = new float[_modificationsBuffer.count];
            Retrieve(in destination);
            return destination;
        }
    }
}