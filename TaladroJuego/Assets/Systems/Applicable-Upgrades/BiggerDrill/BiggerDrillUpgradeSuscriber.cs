using UnityEngine;
using System;
using MovementSystem.Profile;

namespace ApplicableUpgradesSystem
{
    internal class BiggerDrillrUpgradeSubscriber : MonoBehaviour
    {
        [SerializeField] private Transform _drillTransform;
        [SerializeField] private BiggerDrillUpgradeFlyweight[] _upgrades;

        private void Awake()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeBiggerDrillEvent += OnUpgradeBiggerDrillEvent;
            }
        }

        private void OnDestroy()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeBiggerDrillEvent -= OnUpgradeBiggerDrillEvent;
            }
        }

        private Transform OnUpgradeBiggerDrillEvent()
        {
            return _drillTransform;
        }
    }
}