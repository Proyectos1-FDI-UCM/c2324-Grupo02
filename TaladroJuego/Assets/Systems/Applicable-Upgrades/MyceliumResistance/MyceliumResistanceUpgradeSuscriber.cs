using UnityEngine;
using System;
using MovementSystem.Profile;
using DamageSystem.Damager;

namespace ApplicableUpgradesSystem
{
    internal class MyceliumResistanceUpgradeSubscriber : MonoBehaviour
    {
        [SerializeField] private ConstantDamager[] _damagers;
        [SerializeField] private MyceliumResistanceUpgradeFlyweight[] _upgrades;

        private void Awake()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeMyceliumResistanceEvent += OnUpgradeMyceliumResistanceEvent;
            }
        }

        private void OnDestroy()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeMyceliumResistanceEvent -= OnUpgradeMyceliumResistanceEvent;
            }
        }

        private ConstantDamager[] OnUpgradeMyceliumResistanceEvent()
        {
            return _damagers;
        }
    }
}