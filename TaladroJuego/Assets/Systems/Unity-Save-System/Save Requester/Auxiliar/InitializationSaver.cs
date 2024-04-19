using SaveSystem.SaveRequester.Handler;
using SaveSystem.SaveRequester.SavePath;
using UnityEngine;

namespace SaveSystem.SaveRequester.Auxiliar
{
    internal class InitializationSaver : MonoBehaviour
    {
        [SerializeField]
        private SaveRequester _requester;
        private ISaveRequester _saveRequester;

        [SerializeField]
        private Batch.SaveHandler _handler;
        private ISaveHandler _saveHandler;

        [SerializeField]
        private SavePathFlyweight _savePathFlyweight;
        private ISavePathService _savePathService;

        [SerializeField]
        private bool _loadOnStart = true;

        [SerializeField]
        private bool _saveOnQuit = true;

        [SerializeField]
        private bool _saveOnDisable = false;

        private void Awake()
        {
            _saveRequester = GetComponentInChildren<ISaveRequester>() ?? _requester;
            _saveHandler = GetComponentInChildren<ISaveHandler>() ?? _handler;
            _savePathService = GetComponentInChildren<ISavePathService>() ?? _savePathFlyweight;
        }

        private void Start()
        {
            if (_loadOnStart)
                Load();
        }

        private void OnDisable()
        {
            if (_saveOnDisable)
                Save();
        }

        private void OnApplicationQuit()
        {
            if (_saveOnQuit)
                Save();
        }

        [ContextMenu(nameof(Save))]
        public void Save() =>
            _saveRequester.Save(_saveHandler, _savePathService.GetPath());

        [ContextMenu(nameof(Load))]
        public void Load() =>
            _saveRequester.Load(_saveHandler, _savePathService.GetPath());

        [ContextMenu(nameof(Delete))]
        public void Delete() =>
            _saveRequester.Delete(_saveHandler, _savePathService.GetPath());
    }
}