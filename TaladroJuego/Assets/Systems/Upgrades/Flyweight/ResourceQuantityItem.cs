using System;
using System.Collections.Generic;
using UnityEngine;

namespace UpgradesSystem.Flyweight
{
    [Serializable]
    public struct ResourceQuantityItem
    {
        [field: SerializeField]
        public ResourceType Resource { get; private set; }

        [field: SerializeField]
        public int Quantity { get; private set; }

        public ResourceQuantityItem(ResourceType resource, int quantity)
        {
            Resource = resource;
            Quantity = quantity;
        }

        public static Dictionary<ResourceType, int> DictionaryFrom(IEnumerable<ResourceQuantityItem> quotaItems)
        {
            Dictionary<ResourceType, int> quota = new Dictionary<ResourceType, int>();
            foreach (var quotaItem in quotaItems)
                if (!quota.TryAdd(quotaItem.Resource, quotaItem.Quantity))
                    quota[quotaItem.Resource] += quotaItem.Quantity;

            return quota;
        }
    }

}