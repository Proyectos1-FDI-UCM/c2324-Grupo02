
using System.Collections.Generic;
using TerrainSystem.Source;

namespace TerrainSystem.Requester
{
    public interface ITerrainModifierRequestable<in TModifier>
    {
        bool TryInitializeTerrainTo(uint type);

        bool TryModifyWith<UModifier>(UModifier modifier, IReadOnlyList<ITerrainModificationSource> modificationSources)
            where UModifier : TModifier;
    }
}