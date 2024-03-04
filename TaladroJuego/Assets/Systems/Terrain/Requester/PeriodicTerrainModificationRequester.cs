using System.Collections;
using UnityEngine;

namespace TerrainSystem.Requester
{
    internal class PeriodicTerrainModificationRequester : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Vector2Int _terrainTextureSize;

        [SerializeField]
        private Vector2Int _terrainWindowTextureSize;

        [SerializeField]
        private TerrainModificationRequester _terrainModificationRequester;

        [SerializeField]
        private bool _runInFixedUpdate = true;
        [SerializeField]
        private bool _startOnEnable = true;

        private Coroutine _requestCoroutine;

        private void Awake()
        {
            _terrainModificationRequester.Initialize(_terrainTextureSize, _terrainWindowTextureSize, _camera);
        }

        private void OnEnable()
        {
            _ = _startOnEnable && TryStartRequestCoroutine();
        }

        private void OnDestroy()
        {
            _terrainModificationRequester.Finalize();
        }

        public bool TryStartRequestCoroutine()
        {
            if (_requestCoroutine != null)
                return false;

            _requestCoroutine = _runInFixedUpdate
                ? StartCoroutine(FixedUpdateRequestCoroutine())
                : StartCoroutine(UpdateRequestCouroutine());
            return true;
        }

        public bool TryStopRequestCoroutine()
        {
            if (_requestCoroutine == null)
                return false;

            StopCoroutine(_requestCoroutine);
            _requestCoroutine = null;
            return true;
        }

        private IEnumerator UpdateRequestCouroutine()
        {
            while (enabled)
            {
                _terrainModificationRequester.RequestModification();
                yield return null;
            }
        }

        private IEnumerator FixedUpdateRequestCoroutine()
        {
            WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
            while (enabled)
            {
                _terrainModificationRequester.RequestModification();
                yield return waitForFixedUpdate;
            }

            _requestCoroutine = null;
        }
    }
}