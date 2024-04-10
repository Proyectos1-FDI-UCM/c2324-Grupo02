using ApplicableUpgradesSystem.StatusMaxVaueUp;
using StatusSystem;
using UnityEngine;
using TerrainResourcesSystem;

namespace ApplicableUpgradesSystem.MiningEfficiency
{
    internal class MiningEfficiencyUgradeSubscriber: MonoBehaviour
    {
        [SerializeField] private ResourcesModificationObservable _observableResources;
        [SerializeField] private MiningEfficiencyUgradeFlyweight[] _upgrades;

        private void Awake()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeMiningEfficiencyEvent += OnUpgradeStatusUpEvent;
            }
        }

        private void OnDestroy()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeMiningEfficiencyEvent -= OnUpgradeStatusUpEvent;
            }
        }

        private ResourcesModificationObservable OnUpgradeStatusUpEvent()
        {
            return _observableResources;
        }
    }
}

