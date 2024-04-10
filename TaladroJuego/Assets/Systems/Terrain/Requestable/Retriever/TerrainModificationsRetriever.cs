using System;
using System.Threading.Tasks;
using TerrainSystem.Requestable.Retriever.Observable;
using UnityEngine;
using UnityEngine.Rendering;

namespace TerrainSystem.Requestable.Retriever
{
    internal readonly struct TerrainModificationsRetriever : ITerrainDataRetriever<TerrainModification[]>
    {
        private readonly ComputeBuffer _modificationsBuffer;
        private readonly Action<AsyncGPUReadbackRequest> _retrievalCallback;

        private readonly struct AsyncGPUReadbackRequestHandler
        {
            private readonly TerrainModification[] _destination;

            public AsyncGPUReadbackRequestHandler(TerrainModification[] destination)
            {
                _destination = destination;
            }

            public Task Handle(AsyncGPUReadbackRequest request) =>
                HandleAsync(request);

            private async Task HandleAsync(AsyncGPUReadbackRequest request)
            {
                while (!request.done)
                    await Task.Yield();

                if (!request.hasError)
                    request.GetData<TerrainModification>(0).CopyTo(_destination);
            }
        }

        public TerrainModificationsRetriever(ComputeBuffer modificationsBuffer, Action<AsyncGPUReadbackRequest> retrievalCallback)
        {
            _modificationsBuffer = modificationsBuffer;
            _retrievalCallback = retrievalCallback;
        }

        public Task Retrieve(in TerrainModification[] destination) =>
            new AsyncGPUReadbackRequestHandler(destination)
                .Handle(AsyncGPUReadback.Request(_modificationsBuffer, _retrievalCallback));

        public async Task<TerrainModification[]> Retrieve()
        {
            TerrainModification[] destination = new TerrainModification[_modificationsBuffer.count];
            await Retrieve(in destination);
            return destination;
        }
    }
}