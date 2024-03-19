using TerrainSystem.Requestable.Retriever.Observable;
using UnityEngine;

namespace TerrainSystem.Requestable.Retriever
{
    internal readonly struct TerrainModificationsRetriever : ITerrainDataRetriever<TerrainModification[]>
    {
        private readonly ComputeBuffer _modificationsBuffer;

        public TerrainModificationsRetriever(ComputeBuffer modificationsBuffer)
        {
            _modificationsBuffer = modificationsBuffer;
        }

        public void Retrieve(in TerrainModification[] destination)
        {
            _modificationsBuffer.GetData(destination);
        }

        public TerrainModification[] Retrieve()
        {
            TerrainModification[] destination = new TerrainModification[_modificationsBuffer.count];
            Retrieve(in destination);
            return destination;
        }
    }
}