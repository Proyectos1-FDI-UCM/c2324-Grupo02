using TerrainSystem.Data;
using TerrainSystem.Data.Flyweight;
using UnityEngine;

namespace TerrainSystem.Queue
{
    internal readonly struct QueueableTerrainModification<TSource> :
        IQueueableTerrainModification<ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TSource>>>,
        ITerrainModificationSourceFlyweight<TSource>,
        ITerrainModificationSource
    {
        private readonly ITerrainModificationSourceFlyweight<TSource, ITerrainModificationSource> _flyweight;
        private readonly Transform _source;
        private readonly Vector3 _positionOffset;
        private readonly Vector3 _rotationOffset;

        public QueueableTerrainModification(ITerrainModificationSourceFlyweight<TSource, ITerrainModificationSource> flyweight, Transform source, Vector3 positionOffset, Vector3 rotationOffset)
        {
            _flyweight = flyweight;
            _source = source;
            _positionOffset = positionOffset;
            _rotationOffset = rotationOffset;
        }

        public bool AcceptEnqeue<UEnqueuer>(UEnqueuer enqueuer)
            where UEnqueuer : ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TSource>> =>
            enqueuer.Enqueue(this);

        public bool AcceptDequeue<UEnqueuer>(UEnqueuer enqueuer)
            where UEnqueuer : ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TSource>> =>
            enqueuer.Dequeue(this);

        public TSource Create() =>
            _flyweight.CreateFrom(this);

        public Vector3 GetPosition() =>
            _source.TransformPoint(_positionOffset);

        public Quaternion GetRotation() =>
            _source.rotation * Quaternion.Euler(_rotationOffset);

        public Vector3 GetScale() =>
            _source.lossyScale;
    }

    public class QueueableTerrainModification : MonoBehaviour,
        IQueueableTerrainModification<ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TerrainModificationSource>>>
    {
        [SerializeField]
        private TerrainModificationSourceFlyweight _flyweight;

        [SerializeField]
        private Transform _source;

        [SerializeField]
        private Vector3 _positionOffset;

        [SerializeField]
        private Vector3 _rotationOffset;

        private QueueableTerrainModification<TerrainModificationSource> _queueableTerrainModification;

        private void Awake()
        {
            _queueableTerrainModification = new QueueableTerrainModification<TerrainModificationSource>(_flyweight, _source, _positionOffset, _rotationOffset);
        }

        public bool AcceptDequeue<UEnqueuer>(UEnqueuer enqueuer)
            where UEnqueuer : ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TerrainModificationSource>> =>
            _queueableTerrainModification.AcceptDequeue(enqueuer);

        public bool AcceptEnqeue<UEnqueuer>(UEnqueuer enqueuer)
            where UEnqueuer : ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TerrainModificationSource>> =>
            _queueableTerrainModification.AcceptEnqeue(enqueuer);
    }
}