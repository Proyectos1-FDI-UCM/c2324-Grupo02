using System;
using System.Collections.Generic;
using System.Linq;
using TerrainSystem.Requester;
using TerrainSystem.Source;
using UnityEngine;

namespace TerrainSystem.Modifier
{
    internal class TerrainModifer : MonoBehaviour, ITerrainModifier<TerrainModificationSource>
    {
        public bool TryModify<URequester>(URequester requester, IReadOnlyList<ITerrainModificationSource> modificationSources) where URequester : ITerrainModifierRequester<TerrainModificationSource>
        {
            return requester.TryModifyWith(Array.ConvertAll(modificationSources.ToArray(), ModificationSourceFrom));
        }

        private TerrainModificationSource ModificationSourceFrom(ITerrainModificationSource source)
        {
            return TerrainModificationSource.From(source, 0);
        }
    }
}