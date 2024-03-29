using UnityEngine;
using UpgradesSystem.Client;
using UpgradesSystem.Flyweight;
using UpgradesSystem.Resource;

namespace UpgradesSystem.Test
{
    internal class TestUpgradeClient : MonoBehaviour, IUpgradeClient
    {
        [SerializeField]
        private ResourceUpgradeFlyweight _upgradeFlyweight;

        [SerializeField]
        private ResourceQuantityItem[] _resources;

        public bool TryPurchase(IResourceUpgrade upgrade) =>
            upgrade.TryPurchase(ResourceQuantityItem.DictionaryFrom(_resources), out _);

        [ContextMenu(nameof(TryPurchaseUpgrade))]
        private void TryPurchaseUpgrade() =>
            Debug.Log(TryPurchase(_upgradeFlyweight.Create())
                      ? "Upgrade purchased successfully"
                      : "Upgrade purchase failed");
    }
}
