using MovementSystem.Profile;
using UnityEngine;

namespace ApplicableUpgradesSystem.RadarIncrease
{
    internal class RadarIncreaseUpgradeSubscribe : MonoBehaviour
    {
        [SerializeField] private Material _radarMaterial;
        [SerializeField] private RadarIncreaseUpgradeFlyweight[] _upgrades;

        private void Awake()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeRadarIncreaseEvent += OnUpgradeSpeedMultiplierEvent;
            }
        }

        private void OnDestroy()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeRadarIncreaseEvent -= OnUpgradeSpeedMultiplierEvent;
            }
        }

        private Material OnUpgradeSpeedMultiplierEvent()
        {
            return _radarMaterial;
        }
    }
}

