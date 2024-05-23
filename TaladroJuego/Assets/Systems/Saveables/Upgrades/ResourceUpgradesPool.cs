using System;
using UnityEngine;
using UpgradesSystem.Flyweight;

namespace SaveablesSystem.UpgradeSystem
{
    internal class ResourceUpgradesPool : MonoBehaviour
    {
        [SerializeField]
        private ResourceUpgradeFlyweight[] _resourceUpgradeFlyweights;

        public bool TryGetUpgradeIndex(ResourceUpgradeFlyweight upgrade, out int index)
        {
            index = Array.FindIndex(_resourceUpgradeFlyweights, u => u == upgrade);
            return index != -1;
        }

        public bool TryGetUpgrade(int index, out ResourceUpgradeFlyweight upgrade)
        {
            if (index < 0 || index >= _resourceUpgradeFlyweights.Length)
            {
                upgrade = null;
                return false;
            }

            upgrade = _resourceUpgradeFlyweights[index];
            return true;
        }
    }
}