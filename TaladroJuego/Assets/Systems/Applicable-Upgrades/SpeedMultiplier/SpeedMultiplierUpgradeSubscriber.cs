using UnityEngine;
using System;
using MovementSystem.Profile;

namespace ApplicableUpgradesSystem
{
    internal class SpeedMultiplierUpgradeSubscriber : MonoBehaviour
    {
        [SerializeField] private SpeedMultiplier _multiplier;
        [SerializeField] private SpeedMultiplierUpgradeFlyweight[] _upgrades;

        private void Awake()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeSpeedMultiplierEvent += OnUpgradeSpeedMultiplierEvent;
            }
        }

        private void OnDestroy()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeSpeedMultiplierEvent -= OnUpgradeSpeedMultiplierEvent;
            }
        }

        private SpeedMultiplier OnUpgradeSpeedMultiplierEvent()
        {
            return _multiplier;
        }
    }
}