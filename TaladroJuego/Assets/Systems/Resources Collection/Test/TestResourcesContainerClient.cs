using UnityEngine;
using UpgradesSystem.Client;
using UpgradesSystem.Flyweight;
using UpgradesSystem.Resource;

namespace ResourceCollectionSystem.Test
{
    internal class TestResourcesContainerClient : MonoBehaviour, IUpgradeClient
    {
        [SerializeField]
        private ResourcesConainer _resourcesContainer;

        [SerializeField]
        private ResourceUpgradeFlyweight _upgradeFlyweight;

        [SerializeField]
        private ResourceQuantityItem[] _resources;

        public bool TryPurchase(IResourceUpgrade upgrade) =>
            _resourcesContainer.TryPurchase(upgrade);

        [ContextMenu(nameof(SetResources))]
        private void SetResources()
        {
            foreach (var resource in _resources)
                _resourcesContainer.AccountForResource(resource.Resource, resource.Quantity);
        }

        [ContextMenu(nameof(TryPurchaseUpgrade))]
        private void TryPurchaseUpgrade() =>
            Debug.Log(TryPurchase(_upgradeFlyweight.Create())
                      ? "Upgrade purchased successfully"
                      : "Upgrade purchase failed");
    }
}