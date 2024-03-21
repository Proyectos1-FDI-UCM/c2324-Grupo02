using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem.SaveService.Factory
{
    [CreateAssetMenu(fileName = "JsonSaveServiceFactory", menuName = "Save System/Save Service/Json Factory")]
    internal class JsonSaveServiceFactory : SaveServiceFactory
    {
        private bool _hasJsonSerializerSettings = false;
        private JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();

        public override ISaveService Create() => CreateService();
        public JsonSaveService CreateService() =>
            _hasJsonSerializerSettings
            ? new JsonSaveService()
            : new JsonSaveService(_jsonSerializerSettings);

        public JsonSaveServiceFactory WithJsonSerializerSettings(JsonSerializerSettings settings)
        {
            _jsonSerializerSettings = settings;
            _hasJsonSerializerSettings = true;
            return this;
        }

        public JsonSaveServiceFactory WithoutJsonSerializerSettings()
        {
            _hasJsonSerializerSettings = false;
            return this;
        }

        private void OnEnable()
        {
            _hasJsonSerializerSettings = false;
            _jsonSerializerSettings = new JsonSerializerSettings();
        }
    }
}