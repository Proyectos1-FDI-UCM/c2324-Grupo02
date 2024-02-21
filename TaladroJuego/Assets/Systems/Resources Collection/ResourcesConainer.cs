using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UpgradesSystem;
using UpgradesSystem.Client;
using UpgradesSystem.Flyweight;
using UpgradesSystem.Resource;

namespace ResourceCollectionSystem
{
    [CreateAssetMenu(fileName = "Resources Container", menuName = "Resources Container")]
    public class ResourcesConainer : ScriptableObject, IUpgradeClient
    {
        public UnityEvent<ResourceType, int> resourceModified;

        private Dictionary<ResourceType, int> _resourceQuantities;

        private void Awake()
        {
            _resourceQuantities = new Dictionary<ResourceType, int>();
        }
        public void AccountForResource(ResourceType resource)
        {

            if (_resourceQuantities.TryGetValue(resource, out int a))
            {
                _resourceQuantities[resource]++;
            }
            else
            {
                _resourceQuantities.Add(resource, 1);
            }
            resourceModified.Invoke(resource, _resourceQuantities[resource]);
        }

        public bool TryPurchase(IResourceUpgrade upgrade)
        {
            bool purchased = upgrade.TryPurchase(_resourceQuantities, out Dictionary<ResourceType, int> cost);

            if (purchased)
            {
                foreach(KeyValuePair<ResourceType, int> resourceAndCost in cost)
                {
                    _resourceQuantities[resourceAndCost.Key] -= resourceAndCost.Value;
                    resourceModified.Invoke(resourceAndCost.Key, _resourceQuantities[resourceAndCost.Key]);
                }
                
            }

            return purchased;
        }


    }
}

