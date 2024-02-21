using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Flyweight;
using UpgradesSystem.Resource;

namespace UpgradesSystem.Test
{
    public class TestUpgradeClient : MonoBehaviour
    {
        [SerializeField]
        private ResourceUpgradeFlyweight _upgradeFlyweight;

        [SerializeField]
        private ResourceQuotaItem[] _resources;

        [ContextMenu(nameof(TryPurchaseUpgrade))]
        private void TryPurchaseUpgrade() =>
            Debug.Log(_upgradeFlyweight.Create().TryPurchase(ResourceQuotaItem.DictionaryFrom(_resources), out _)
                      ? "Upgrade purchased successfully"
                      : "Upgrade purchase failed");
    }
}
