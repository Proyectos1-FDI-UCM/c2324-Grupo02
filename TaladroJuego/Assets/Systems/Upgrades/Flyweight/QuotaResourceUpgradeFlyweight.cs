using System.Collections.Generic;
using System;
using UnityEngine;
using UpgradesSystem.Resource;
using UpgradesSystem.Flyweight.Applicable;

namespace UpgradesSystem.Flyweight
{
    [CreateAssetMenu(fileName = "QuotaResourceUpgradeFlyweight", menuName = "Upgrades/Flyweight/QuotaResourceUpgradeFlyweight")]
    internal class QuotaResourceUpgradeFlyweight : ResourceUpgradeFlyweight
    {
        [SerializeField]
        private ResourceQuantityItem[] _resourceQuotaItems;
        [SerializeField]
        private ApplicableUpgradeFlyweight _applicableUpgradeFlyweight;

        public override IResourceUpgrade Create()
        {
            return new ApplicableResourceUpgrade(
                new ResourceUpgrade(ResourceQuantityItem.DictionaryFrom(_resourceQuotaItems)),
                _applicableUpgradeFlyweight.Create());
        }
    }
}