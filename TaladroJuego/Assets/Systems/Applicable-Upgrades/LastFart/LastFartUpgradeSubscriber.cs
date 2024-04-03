using ApplicableUpgradesSystem;
using MovementSystem.LastFart;
using MovementSystem.Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApplicableUpgradesSystem.LastFartUpgrade
{
    public class LastFartUpgradeSubscriber : MonoBehaviour
    {
        [SerializeField] private LastFartLauncher _fartLauncher;
        [SerializeField] private LastFartUpgradeFlyweight[] _upgrades;

        private void Awake()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeLastFartEvent += OnUpgradeLastFartEvent;
            }
        }

        private void OnDestroy()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeLastFartEvent -= OnUpgradeLastFartEvent;
            }
        }

        private LastFartLauncher OnUpgradeLastFartEvent()
        {
            return _fartLauncher;
        }
    }
}

