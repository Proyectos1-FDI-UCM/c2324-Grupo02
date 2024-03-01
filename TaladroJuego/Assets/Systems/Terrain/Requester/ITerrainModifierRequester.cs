
namespace TerrainSystem.Requester
{
    public interface ITerrainModifierRequester<in TModificationSource>
    {
        bool TryModifyWith(TModificationSource[] sources);
    }
}