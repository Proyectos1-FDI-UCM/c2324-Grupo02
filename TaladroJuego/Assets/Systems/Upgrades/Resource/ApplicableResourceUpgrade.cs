using System.Collections.Generic;
using UpgradesSystem.Applicable;
using UpgradesSystem.Flyweight;

namespace UpgradesSystem.Resource
{
    internal readonly struct ApplicableResourceUpgrade : IResourceUpgrade
    {
        private readonly IResourceUpgrade _resourceUpgrade;
        private readonly IApplicableUpgrade _applicableUpgrade;

        public ApplicableResourceUpgrade(IResourceUpgrade resourceUpgrade, IApplicableUpgrade applicableUpgrade)
        {
            _resourceUpgrade = resourceUpgrade;
            _applicableUpgrade = applicableUpgrade;
        }

        public bool TryPurchase(Dictionary<ResourceType, int> resourceQuantityPairs)
        {
            bool success = _resourceUpgrade.TryPurchase(resourceQuantityPairs);
            if (success)
                _applicableUpgrade.Apply();

            return success;
        }
    }
}
