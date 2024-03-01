using System;
using System.Collections.Generic;
using System.Linq;
using TerrainSystem.Requester;
using TerrainSystem.Source;
using UnityEngine;

namespace TerrainSystem.Modifier
{
    internal class TexturedTerrainModifer : MonoBehaviour, ITerrainModifier<TexturedTerrainModificationSource>
    {
        public bool TryModify<URequester>(URequester requester, IReadOnlyList<ITerrainModificationSource> modificationSources) where URequester : ITerrainModifierRequester<TexturedTerrainModificationSource>
        {
            return requester.TryModifyWith(Array.ConvertAll(modificationSources.ToArray(), ModificationSourceFrom));
        }

        private TexturedTerrainModificationSource ModificationSourceFrom(ITerrainModificationSource source)
        {
            return TexturedTerrainModificationSource.From(source, 0, Vector2Int.zero);
        }
    }
}