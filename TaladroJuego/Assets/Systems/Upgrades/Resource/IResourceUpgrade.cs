using System.Collections.Generic;
using UpgradesSystem.Flyweight;

namespace UpgradesSystem.Resource
{
    public interface IResourceUpgrade
    {
        IReadOnlyDictionary<ResourceType, int> PurchaseCost { get; }
        bool TryPurchase(Dictionary<ResourceType, int> resourceQuantityPairs, out Dictionary<ResourceType, int> purchaseCost);
    }
}
