using ResourceCollectionSystem;
using SaveSystem.Saveable;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Flyweight;

namespace SaveablesSystem.ResourcesCollection
{
    internal class SaveableResourcesContainer : MonoBehaviour, ISaveable<IReadOnlyDictionary<ResourceType, int>>
    {
        [SerializeField]
        private ResourcesContainer _resourcesContainer;

        public IReadOnlyDictionary<ResourceType, int> GetData() =>
            _resourcesContainer.ResourceQuantities;

        public bool TrySetData(IReadOnlyDictionary<ResourceType, int> saveData)
        {
            foreach (KeyValuePair<ResourceType, int> resourceAndQuantity in saveData)
                _resourcesContainer.AccountForResource(resourceAndQuantity.Key, resourceAndQuantity.Value);

            return true;
        }
    }
}