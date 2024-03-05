using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TerrainSystem.Queue
{
    internal class InitializationTerrainModificationEnqueuer : MonoBehaviour
    {
        [SerializeField]
        private TerrainModificationEnqueuer _enqueuer;
        private IEnumerable<IQueueableTerrainModification<TerrainModificationEnqueuer>> _queueables;

        [SerializeField]
        private bool _enqueueOnStart;

        [SerializeField]
        private bool _dequeueOnDestroy;

        private void Start()
        {
            if (!_enqueueOnStart)
                return;

            _queueables ??= TerrainModificationEnqueuerExtensions.GetEnqueueablesInChildren(gameObject).ToArray();
            foreach (var queueable in _queueables)
                queueable.AcceptEnqeue(_enqueuer);
        }

        private void OnDestroy()
        {
            if (!_dequeueOnDestroy)
                return;

            _queueables ??= TerrainModificationEnqueuerExtensions.GetEnqueueablesInChildren(gameObject).ToArray();
            foreach (var queueable in _queueables)
                queueable.AcceptDequeue(_enqueuer);
        }
    }
}