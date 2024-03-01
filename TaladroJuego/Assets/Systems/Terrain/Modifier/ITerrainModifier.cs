using System.Collections.Generic;
using TerrainSystem.Requester;
using TerrainSystem.Source;

namespace TerrainSystem.Modifier
{
    public interface ITerrainModifier<out TModificationSource>
    {
        bool TryModify<URequester>(URequester requester, IReadOnlyList<ITerrainModificationSource> modificationSources)
            where URequester : ITerrainModifierRequester<TModificationSource>;
    }
}