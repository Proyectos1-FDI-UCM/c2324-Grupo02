using SaveSystem.Saveable;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Flyweight;
using UpgradesSystem.Resource;

using UpgradeIndex = System.Int32;
using PurchaseCount = System.Int32;

namespace SaveablesSystem.UpgradeSystem
{
    internal class SaveableUpgradesPoolApplier : MonoBehaviour, ISaveable<IReadOnlyDictionary<UpgradeIndex, PurchaseCount>>
    {
        [SerializeField]
        private SaveableUpgradesPool _saveableUpgradesPool;

        public IReadOnlyDictionary<UpgradeIndex, PurchaseCount> GetData() => _saveableUpgradesPool.GetData();

        public bool TrySetData(IReadOnlyDictionary<UpgradeIndex, PurchaseCount> saveData)
        {
            bool success = _saveableUpgradesPool.TrySetData(saveData);
            if (success)
                foreach (KeyValuePair<UpgradeIndex, PurchaseCount> upgradeIndexPurchase in saveData)
                    if (_saveableUpgradesPool.ResourceUpgradesPool.TryGetUpgrade(
                        upgradeIndexPurchase.Key,
                        out ResourceUpgradeFlyweight upgrade))
                    {
                        IResourceUpgrade resourceUpgrade = upgrade.Create();
                        for (int i = 0; i < upgradeIndexPurchase.Value; i++)
                            resourceUpgrade.TryPurchase(
                                new Dictionary<ResourceType, int>(resourceUpgrade.PurchaseCost),
                                out _);

                    }

            return success;
        }
    }
}