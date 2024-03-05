
using ApplicableUpgradesSystem.StatusUp;
using StatusSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApplicableUpgradesSystem.StatusRefillObserver
{
    internal class StatusRefillObserverUpgradeSubscriber : MonoBehaviour
    {
        [SerializeField] private StatusParameter _statusParameter;
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

        private StatusParameter OnUpgradeStatusUpEvent()
        {
            return _statusParameter;
        }
    }
}

