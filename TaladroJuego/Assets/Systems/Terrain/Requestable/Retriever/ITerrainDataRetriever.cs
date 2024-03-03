namespace TerrainSystem.Requestable.Retriever
{
    public interface ITerrainDataRetriever<TRetrieved>
    {
        void Retrieve(TRetrieved destination);
    }
}