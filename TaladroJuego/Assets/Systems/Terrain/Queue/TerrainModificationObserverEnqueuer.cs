using System;
using TerrainSystem.Requester;
using UnityEngine;

namespace TerrainSystem.Queue
{
    internal class TerrainModificationObserverEnqueuer : MonoBehaviour
    {
        [SerializeField]
        private TerrainModificationEnqueuer _enqueuer;

        [SerializeField]
        private TerrainModificationRequester _requester;

        [SerializeField]
        [Min(0)]
        private int _enqueueAfterEventCount;
        private int _eventCount;

        [SerializeField]
        private bool _dequeueInstead;

        private void Start()
        {
            _requester.ModificationRequested += OnModificationRequested;
        }

        private void OnDestroy()
        {
            _requester.ModificationRequested -= OnModificationRequested;
        }

        private void OnModificationRequested(object sender, EventArgs e)
        {
            _ = _eventCount++ == _enqueueAfterEventCount
                && (_dequeueInstead
                    ? DequeueAll()
                    : EnqueueAll());
        }
        
        private bool EnqueueAll()
        {
            bool success = true;
            foreach (var queueable in TerrainModificationEnqueuerExtensions.GetEnqueueablesInChildren(gameObject))
                success &= queueable.AcceptEnqeue(_enqueuer);
            return success;
        }

        private bool DequeueAll()
        {
            bool success = true;
            foreach (var queueable in TerrainModificationEnqueuerExtensions.GetEnqueueablesInChildren(gameObject))
                success &= queueable.AcceptDequeue(_enqueuer);
            return success;
        }
    }
}