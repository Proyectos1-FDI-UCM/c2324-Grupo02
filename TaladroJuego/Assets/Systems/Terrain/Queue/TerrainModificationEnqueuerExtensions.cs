using System.Collections.Generic;
using TerrainSystem.Data;
using TerrainSystem.Data.Flyweight;
using UnityEngine;

namespace TerrainSystem.Queue
{
    internal static class TerrainModificationEnqueuerExtensions
    {
        public static IEnumerable<IQueueableTerrainModification<TerrainModificationEnqueuer>> GetEnqueueablesInChildren(GameObject gameObject)
        {
            foreach (var queueable in gameObject.GetComponentsInChildren<IQueueableTerrainModification<ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource>>>>())
                yield return queueable;
            foreach (var queueable in gameObject.GetComponentsInChildren<IQueueableTerrainModification<ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TerrainModificationSource>>>>())
                yield return queueable;
        }
    }
}