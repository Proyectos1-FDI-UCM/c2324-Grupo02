using System;
using System.Collections.Generic;
using UnityEngine;

namespace UpgradesSystem.Flyweight
{
    [Serializable]
    public struct ResourceQuotaItem
    {
        [field: SerializeField]
        public ResourceType Resource { get; private set; }

        [field: SerializeField]
        public int Quota { get; private set; }

        public ResourceQuotaItem(ResourceType resource, int quota)
        {
            Resource = resource;
            Quota = quota;
        }

        public static Dictionary<ResourceType, int> DictionaryFrom(IEnumerable<ResourceQuotaItem> quotaItems)
        {
            var quota = new Dictionary<ResourceType, int>();
            foreach (var quotaItem in quotaItems)
                if (!quota.TryAdd(quotaItem.Resource, quotaItem.Quota))
                    quota[quotaItem.Resource] += quotaItem.Quota;

            return quota;
        }
    }

}