using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace TerrainSystem.Requester
{
    internal class PeriodicTerrainModificationRequester : MonoBehaviour, IInitializableTerrainModificationRequester
    {
        [SerializeField]
        private TerrainModificationRequester _terrainModificationRequester;

        [SerializeField]
        private bool _runInFixedUpdate = true;
        [SerializeField]
        private bool _startOnEnable = false;
        [SerializeField]
        private bool _startOnStart = true;

        private CancellationTokenSource _cancellationTokenSource;
        private Task _requestTask;
        private Coroutine _requestCoroutine;

        public bool Initialized => _terrainModificationRequester.Initialized;

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _requestTask = Task.CompletedTask;
        }

        private void OnEnable()
        {
            _ = _startOnEnable && TryStartRequestCoroutine();
        }

        private void Start()
        {
            _ = _startOnStart && TryStartRequestCoroutine();
        }

        public bool TryStartRequestTask()
        {
            if (_requestTask.Status == TaskStatus.Running
                || !Initialized)
                return false;

            _requestTask = _runInFixedUpdate
                ? Task.Run(() => FixedUpdateRequestAsync(_cancellationTokenSource.Token))
                : Task.Run(() => UpdateRequestAsync(_cancellationTokenSource.Token));
            return true;
        }

        public bool TryStopRequestTask()
        {
            if (_requestTask.Status != TaskStatus.Running)
                return false;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            return true;
        }

        public bool TryStartRequestCoroutine()
        {
            if (_requestCoroutine != null
                || !Initialized)
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

        public bool Initialize(RenderTexture terrainTexture, RenderTexture terrainWindowTexture, Camera camera) =>
            _terrainModificationRequester.Initialize(terrainTexture, terrainWindowTexture, camera);
        public bool Initialize(Vector2Int terrainTextureSize, Vector2Int terrainWindowTextureSize, Camera camera, out RenderTexture terrainRenderTexture, out RenderTexture terrainWindowRenderTexture) =>
            _terrainModificationRequester.Initialize(terrainTextureSize, terrainWindowTextureSize, camera, out terrainRenderTexture, out terrainWindowRenderTexture);
        public bool Finalize() => _terrainModificationRequester.Finalize();

        private async Task UpdateRequestAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _terrainModificationRequester.RequestModification();
                float time = Time.time;
                while (time == Time.time)
                    await Task.Yield();
            }
        }

        private async Task FixedUpdateRequestAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _terrainModificationRequester.RequestModification();
                float fixedTime = Time.fixedTime;
                while (fixedTime == Time.fixedTime)
                    await Task.Yield();
            }
        }
    }
}