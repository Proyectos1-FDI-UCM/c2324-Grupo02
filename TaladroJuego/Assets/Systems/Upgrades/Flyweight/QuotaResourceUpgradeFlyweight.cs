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
        private ResourceQuotaItem[] _resourceQuotaItems;
        [SerializeField]
        private ApplicableUpgradeFlyweight _applicableUpgradeFlyweight;

        public override IResourceUpgrade Create()
        {
            return new ApplicableResourceUpgrade(
                new ResourceUpgrade(ResourceQuotaItem.DictionaryFrom(_resourceQuotaItems)),
                _applicableUpgradeFlyweight.Create());
        }
    }
}