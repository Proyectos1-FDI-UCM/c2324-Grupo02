using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UpgradesSystem.Client;
using UpgradesSystem.Flyweight;
using UpgradesSystem.Resource;

namespace ResourceCollectionSystem
{
    [CreateAssetMenu(fileName = "Resources Container", menuName = "Resources Container")]
    public class ResourcesContainer : ScriptableObject, IUpgradeClient
    {
        [field: SerializeField]
        public UnityEvent<ResourceType, int> ResourceModified { get; private set; }

        private Dictionary<ResourceType, int> _resourceQuantities;

        private void OnEnable()
        {
            _resourceQuantities = new Dictionary<ResourceType, int>();
        }

        public void AccountForResource(ResourceType resource, int quantity)
        {
            if (!_resourceQuantities.TryAdd(resource, quantity))
                _resourceQuantities[resource] += quantity;

            ResourceModified.Invoke(resource, _resourceQuantities[resource]);
        }

        public bool TryPurchase(IResourceUpgrade upgrade)
        {
            bool purchased = upgrade.TryPurchase(_resourceQuantities, out Dictionary<ResourceType, int> cost);

            if (purchased)
            {
                foreach (KeyValuePair<ResourceType, int> resourceAndCost in cost)
                {
                    _resourceQuantities[resourceAndCost.Key] -= resourceAndCost.Value;
                    ResourceModified.Invoke(resourceAndCost.Key, _resourceQuantities[resourceAndCost.Key]);
                }
            }

            return purchased;
        }
    }
}

