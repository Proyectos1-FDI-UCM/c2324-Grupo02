using TerrainSystem.Data;
using TerrainSystem.Data.Flyweight;
using UnityEngine;

namespace TerrainSystem.Queue
{
    internal class QueueableTexturedTerrainModification : MonoBehaviour,
        IQueueableTerrainModification<ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource>>>
    {
        [SerializeField]
        private TexturedTerrainModificationSourceFlyweight _flyweight;

        [SerializeField]
        private Transform _source;

        [SerializeField]
        private Vector3 _positionOffset;

        [SerializeField]
        private Vector3 _rotationOffset;

        private QueueableTerrainModification<TexturedTerrainModificationSource> _queueableTerrainModification;

        private void Awake()
        {
            _queueableTerrainModification = new QueueableTerrainModification<TexturedTerrainModificationSource>(_flyweight, _source, _positionOffset, _rotationOffset);
        }

        public bool AcceptDequeue<TEnqueuer>(TEnqueuer enqueuer)
            where TEnqueuer : ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource>> =>
            _queueableTerrainModification.AcceptDequeue(enqueuer);

        public bool AcceptEnqeue<TEnqueuer>(TEnqueuer enqueuer)
            where TEnqueuer : ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TexturedTerrainModificationSource>> =>
            _queueableTerrainModification.AcceptEnqeue(enqueuer);
    }
}