using System.Threading.Tasks;

namespace TerrainSystem.Requestable.Retriever
{
    public interface ITerrainDataRetriever<TRetrieved>
    {
        Task Retrieve(in TRetrieved destination);
        Task<TRetrieved> Retrieve();
    }
}