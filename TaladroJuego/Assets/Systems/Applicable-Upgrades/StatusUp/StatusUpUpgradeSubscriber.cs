using MovementSystem.Profile;
using StatusSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApplicableUpgradesSystem
{
    internal class StatusUpUpgradeSubscriber : MonoBehaviour
    {
        [SerializeField] private IStatusParameter _statusParameter;
        [SerializeField] private StatusUpUpgradeFlyweight[] _upgrades;

        private void Awake()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeStatusUpEvent += OnUpgradeStatusUpEvent;
            }
        }

        private void OnDestroy()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeStatusUpEvent += OnUpgradeStatusUpEvent;
            }
        }

        private IStatusParameter OnUpgradeStatusUpEvent()
        {
            return _statusParameter;
        }
    }
}

