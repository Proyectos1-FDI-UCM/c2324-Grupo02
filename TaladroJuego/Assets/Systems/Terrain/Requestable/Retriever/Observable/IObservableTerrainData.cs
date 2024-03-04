using System;

namespace TerrainSystem.Requestable.Retriever.Observable
{
    public interface IObservableTerrainData<T>
    {
        event EventHandler<T> DataRetrieved;
    }
}