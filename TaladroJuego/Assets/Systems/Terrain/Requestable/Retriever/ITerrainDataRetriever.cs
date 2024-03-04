namespace TerrainSystem.Requestable.Retriever
{
    public interface ITerrainDataRetriever<TRetrieved>
    {
        void Retrieve(in TRetrieved destination);
        TRetrieved Retrieve();
    }
}