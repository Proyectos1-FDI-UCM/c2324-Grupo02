using System.Threading.Tasks;

namespace TerrainSystem.Requestable.Retriever
{
    public interface ITerrainDataRetriever<TRetrieved>
    {
        Task<bool> TryRetrieve(in TRetrieved destination);
        Task<TRetrieved> Retrieve();
    }
}