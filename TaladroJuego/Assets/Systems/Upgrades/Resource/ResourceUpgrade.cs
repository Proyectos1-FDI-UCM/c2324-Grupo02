using System.Collections.Generic;
using UpgradesSystem.Flyweight;

namespace UpgradesSystem.Resource
{
    internal readonly struct ResourceUpgrade : IResourceUpgrade
    {
        private readonly Dictionary<ResourceType, int> _cost;
        public IReadOnlyDictionary<ResourceType, int> PurchaseCost => _cost;

        public ResourceUpgrade(Dictionary<ResourceType, int> cost)
        {
            _cost = cost;
        }

        public readonly bool TryPurchase(Dictionary<ResourceType, int> resourceQuantityPairs, out Dictionary<ResourceType, int> purchaseCost)
        {
            purchaseCost = new Dictionary<ResourceType, int>(_cost);
            Dictionary<ResourceType, int> cost = new Dictionary<ResourceType, int>(_cost);
            foreach (var resource in resourceQuantityPairs)
            {
                if (ReduceQuota(cost, resource.Key, resource.Value) <= 0)
                    cost.Remove(resource.Key);
            }

            return cost.Count == 0;
        }

        private static int ReduceQuota(Dictionary<ResourceType, int> quota, ResourceType resource, int amount)
        {
            int newQuota = -amount;
            if (quota.TryGetValue(resource, out var quotaAmount))
            {
                newQuota += quotaAmount;
                quota[resource] = newQuota;
            }

            return newQuota;
        }
    }
}
