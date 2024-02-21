using System.Collections.Generic;
using UpgradesSystem.Flyweight;

namespace UpgradesSystem.Resource
{
    public interface IResourceUpgrade
    {
        bool TryPurchase(Dictionary<ResourceType, int> resourceQuantityPairs, out Dictionary<ResourceType, int> purchaseCost);
    }
}
