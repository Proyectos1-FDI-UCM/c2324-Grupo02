using TerrainSystem.Data;
using TerrainSystem.Data.Flyweight;
using TerrainSystem.Requester;
using TerrainSystem.Test;
using UnityEngine;

namespace TerrainSystem.Queue
{
    internal class TerrainModificationEnqueuer : MonoBehaviour,
        ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TerrainModificationSource>>,
        ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource>>
    {
        [SerializeField]
        private TerrainModificationRequester _requester;

        [SerializeField]
        private bool _enqueueOnStart;

        [SerializeField]
        private bool _dequeueOnDestroy;

        private void Start()
        {
            if (!_enqueueOnStart)
                return;

            foreach (var queueable in GetComponentsInChildren<IQueueableTerrainModification<ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TerrainModificationSource>>>>())
                queueable.AcceptEnqeue(this);

            foreach (var queueable in GetComponentsInChildren<IQueueableTerrainModification<ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource>>>>())
                queueable.AcceptEnqeue(this);
        }

        private void OnDestroy()
        {
            if (!_dequeueOnDestroy)
                return;

            foreach (var queueable in GetComponentsInChildren<IQueueableTerrainModification<ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TerrainModificationSource>>>>())
                queueable.AcceptDequeue(this);

            foreach (var queueable in GetComponentsInChildren<IQueueableTerrainModification<ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource>>>>())
                queueable.AcceptDequeue(this);
        }

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