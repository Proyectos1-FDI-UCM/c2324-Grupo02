using System.Collections.Generic;
using TerrainSystem.Data;
using TerrainSystem.Data.Flyweight;
using TerrainSystem.Requester;
using UnityEngine;

namespace TerrainSystem.Queue
{
    internal class TerrainModificationEnqueuer : MonoBehaviour,
        ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TerrainModificationSource>>,
        ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource>>
    {
        [SerializeField]
        private TerrainModificationRequester _requester;

        public bool Dequeue(ITerrainModificationSourceFlyweight<TerrainModificationSource> modification) =>
            _requester.Dequeue(modification);

        public bool Dequeue(ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource> modification) =>
            _requester.Dequeue(modification);

        public bool Enqueue(ITerrainModificationSourceFlyweight<TerrainModificationSource> modification) =>
            _requester.Enqueue(modification);

        public bool Enqueue(ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource> modification) =>
            _requester.Enqueue(modification);
    }
}