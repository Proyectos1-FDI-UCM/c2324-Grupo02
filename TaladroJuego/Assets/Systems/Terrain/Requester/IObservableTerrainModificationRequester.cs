using System;

namespace TerrainSystem.Requester
{
    public interface IObservableTerrainModificationRequester
    {
        event EventHandler ModificationRequested;
    }
}