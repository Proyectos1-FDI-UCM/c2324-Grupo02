using ResourceCollectionSystem;
using SaveSystem.Saveable;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Flyweight;

using UpgradeIndex = System.Int32;
using PurchaseCount = System.Int32;

namespace SaveablesSystem.UpgradeSystem
{
    internal class SaveableUpgradesPool : MonoBehaviour, ISaveable<IReadOnlyDictionary<UpgradeIndex, PurchaseCount>>
    {
        private Dictionary<UpgradeIndex, PurchaseCount> _upgradeIndicesPurchases;
        private ResourceUpgradesPool _resourceUpgradesPool;

        public ResourceUpgradesPool ResourceUpgradesPool => _resourceUpgradesPool;

        public IReadOnlyDictionary<UpgradeIndex, PurchaseCount> GetData() => _upgradeIndicesPurchases;

        public bool TrySetData(IReadOnlyDictionary<UpgradeIndex, PurchaseCount> saveData)
        {
            _upgradeIndicesPurchases = new Dictionary<UpgradeIndex, PurchaseCount>(saveData);
            return true;
        }

        public void AddUpgrade(ResourceUpgradeFlyweight upgrade)
        {
            if (_resourceUpgradesPool.TryGetUpgradeIndex(upgrade, out int index)
                && !_upgradeIndicesPurchases.TryAdd(index, 1))
                _upgradeIndicesPurchases[index]++;
        }

        public void RemoveUpgrade(ResourceUpgradeFlyweight upgrade)
        {
            if (_resourceUpgradesPool.TryGetUpgradeIndex(upgrade, out int index)
                && _upgradeIndicesPurchases.TryGetValue(index, out int purchases))
                _upgradeIndicesPurchases[index] = Mathf.Max(purchases - 1, 0);
        }

        private void Awake()
        {
            _resourceUpgradesPool = GetComponent<ResourceUpgradesPool>();
            _upgradeIndicesPurchases = new Dictionary<UpgradeIndex, PurchaseCount>();
        }
    }
}